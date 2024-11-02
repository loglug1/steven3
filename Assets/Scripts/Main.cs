using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    static Main S;
    static public Color PlayerColor = new Color(255/255f,147/255f,255/255f,255/255f);
    static public Color MenuPlayerColor;
    static public string macho = "HAPPY";
    static private Dictionary<elementTypes, ElementDefinition> ELE_DICT;
    [Header("Inscribed")]
    public ElementDefinition[] elementDefinitions;
    public GameObject player;
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

    static public GameObject GET_PLAYER() {
        return S.player;
    }

    static public void GameOver() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
