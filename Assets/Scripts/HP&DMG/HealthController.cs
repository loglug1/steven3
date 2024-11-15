using UnityEngine;

public class HealthController : MonoBehaviour
{
    [Header("Inscribed")]
    public float maxHealth = 60f; 
    public HealthBarController healthBar;
    public float currHealth = 50f;
    public float theHOLDER = 0f;
    public Color mainColor;
    public Color lowColor;
    private ElementHandler elementChecker;
    
    // [Header("Dynamic")]

    void Awake() {
        healthBar.maxValue = maxHealth;
        healthBar.value = currHealth;

        healthBar.color = mainColor;
        
    }
    void Update() {
        PlayerController p;
        if ((p = GetComponent<PlayerController>()) != null && (theHOLDER != currHealth) && (currHealth > 0)) {
            Inventory.I.UpdateHealth(currHealth);
        }
    }

    public void Damage(float amount) {
        currHealth -= amount;
        healthBar.value = currHealth;
        PlayerController p;
        if (currHealth / maxHealth <= 0.2) {
            healthBar.color = lowColor;
        }
        if (currHealth <= 0) {
            EnemyController e;
            BossController b;
            if ((p = GetComponent<PlayerController>()) != null) {
                Inventory.I.UpdateHealth(0f);
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
}
