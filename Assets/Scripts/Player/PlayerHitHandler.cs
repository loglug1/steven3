using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatusController))]
[RequireComponent(typeof(SpriteController))]
public class PlayerHitHandler : MonoBehaviour
{
    [Header("Inscribed")]
    public float invincibilityTime = 1f;
    public Color hitColor = new Color(1, 0, 0);
    public float hitBlinkTime = 0.25f;

    [Header("Dynamic")]
    public HealthController healthController;
    public StatusController statusController;
    public SpriteController spriteController;
    // Start is called before the first frame update
    void Awake()
    {
        healthController = GetComponent<HealthController>();
        statusController = GetComponent<StatusController>();
        spriteController = GetComponent<SpriteController>();
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
            HandleHit(eEle.enemyElementofChoice, ElementHandler.enemyLevel);
        }
    }

    void OnTriggerEnter(Collider c) {
        GameObject otherGO = c.gameObject;
        EnemyProjectile eProj = otherGO.GetComponent<EnemyProjectile>();

        if (eProj != null) {
            // handles status effect on hit
            HandleHit(eProj.type, ElementHandler.enemyLevel); //REPLACE WITH STATUS CONTROLLER
            Destroy(otherGO);
        }
    }

    void HandleHit(elementTypes elemType, int currentEnemyLevels) {
        
        Dictionary<elementTypes, int> enemyElementDict = new Dictionary<elementTypes, int>();
        enemyElementDict.Add(elemType, currentEnemyLevels);
        statusController.ApplyEffect(new List<ElementDefinition>{Main.GET_ELEMENT_DEFINITION(elemType)}, enemyElementDict);

        healthController.Damage(Main.GET_ELEMENT_DEFINITION(elemType).damageOnHit);
        spriteController.Tint(hitColor, hitBlinkTime);
    }

    

    void ResetInvicibility() {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), false);
    }

    public void OnTriggerStay(Collider c) {
        GameObject otherGO = c.gameObject;
        if (otherGO.layer == LayerMask.NameToLayer("EnvironmentalHazards")) { 
            EnvironmentalHazard eH = otherGO.GetComponent<EnvironmentalHazard>();
            if (eH != null) {
                spriteController.Tint(hitColor, hitBlinkTime);
                healthController.Damage(Time.deltaTime * eH.damage);
            }
        }
    }
}
