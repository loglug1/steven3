using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Inscribed")]
    public float walkingSpeed = 6f;
    public float moveDownSpeed = 75f;
    public float jumpPower = 350f;
    public float jumpDelay = 0.1f;
    public float wallJumpInvertDelay = 0.2f;
    public float playerHoneDistance = 1f;
    public bool canMove = true;

    [Header("Dynamic")]
    public Rigidbody rBody;
    public bool canJump = true;
    public bool onGround = false;
    public float distToGround;
    public float distToWall;
    public bool canDoubleJump;
    public int movementDirection = 1;
    public float jAxis;
    public float hAxis;
    public float vAxis;
    public PlayerController player;
    public HealthController healthController;

    void Awake() {
        rBody = GetComponent<Rigidbody>();
        Collider c = GetComponent<Collider>();
        distToGround = c.bounds.extents.y;
        distToWall = c.bounds.extents.x;
        player = Main.GET_PLAYER();
        healthController = GetComponent<HealthController>();

        canJump = true;
        canDoubleJump = true;
    }
    void FixedUpdate() {
        //enemy movement AI
        //uses hAxis, jAxis, and vAxis to mimic controller inputs and uses player controller logic to move
        if (canMove) {
            float distanceToPlayer = player.transform.position.x - transform.position.x;

            if ( Mathf.Abs(distanceToPlayer) > playerHoneDistance) {
                hAxis = distanceToPlayer/Mathf.Abs(distanceToPlayer);
            }

            if (player.transform.position.y > transform.position.y) {
                vAxis = 1;
                Invoke("ResetJAxis", jumpDelay); //mimic holding space for jumpDelay seconds
            } else {
                vAxis = 0;
            }
        } else {
            hAxis = 0;
            vAxis = 0;
            jAxis = 0;
        }

        //jumping
        if (jAxis == 0) {
            canJump = true;
        }

        bool grounded = IsGrounded();
        bool holdingWall = HoldingWall();

        if (grounded && canJump && jAxis == 1) {
            Jump();
        }
        
        //wall jumping (must check before double jump)
        if (holdingWall && canJump && jAxis == 1) {
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


        if (!holdingWall) {
            //walking
            float hMovement = hAxis * walkingSpeed * movementDirection;
            //moveDown
            float vMovement = vAxis * moveDownSpeed * Time.deltaTime;

            //apply
            rBody.velocity = new Vector3(hMovement, rBody.velocity.y + vMovement, 0);

        } else {
            jAxis = 1;
            Invoke("ResetJAxis", jumpDelay);
        }
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f, ~LayerMask.NameToLayer("Ground"));
    }

    bool HoldingWall() {
        return Physics.Raycast(transform.position, Vector3.right * hAxis * movementDirection, distToWall + 0.1f, ~LayerMask.NameToLayer("Ground"));
    }

    void Jump(float multiplier = 1f) {
        Vector3 jForce = Vector3.up * jumpPower * multiplier;
        rBody.AddForce(jForce,ForceMode.Force);
        canJump = false;
    }

    void InvertMovement() {
        movementDirection *= -1;
    }

    void ResetJAxis() {
        jAxis = 0;
    }

    public void Die() {
        Destroy(gameObject);
    }

    //meant for water projectiles
    void OnCollisionEnter(Collision c) {
        GameObject otherGO = c.gameObject;
        wandProjectile wProj = otherGO.GetComponent<wandProjectile>();
        if (wProj != null) {
            healthController.Damage(wProj.dmg);
            Destroy(otherGO);
        }
    }

    //meant for all other projectiles
    void OnTriggerEnter(Collider c) {
        GameObject otherGO = c.gameObject;
        wandProjectile wProj = otherGO.GetComponent<wandProjectile>();
        if (wProj != null) {
            healthController.Damage(wProj.dmg);
            Destroy(otherGO);
        }
    }
}
