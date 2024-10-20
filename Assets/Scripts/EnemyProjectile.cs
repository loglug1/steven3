using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Inscribed")]
    public elementTypes type = elementTypes.None;
    public Vector3 vel;

    [Header("Dynamic")]
    public Rigidbody rBody;

    void Awake() {
        rBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (vel != Vector3.zero) {
        transform.position += vel * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider c) {
        GameObject otherGO = c.gameObject;
        if(otherGO.layer == LayerMask.NameToLayer("Ground")) {
            Destroy(gameObject);
        }
    }
}
