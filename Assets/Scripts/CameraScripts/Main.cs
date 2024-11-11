using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ItemType {
    potion,
    crystal,
    weapon,
}
public enum PotionType {
    health,
}
[System.Serializable]
public class ItemDefinition {
    public ItemType type;
    public elementTypes elementType;
    public PotionType potionType;
    public weaponType weaponType;
    public float level;
    public string name;
    public string description;
    public Sprite icon;
    public int price;
}
public class Main : MonoBehaviour
{
    static Main S;
    static public Color PlayerColor = new Color(255/255f,147/255f,255/255f,255/255f);
    static public Color MenuPlayerColor;
    static public string macho = "HAPPY";
    static private Dictionary<elementTypes, ElementDefinition> ELE_DICT;
    static private Dictionary<ItemType, List<ItemDefinition>> ITEM_DICT;
    [Header("Inscribed")]
    public ElementDefinition[] elementDefinitions;
    public ItemDefinition[] items;
    public GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        S = this;
        ELE_DICT = new Dictionary<elementTypes, ElementDefinition>();
        foreach(ElementDefinition def in elementDefinitions) {
            ELE_DICT[def.element]=def;
        }        
        foreach (ItemDefinition item in items) {
            ITEM_DICT[item.type].Add(item);
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
