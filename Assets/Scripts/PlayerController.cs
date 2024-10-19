using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class PlayerController : MonoBehaviour
{
    [Header("Inscribed")]
    public float walkingSpeed = 6f;
    public float moveDownSpeed = 75f;
    public float jumpPower = 350f;
    public float jumpDelay = 0.1f;
    public float wallJumpInvertDelay = 0.2f;

    [Header("Dynamic")]
    public Rigidbody rBody;
    public bool canJump = true;
    public bool onGround = false;
    public float distToGround;
    public float distToWall;
    public bool canDoubleJump;
    public int movementDirection = 1;

    void Awake() {
        rBody = GetComponent<Rigidbody>();
        Collider c = GetComponent<Collider>();
        distToGround = c.bounds.extents.y;
        distToWall = c.bounds.extents.x;

        canJump = true;
        canDoubleJump = true;
    }
    void FixedUpdate() {
        //jumping
        if (Input.GetAxis("Jump") == 0) {
            canJump = true;
        }

        bool grounded = IsGrounded();

        if (grounded && canJump && Input.GetAxis("Jump") == 1) {
            Jump();
        }
        
        //wall jumping (must check before double jump)
        if (HoldingWall() && canJump && Input.GetAxis("Jump") == 1) {
            InvertMovement();
            Invoke("InvertMovement",wallJumpInvertDelay);
            Jump(1.5f);
        }

        //double jumping
        if (!grounded && canJump && canDoubleJump && Input.GetAxis("Jump") == 1) {
            Vector3 vel = rBody.velocity;
            vel.y = 0;
            rBody.velocity = vel;
            Jump();
            canDoubleJump = false;
        }

        if (grounded) {
            canDoubleJump = true;
        }

        bool holdingWall = HoldingWall();

        if (!holdingWall) {
            //walking
            float hMovement = Input.GetAxis("Horizontal") * walkingSpeed * movementDirection;
            //moveDown
            float vMovement = Mathf.Min(Input.GetAxis("Vertical"), 0f) * moveDownSpeed * Time.deltaTime;

            //apply
            rBody.velocity = new Vector3(hMovement, rBody.velocity.y + vMovement, 0);

        }
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f, ~LayerMask.NameToLayer("Ground"));
    }

    bool HoldingWall() {
        return Physics.Raycast(transform.position, Vector3.right * Input.GetAxis("Horizontal") * movementDirection, distToWall + 0.1f, ~LayerMask.NameToLayer("Ground"));
    }

    void Jump(float multiplier = 1f) {
        Vector3 jForce = Vector3.up * jumpPower * multiplier;
        rBody.AddForce(jForce,ForceMode.Force);
        canJump = false;
    }

    void InvertMovement() {
        movementDirection *= -1;
    }

    // void OnCollisionEnter(Collision c) {
    //     GameObject otherGO = c.gameObject;
    //     if (otherGO.layer == LayerMask.NameToLayer("Platforms")) {
    //         if (Input.GetAxis("Vertical") == -1) {
    //             otherGO.GetComponent<Collider>().isTrigger = true;
    //         }
    //     }
    // }
    
    // void OnTriggerExit(Collider c) {
    //     GameObject otherGO = c.gameObject;
    //     if (otherGO.layer == LayerMask.NameToLayer("Platforms")) {
    //         if (otherGO.transform.position.y < transform.position.y - distToGround) {
    //             otherGO.GetComponent<Collider>().isTrigger = false;
    //         }
    //     }
    // }
}
