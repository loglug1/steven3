using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum elementTypes {
    None,
    Fire,
    Water,
    Grass
}
public enum Colors {
    white,
    orange,
    blue,
    green
}

[System.Serializable]
public class ElementDefinition {
    public elementTypes element             = elementTypes.None;
    public GameObject projectilePrefab;
    public float        damageOnHit         = 0;
    public float        damagePerSec        = 0;
    public float        delayBetweenShots   = 0;
    public float        velocity            = 50;
    public Colors       color;
    public string       name;
    
}

public class Main : MonoBehaviour
{
    static public Dictionary<elementTypes, ElementDefinition> ELE_DICT;
    public ElementDefinition[] elementDefinitions;
    // Start is called before the first frame update
    void Awake()
    {
        ELE_DICT = new Dictionary<elementTypes, ElementDefinition>();
        foreach(ElementDefinition def in elementDefinitions) {
            ELE_DICT[def.element]=def;
        }        
    }
    static public ElementDefinition GET_ELEMENT_DEFINITION(elementTypes et) {
        if (ELE_DICT.ContainsKey(et)) {
            return(ELE_DICT[et]);
        }
        return(new ElementDefinition());
    }
}
