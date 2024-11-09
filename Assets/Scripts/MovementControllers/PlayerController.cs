using UnityEngine;

[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(SpriteController))]
public class PlayerController : MonoBehaviour
{
    //[Header("Inscribed")]
    

    [Header("Dynamic")]
    public MovementController movementController;
    public SpriteController spriteController;
    

    void Awake() {
        movementController = GetComponent<MovementController>();
        spriteController = GetComponent<SpriteController>();
        spriteController.color = Main.PlayerColor;
    }
    void FixedUpdate() {
        movementController.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetAxis("Jump"));
        if (
            (Input.GetKey("right") && spriteController.flipped && !Input.GetKey("left")) || 
            (Input.GetKey("left") && !spriteController.flipped && !Input.GetKey("right")) ||
            (!(Input.GetKey("right") || Input.GetKey("left")) && ((movementController.rBody.velocity.x > 0 && spriteController.flipped) || (movementController.rBody.velocity.x < 0 && !spriteController.flipped)))) {
            spriteController.Flip();
        }
    }
}
