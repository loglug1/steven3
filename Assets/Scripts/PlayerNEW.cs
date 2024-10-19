using UnityEngine;

public class PlayerNEW : MonoBehaviour
{
    const float gravAcc = 9.8f;

    [Header("Inscribed")]
    float walkingSpeed;

    [Header("Dynamic")]
    float distToGround;
    float distToWall;


    void Awake() {
        Collider c = GetComponent<Collider>();
        distToGround = c.bounds.extents.y;
        distToWall = c.bounds.extents.x;
    }
    void FixedUpdate() {
        Vector3 fallingVel = Vector3.down * 0f;
        Vector3 walkingVel = Vector3.right * walkingSpeed * Input.GetAxis("Horizontal");
        transform.position += walkingVel * Time.deltaTime;
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
    }

    bool TouchingWall() {
        return Physics.Raycast(transform.position, Vector3.right * Input.GetAxis("Horizontal"), distToWall + 0.1f);
    }
}
