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

        // GF mode
        if (Main.PlayerColor == new Color(253f / 255f, 104f / 255f, 157f / 255f))
        {
            healthController.maxHealth = 10000f;
            healthController.currHealth = 10000f;
        }
    }

    //when enemy hits player
    void OnCollisionEnter(Collision c) {
        GameObject otherGO = c.gameObject;
        Collider coll = c.collider;

        ElementHandler eEle = otherGO.GetComponent<ElementHandler>();
        if (eEle != null) {
            HandleHit(eEle.enemyElementofChoice, ElementHandler.enemyLevel);

            //allow invicibility for some time
            InvincibleToLayer("Enemies", invincibilityTime);

            return;
        }

        BossDamageHandler bossDamageHandler = otherGO.GetComponent<BossDamageHandler>();
        if (bossDamageHandler != null) {
            //allow invicibility for some time
            InvincibleTo(coll, invincibilityTime);
            //HandleHit(eEle.enemyElementofChoice);
            spriteController.Tint(hitColor, hitBlinkTime);
            healthController.Damage(Main.GET_ELEMENT_DEFINITION(elementTypes.None).damageOnHit);
            return;

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

        if (otherGO.layer == LayerMask.NameToLayer("EnvironmentalHazards")) { 
            EnvironmentalHazard eH = otherGO.GetComponent<EnvironmentalHazard>();
            if (eH != null) {
                AudioController.RepeatClip("vine-hurt");
            }
        }
    }

    void HandleHit(elementTypes elemType, int currentEnemyLevels) {
        
        AudioController.PlayClip("enemyHit");

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
                AudioController.RepeatClip("vine-hurt");
            }
        }
    }

    public void OnTriggerExit(Collider c) {
        GameObject otherGO = c.gameObject;
        if (otherGO.layer == LayerMask.NameToLayer("EnvironmentalHazards")) { 
            EnvironmentalHazard eH = otherGO.GetComponent<EnvironmentalHazard>();
            if (eH != null) {
                AudioController.StopClip();
            }
        }
    }

    public void InvincibleTo(Collider otherCollider, float time) {
        StartCoroutine(_InvincibleTo(otherCollider, time));
    }

    IEnumerator _InvincibleTo(Collider otherCollider, float time) {
        Collider c = GetComponent<Collider>();
        Physics.IgnoreCollision(GetComponent<Collider>(), otherCollider, true);
        yield return new WaitForSeconds(time);
        Physics.IgnoreCollision(GetComponent<Collider>(), otherCollider, false);
    }

    public void InvincibleToLayer(string layerName, float time) {
        StartCoroutine(_InvincibleToLayer(layerName, time));
    }

    IEnumerator _InvincibleToLayer(string layerName, float time) {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer(layerName), true);
        yield return new WaitForSeconds(time);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer(layerName), false);
    }
}
