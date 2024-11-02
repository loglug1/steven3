using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class EnemyController : MonoBehaviour
{
    [Header("Inscribed")]
    public float playerHoneDistance = 1f;
    public bool canSeePlayer = false;
    public float objectPermanence = 0f;
    public float jumpDelay = 0.1f;

    [Header("Dynamic")]
    public bool holdingWall = false;
    public int movementDirection = 1;
    public float jAxis;
    public float hAxis;
    public float vAxis;
    public GameObject player;
    public HealthController healthController;
    public MovementController movementController;

    void Awake() {
        player = Main.GET_PLAYER();
        healthController = GetComponent<HealthController>();
        movementController = GetComponent<MovementController>();
    }
    void FixedUpdate() {
        if (IsPlayerVisible()) {
            objectPermanence = 3f;
        } else {
            objectPermanence -= Time.deltaTime;
        }

        if (objectPermanence > 0f) {
            canSeePlayer = true;
        } else {
            canSeePlayer = false;
        }
        
        //enemy movement AI
        //uses hAxis, jAxis, and vAxis to mimic controller inputs and uses player controller logic to move
        if (movementController.canMove && canSeePlayer) {
            float distanceToPlayer = player.transform.position.x - transform.position.x;

            if ( Mathf.Abs(distanceToPlayer) > playerHoneDistance) {
                hAxis = distanceToPlayer/Mathf.Abs(distanceToPlayer);
            }

            if (player.transform.position.y > transform.position.y && movementController.canJump) {
                jAxis = 1;
                Invoke("ResetJAxis", jumpDelay); //mimic holding space for jumpDelay seconds
            } else {
                vAxis = 0;
            }
        } else {
            hAxis = 0;
            vAxis = 0;
            jAxis = 0;
        }

        movementController.Move(hAxis, jAxis);
    }

    bool IsPlayerVisible() {
        Vector3 vecToPlayer = player.transform.position - transform.position;
        return !Physics.Raycast(transform.position, vecToPlayer.normalized, vecToPlayer.magnitude, LayerMask.GetMask("Ground"));
    }

    void ResetJAxis() {
        jAxis = 0;
    }

    public void Die() {
        Destroy(gameObject);
    }
}