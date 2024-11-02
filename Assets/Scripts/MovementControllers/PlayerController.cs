using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class PlayerController : MonoBehaviour
{
    //[Header("Inscribed")]
    

    [Header("Dynamic")]
    public MovementController movementController;
    

    void Awake() {
        movementController = GetComponent<MovementController>();
    }
    void FixedUpdate() {
        movementController.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetAxis("Jump"));
    }
}
