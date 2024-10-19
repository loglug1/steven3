using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    static private Dictionary<elementTypes, ElementDefinition> ELE_DICT;
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
