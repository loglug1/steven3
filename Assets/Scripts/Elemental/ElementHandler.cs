using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum elementTypes {
    None,
    Fire,
    Water,
    Grass,
    Ice,
    Poison,
    Rock
}
[System.Serializable]
public class ElementDefinition {
    public elementTypes element             = elementTypes.None;
    public GameObject projectilePrefab;
    public float        damageOnHit         = 0;
    public float        damagePerSec        = 0;
    public float        delayBetweenShots   = 0;
    public float        velocity            = 50;
    public Color       color;
    public string       name;
    public Sprite       sprite;
    public elementTypes weakElement         = elementTypes.None;
    public elementTypes strElement          = elementTypes.None;
    public float          effectDuration;
    
}

[RequireComponent(typeof(SpriteController))]
public class ElementHandler : MonoBehaviour
{
    public SpriteRenderer   rm;                         // material to access for enemy color
    public elementTypes     enemyElementofChoice;       // randomly chosen element for enemy
    public elementTypes[]   enemyElementChance;         // stores possible elements this enemy could be
    public ElementDefinition    enemyEleDef;            // used to get info from definiton
    public float            strMult = 1.5f;             // multiplier if strong element
    public float            secondaryStrMult = 1.22f;   // multiplier if 2nd chosen and strong
    public float            resMult = .80f;             // multiplier if resistant
    public float            secondaryResMult = .90f;    // multiplier if 2nd chosen and resistant
    public SpriteController spriteController;
    static public int enemyLevel;

    void Awake() {
        spriteController = GetComponent<SpriteController>();
    }

    // basic slime enemy element
    public void BasicEnemySpawn()
    {
        // random set
        enemyElementofChoice = enemyElementChance[Random.Range(0, enemyElementChance.Length)];
        // // material is part of grandson for enemy
        // Transform son = transform.GetChild(1);
        // Transform grandson = son.GetChild(0);
        // rm = grandson.GetComponent<SpriteRenderer>();
        enemyEleDef = Main.GET_ELEMENT_DEFINITION(enemyElementofChoice);
        spriteController.color = enemyEleDef.color;
        // // change color based on element for enemy
        // rm.color = enemyEleDef.color;
    }

    public void SetEnemyElement(elementTypes element)
    {
        // random set
        enemyElementofChoice = element;
        // // material is part of grandson for enemy
        // Transform son = transform.GetChild(1);
        // Transform grandson = son.GetChild(0);
        // rm = grandson.GetComponent<SpriteRenderer>();
        enemyEleDef = Main.GET_ELEMENT_DEFINITION(enemyElementofChoice);
        spriteController.color = enemyEleDef.color;
        // // change color based on element for enemy
        // rm.color = enemyEleDef.color;
    }
    

}
