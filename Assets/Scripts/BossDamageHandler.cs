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

        HandleProjectile(wProj, otherGO);
    }

    //meant for all other projectiles
    void OnTriggerEnter(Collider c) {
        GameObject otherGO = c.gameObject;
        wandProjectile wProj = otherGO.GetComponent<wandProjectile>();

        HandleProjectile(wProj, otherGO);
    }
    void HandleProjectile(wandProjectile wProj, GameObject otherGO)
    {
        // primary grass
        if (wProj != null) {

            if (Random.Range(0f,1f) > elementChangeChance) {
                bossType = elementChances[(int)Mathf.Floor(Random.Range(0,elementChances.Length))];
            }

            if (wandProjectile.def1.weakElement == bossType) {
                wProj.dmg = wProj.dmg * resMult;
            }
            if (wandProjectile.def2.weakElement == bossType) {
                wProj.dmg = wProj.dmg * secondaryResMult;
            }
            if (wandProjectile.def1.strElement == bossType) {
                wProj.dmg = wProj.dmg * strMult;
            }
            if (wandProjectile.def2.strElement == bossType) {
                wProj.dmg = wProj.dmg * secondaryStrMult;
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
