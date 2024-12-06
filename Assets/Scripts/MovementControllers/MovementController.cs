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
    public float terminalVelocity = 15f;

    [Header("Dynamic")]
    public Rigidbody rBody;
    public bool canJump = true;
    public float distToGround;
    public float distToWall;
    public bool canDoubleJump;
    public int movementDirection = 1;
    public bool canMove = true;
    public float prevHAxis = 0;
    public float prevVAxis = 0;
    public float prevJAxis = 0;
    public float wallJumpVelocity;

    void Awake() {
        rBody = GetComponent<Rigidbody>();
        Collider c = GetComponent<Collider>();
        distToGround = c.bounds.extents.y;
        distToWall = c.bounds.extents.x;

        wallJumpVelocity = 0;

        canJump = true;
        canDoubleJump = true;
    }

    public void Move(float hAxis, float vAxis, float jAxis) {
        if (!canMove) return;

        Vector3 vel;

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
            wallJumpVelocity = -hAxis * walkingSpeed;
            Invoke("ResetWallJump",wallJumpInvertDelay);
            Jump(1.5f);
        }

        //double jumping
        if (!grounded && canJump && canDoubleJump && jAxis == 1) {
            vel = rBody.velocity;
            vel.y = 0;
            rBody.velocity = vel;
            Jump();
            canDoubleJump = false;
        }

        if (grounded) {
            canDoubleJump = true;
        }

        bool holdingWall = HoldingWall(hAxis);

        float hMovement;
        float vMovement;
        if (!holdingWall && wallJumpVelocity == 0f) {
            //walking
            hMovement = hAxis * walkingSpeed * movementDirection;
        } else {
            //used for wall jumping
            hMovement = wallJumpVelocity;
        }
        //moveDown
        vMovement = Mathf.Min(vAxis, 0f) * diveSpeed * Time.deltaTime;

        //apply movement
        rBody.velocity = new Vector3(hMovement, rBody.velocity.y + vMovement, 0);

        //terminal down velocity (fixes clipping)
        vel = rBody.velocity;
        vel.y = Mathf.Max(vel.y, -terminalVelocity);
        rBody.velocity = vel;

        //saves these so other classes (platforms) can read them
        prevHAxis = hAxis;
        prevVAxis = vAxis;
        prevJAxis = jAxis;

        //stop z axis from changing ever so slightly
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f, LayerMask.GetMask("Ground", "Platforms"));
    }

    bool HoldingWall(float hAxis) { //checks raycast at head, midbody, and foot
        return Physics.Raycast(transform.position + Vector3.down * distToGround * 0.9f, Vector3.right * hAxis * movementDirection, distToWall + 0.1f, LayerMask.GetMask("Ground", "Platforms")) || Physics.Raycast(transform.position + Vector3.up * distToGround, Vector3.right * hAxis * movementDirection, distToWall + 0.1f, LayerMask.GetMask("Ground", "Platforms")) || Physics.Raycast(transform.position, Vector3.right * hAxis * movementDirection, distToWall + 0.1f, LayerMask.GetMask("Ground", "Platforms"));
    }

    void Jump(float multiplier = 1f) {
        Vector3 jForce = Vector3.up * jumpPower * multiplier;
        rBody.AddForce(jForce,ForceMode.Force);
        canJump = false;
    }

    void InvertMovement() {
        movementDirection *= -1;
    }

    void ResetWallJump() {
        wallJumpVelocity = 0f;
    }
}
