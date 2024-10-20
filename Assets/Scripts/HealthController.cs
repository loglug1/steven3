using UnityEngine;

public class HealthController : MonoBehaviour
{
    [Header("Inscribed")]
    public float maxHealth = 60f;
    public HealthBarController healthBar;
    public float currHealth = 50f;
    public Color mainColor;
    public Color lowColor;
    private EnemyElement elementChecker;
    
    // [Header("Dynamic")]

    void Awake() {
        healthBar.maxValue = maxHealth;
        healthBar.value = currHealth;

        healthBar.color = mainColor;
    }

    public void Damage(float amount) {
        currHealth -= amount;
        healthBar.value = currHealth;
        if (currHealth / maxHealth <= 0.2) {
            healthBar.color = lowColor;
        }
        if (currHealth <= 0) {
            PlayerController p;
            EnemyController e;
            BossController b;
            if ((p = GetComponent<PlayerController>()) != null) {
                Main.GameOver();
            } else if ((e = GetComponent<EnemyController>()) != null) {
                e.Die();
            } else if ((b = GetComponent<BossController>()) != null) {
                Debug.Log("You Win!");
                Destroy(gameObject);
            }
        }
    }
    public void Heal(float amount) {
        currHealth += amount;
        healthBar.value = currHealth;
        if (currHealth / maxHealth > 0.2) {
            healthBar.color = mainColor;
        }
    }

    public void OnTriggerStay(Collider c) {
        GameObject otherGO = c.gameObject;
        if (otherGO.layer == LayerMask.NameToLayer("EnvironmentalHazards")) { 
            EnvironmentalHazard eH = otherGO.GetComponent<EnvironmentalHazard>();
            if (eH != null) {
                elementChecker = GetComponent<EnemyElement>();

                if (elementChecker == null) { // player doesn't have enemyelement
                    Damage(Time.deltaTime * eH.damage);
                    return;
                }

                if (elementChecker.enemyElementofChoice == elementTypes.Grass && eH.hT == HazardType.Vines) {
                    Damage(Time.deltaTime * 0);
                }
                else if (elementChecker.enemyElementofChoice == elementTypes.Fire && eH.hT == HazardType.Lava) {
                    Damage(Time.deltaTime * 0);
                }
                else {
                    Damage(Time.deltaTime * eH.damage);
                    
                }
            }
        }
    }
}
