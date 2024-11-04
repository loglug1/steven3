using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BossDamageHandler : MonoBehaviour
{
    public HealthController healthController;
    public elementTypes bossType {
        get { return _bossType; }
        set { SetType(value); }
    }
    private elementTypes _bossType = elementTypes.None;
    public SpriteRenderer stemRenderer;
    public elementTypes[] elementChances;
    public float            strMult = 1.3f;
    public float            secondaryStrMult = 1.2f;
    public float            resMult = .80f;
    public float            secondaryResMult = .90f;
    public float            elementChangeChance = 0.5f;
    
    void Awake() {
        healthController = transform.parent.GetComponent<HealthController>();
        stemRenderer = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
    }

    //meant for water projectiles
    void OnCollisionEnter(Collision c) {
        GameObject otherGO = c.gameObject;
        wandProjectile wProj = otherGO.GetComponent<wandProjectile>();

        HandleProjectile(wProj.eleDefs, wProj, otherGO);
    }

    //meant for all other projectiles
    void OnTriggerEnter(Collider c) {
        GameObject otherGO = c.gameObject;
        wandProjectile wProj = otherGO.GetComponent<wandProjectile>();

        HandleProjectile(wProj.eleDefs, wProj, otherGO);
    }
    void HandleProjectile(List<ElementDefinition> ele, wandProjectile wProj, GameObject otherGO)
    {
        // primary grass
        if (wProj != null) {

            if (Random.Range(0f,1f) > elementChangeChance) {
                bossType = elementChances[(int)Mathf.Floor(Random.Range(0,elementChances.Length))];
            }

        float i = 1f;
        foreach (ElementDefinition elementDef in ele) {
            if (EnemyHitHandler.isStrong(elementDef, bossType)) {
                wProj.dmg = wProj.dmg + 3f/i;  // this calculation is just placeholder for now to test functionality
            }
            if (EnemyHitHandler.isWeak(elementDef, bossType)) {
                wProj.dmg = wProj.dmg - 2f/i; // res is weak... change later to "weakMult" instead
            }
            i = i + 1f;
        }
            healthController.Damage(wProj.dmg);
            Destroy(otherGO);
        }
    }

    void SetType(elementTypes e) {
        _bossType = e;
        stemRenderer.color = Main.GET_ELEMENT_DEFINITION(e).color;
    }
}
