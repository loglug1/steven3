using UnityEngine;

public class HealthController : MonoBehaviour
{
    [Header("Inscribed")]
    public float maxHealth = 50f;
    public HealthBarController healthBar;
    public float currHealth = 50f;
    
    // [Header("Dynamic")]

    void Awake() {
        healthBar.maxValue = maxHealth;
        healthBar.value = currHealth;
    }

    public void Damage(float amount) {
        currHealth -= amount;
        healthBar.value = currHealth;
        if (currHealth <= 0) {
            if (GetComponent<PlayerController>() != null) {
                Main.GameOver();
            }
        }
    }
    public void Heal(float amount) {
        currHealth += amount;
        healthBar.value = currHealth;
    }

    public void OnTriggerStay(Collider c) {
        GameObject otherGO = c.gameObject;
        if (otherGO.layer == LayerMask.NameToLayer("EnvironmentalHazards")) {
            EnvironmentalHazard eH = otherGO.GetComponent<EnvironmentalHazard>();
            if (eH != null) {
                Damage(Time.deltaTime * eH.damage);
            }
        }
    }
}
