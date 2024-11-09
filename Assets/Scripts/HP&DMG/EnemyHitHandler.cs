using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatusController))]
[RequireComponent(typeof(HealthController))]
[RequireComponent(typeof(ElementHandler))]
[RequireComponent(typeof(SpriteController))]
public class EnemyHitHandler : MonoBehaviour
{
    public HealthController healthController;
    public ElementHandler     elementHandling;
    public StatusController statusController;
    public SpriteController spriteController;

    void Awake()
    {
        healthController = GetComponent<HealthController>();
        elementHandling  = GetComponent<ElementHandler>();
        statusController = GetComponent<StatusController>();
        spriteController = GetComponent<SpriteController>();
    }
    //meant for water projectiles
    void OnCollisionEnter(Collision c) {
        GameObject otherGO = c.gameObject;
        wandProjectile wProj = otherGO.GetComponent<wandProjectile>();

        HandleProjectile(wProj);
    }

    //meant for all other projectiles
    void OnTriggerEnter(Collider c) {
        GameObject otherGO = c.gameObject;
        wandProjectile wProj = otherGO.GetComponent<wandProjectile>();

        HandleProjectile(wProj);
    }

    void HandleProjectile(wandProjectile wProj)
    {
        
        if (wProj != null) {
            spriteController.Tint(new Color(1, 0, 0), 0.25f);
            
            //status effects
            statusController.ApplyEffect(wProj.eleDefs, Inventory.I.playerElementLevels);

            EnemyDamageCalculation(wProj.eleDefs, wProj); 
            healthController.Damage(wProj.dmg);

            Destroy(wProj.gameObject);
        }
    }

    public float EnemyDamageCalculation(List<ElementDefinition> ele, wandProjectile wProj)
    {
        // cycles through each element with decreasing effect
        float i = 1f;
        foreach (ElementDefinition elementDef in ele) {
            if (isStrong(elementDef, elementHandling.enemyElementofChoice)) {
                wProj.dmg = wProj.dmg + 5/i;  // this calculation is just placeholder for now to test functionality
            }
            if (isWeak(elementDef, elementHandling.enemyElementofChoice)) {
                wProj.dmg = wProj.dmg - 2/i; // res is weak... change later to "weakMult" instead
            }
            i = i + 1f;
        }
        Debug.Log(wProj.dmg);
        return(wProj.dmg);
    }

    static public bool isWeak(ElementDefinition playerElementDef, elementTypes enemyEle ) {
        if (playerElementDef.weakElement == enemyEle) {
            return true;
        } else {
            return false;
        }
    }
    static public bool isStrong(ElementDefinition playerElementDef, elementTypes enemyEle) {
        if (playerElementDef.strElement == enemyEle) {
            return true;
        } else {
            return false;
        }
    }

    public void OnTriggerStay(Collider c) {
        GameObject otherGO = c.gameObject;
        if (otherGO.layer == LayerMask.NameToLayer("EnvironmentalHazards")) { 
            EnvironmentalHazard eH = otherGO.GetComponent<EnvironmentalHazard>();
            if (eH != null) {
                if (elementHandling.enemyElementofChoice == elementTypes.Grass && eH.hT == HazardType.Vines) {
                    healthController.Damage(Time.deltaTime * 0);
                }
                else if (elementHandling.enemyElementofChoice == elementTypes.Fire && eH.hT == HazardType.Lava) {
                    healthController.Damage(Time.deltaTime * 0);
                }
                else {
                    healthController.Damage(Time.deltaTime * eH.damage);
                    
                }
            }
        }
    }
}
