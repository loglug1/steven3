using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyElement : MonoBehaviour
{
    public HealthController healthController;
    public EnemyController  enemyController;
    public EnemyDamage      enemyDamage;
    public SpriteRenderer         rm;
    public elementTypes     enemyElementofChoice;
    public elementTypes[]   enemyElementChance;
    public ElementDefinition    enemyEleDef;
    public float            strMult = 1.5f;
    public float            secondaryStrMult = 1.22f;
    public float            resMult = .80f;
    public float            secondaryResMult = .90f;

    // Start is called before the first frame update
    void Awake()
    {
        enemyElementofChoice = enemyElementChance[Random.Range(0, enemyElementChance.Length)];
        Transform son = transform.GetChild(1);
        Transform grandson = son.GetChild(0);
        rm = grandson.GetComponent<SpriteRenderer>();

        enemyEleDef = Main.GET_ELEMENT_DEFINITION(enemyElementofChoice);
        rm.color = enemyEleDef.color;
    }
}
