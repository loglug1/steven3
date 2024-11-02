using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatusController))]
public class PlayerHitHandler : MonoBehaviour
{
    [Header("Inscribed")]
    public float invincibilityTime = 1f;

    [Header("Dynamic")]
    public HealthController healthController;
    public StatusController statusController;
    // Start is called before the first frame update
    void Awake()
    {
        healthController = GetComponent<HealthController>();
        statusController = GetComponent<StatusController>();
        ResetInvicibility();
    }

    //when enemy hits player
    void OnCollisionEnter(Collision c) {
        GameObject otherGO = c.gameObject;
        ElementHandler eEle = otherGO.GetComponent<ElementHandler>();

        if (eEle != null) {
            //allow invicibility for some time
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), true);
            Invoke("ResetInvicibility", invincibilityTime);
            HandleHit(eEle.enemyElementofChoice);
        }
    }

    void OnTriggerEnter(Collider c) {
        GameObject otherGO = c.gameObject;
        EnemyProjectile eProj = otherGO.GetComponent<EnemyProjectile>();

        if (eProj != null) {
            // handles status effect on hit
            HandleHit(eProj.type); //REPLACE WITH STATUS CONTROLLER
            Destroy(otherGO);
        }
    }

    void HandleHit(elementTypes elemType) {
        statusController.ApplyEffect(new List<elementTypes>{elemType});
        healthController.Damage(Main.GET_ELEMENT_DEFINITION(elemType).damageOnHit);
    }

    void ResetInvicibility() {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), false);
    }
}
