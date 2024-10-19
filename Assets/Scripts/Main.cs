using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    static Main S;
    static private Dictionary<elementTypes, ElementDefinition> ELE_DICT;
    [Header("Inscribed")]
    public ElementDefinition[] elementDefinitions;
    public PlayerController player;
    // Start is called before the first frame update
    void Awake()
    {
        S = this;
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

    static public PlayerController GET_PLAYER() {
        return S.player;
    }
}
