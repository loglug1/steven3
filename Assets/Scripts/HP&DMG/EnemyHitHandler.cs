using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatusController))]
[RequireComponent(typeof(HealthController))]
public class EnemyHitHandler : MonoBehaviour
{
    public HealthController healthController;
    public ElementHandler     elementHandling;
    public StatusController statusController;

    // variables that hold current elements to make code shorter lol
    public ElementDefinition currentDef1;
    public ElementDefinition currentDef2;
    void Awake()
    {
        healthController = GetComponent<HealthController>();
        elementHandling = GetComponent<ElementHandler>();
        statusController = GetComponent<StatusController>();
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

            currentDef1 = wandProjectile.def1;
            currentDef2 = wandProjectile.def2;
            
            //status effects
            statusController.ApplyEffect(new List<elementTypes>{currentDef1.element, currentDef2.element});

            EnemyDamageCalculation(currentDef1, currentDef2, wProj); 
            healthController.Damage(wProj.dmg);

            Destroy(wProj.gameObject);
        }
    }

    public float EnemyDamageCalculation(ElementDefinition currentDef1, ElementDefinition currentDef2, wandProjectile wProj)
    {
        if (currentDef1.weakElement == elementHandling.enemyElementofChoice) {
            wProj.dmg = wProj.dmg * elementHandling.resMult;
        }
        if (currentDef2.weakElement == elementHandling.enemyElementofChoice) {
            wProj.dmg = wProj.dmg * elementHandling.secondaryResMult;
        }
        if (currentDef1.strElement == elementHandling.enemyElementofChoice) {
            wProj.dmg = wProj.dmg * elementHandling.strMult;
        }
        if (currentDef2.strElement == elementHandling.enemyElementofChoice) {
            wProj.dmg = wProj.dmg * elementHandling.secondaryStrMult;        
        }
        return(wProj.dmg);
    }
}
