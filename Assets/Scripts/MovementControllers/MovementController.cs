using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class MovementController : MonoBehaviour
{
    [Header("Inscribed")]
    public float walkingSpeed = 6f;
    public float diveSpeed = 75f;
    public float jumpPower = 350f;
    public float wallJumpInvertDelay = 0.2f;

    [Header("Dynamic")]
    public Rigidbody rBody;
    public bool canJump = true;
    public float distToGround;
    public float distToWall;
    public bool canDoubleJump;
    public int movementDirection = 1;
    public bool canMove = true;

    void Awake() {
        rBody = GetComponent<Rigidbody>();
        Collider c = GetComponent<Collider>();
        distToGround = c.bounds.extents.y;
        distToWall = c.bounds.extents.x;

        canJump = true;
        canDoubleJump = true;
    }

    public void Move(float hAxis, float vAxis, float jAxis) {
        if (!canMove) return;

        //jumping
        if (jAxis == 0) {
            canJump = true;
        }

        bool grounded = IsGrounded();

        if (grounded && canJump && jAxis == 1) {
            Jump();
        }
        
        //wall jumping (must check before double jump)
        if (HoldingWall(hAxis) && canJump && jAxis == 1) {
            InvertMovement();
            Invoke("InvertMovement",wallJumpInvertDelay);
            Jump(1.5f);
        }

        //double jumping
        if (!grounded && canJump && canDoubleJump && jAxis == 1) {
            Vector3 vel = rBody.velocity;
            vel.y = 0;
            rBody.velocity = vel;
            Jump();
            canDoubleJump = false;
        }

        if (grounded) {
            canDoubleJump = true;
        }

        bool holdingWall = HoldingWall(hAxis);

        if (!holdingWall) {
            //walking
            float hMovement = hAxis * walkingSpeed * movementDirection;
            //moveDown
            float vMovement = Mathf.Min(vAxis, 0f) * diveSpeed * Time.deltaTime;

            //apply
            rBody.velocity = new Vector3(hMovement, rBody.velocity.y + vMovement, 0);

        }
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f, ~LayerMask.NameToLayer("Ground"));
    }

    bool HoldingWall(float hAxis) { //checks raycast at head, midbody, and foot
        return Physics.Raycast(transform.position + Vector3.down * distToGround * 0.9f, Vector3.right * hAxis * movementDirection, distToWall + 0.1f, LayerMask.GetMask("Ground")) || Physics.Raycast(transform.position + Vector3.up * distToGround, Vector3.right * hAxis * movementDirection, distToWall + 0.1f, LayerMask.GetMask("Ground")) || Physics.Raycast(transform.position, Vector3.right * hAxis * movementDirection, distToWall + 0.1f, LayerMask.GetMask("Ground"));
    }

    void Jump(float multiplier = 1f) {
        Vector3 jForce = Vector3.up * jumpPower * multiplier;
        rBody.AddForce(jForce,ForceMode.Force);
        canJump = false;
    }

    void InvertMovement() {
        movementDirection *= -1;
    }
}
