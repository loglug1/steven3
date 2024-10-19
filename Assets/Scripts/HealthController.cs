using JetBrains.Annotations;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [Header("Inscribed")]
    public float maxHealth = 50f;
    
    [Header("Dynamic")]
    public float currHealth;

    void Awake() {
        currHealth = maxHealth;
    }
}
