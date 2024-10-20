using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyElement : MonoBehaviour
{
    public HealthController healthController;
    public EnemyController  enemyController;
    public EnemyDamage      enemyDamage;
    public elementTypes     enemyElementofChoice;
    public elementTypes[]   enemyElementChance;
    public float            strMult = 1.3f;
    public float            secondaryStrMult = 1.2f;
    public float            resMult = .80f;
    public float            secondaryResMult = .90f;

    // Start is called before the first frame update
    void Awake()
    {
        enemyElementofChoice = enemyElementChance[Random.Range(0, enemyElementChance.Length - 1)];
    }
}
