using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    static Main S;
    static public Color PlayerColor;
    static public Color MenuPlayerColor;
    static public string macho = "HAPPY";
    static private Dictionary<elementTypes, ElementDefinition> ELE_DICT;
    static private Dictionary<ItemPool, List<ItemDefinition>> ITEM_DICT;
    static private Dictionary<weaponType, WandDefinition> WAND_DICT;
    [Header("Inscribed")]
    public ElementDefinition[] elementDefinitions; //Definition Structure defined in Elemental/ElementHandler.cs
    public WandDefinition[]    wandDefinitions; // Definition Structure defined in weapon/Weapon.cs
    public ItemDefinition[] itemDefinitions; //Definition Structure defined in ItemHandler.cs
    public GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        S = this;
        // if a player has chosen a color, sets it to that otherwise default
        PlayerColor = PlayerPrefs.HasKey("PlayerColorR") ?
                      new Color(PlayerPrefs.GetFloat("PlayerColorR"), PlayerPrefs.GetFloat("PlayerColorG"), PlayerPrefs.GetFloat("PlayerColorB"), 1) 
                      : new Color(255 / 255f, 147 / 255f, 255 / 255f, 255 / 255f);
        // ele defs
        ELE_DICT = new Dictionary<elementTypes, ElementDefinition>();
        foreach(ElementDefinition def in elementDefinitions) {
            ELE_DICT[def.element]=def;
        }
        // item defs
        ITEM_DICT = new Dictionary<ItemPool, List<ItemDefinition>>();
        foreach (ItemDefinition item in itemDefinitions) {
            foreach (WeightedItemPool itemPool in item.itemPools) {
                //create pool list in dictionary if it doesn't exist
                if (!ITEM_DICT.ContainsKey(itemPool.pool)) {
                    ITEM_DICT[itemPool.pool] = new List<ItemDefinition>();
                }
                //add item to pool (weight) times
                for (int i = 0; i < itemPool.weight; i++) {
                    ITEM_DICT[itemPool.pool].Add(item);
                }
            }
        }
        // wand defs
        WAND_DICT = new Dictionary<weaponType, WandDefinition>();
        foreach(WandDefinition def in wandDefinitions) {
            WAND_DICT[def.wandType]=def;
        }
    }
    static public ElementDefinition GET_ELEMENT_DEFINITION(elementTypes et) {
        if (ELE_DICT.ContainsKey(et)) {
            return(ELE_DICT[et]);
        }
        return(new ElementDefinition());
    }
    static public WandDefinition GET_WAND_DEFINITION(weaponType wt) {
        if (WAND_DICT.ContainsKey(wt)) {
            return WAND_DICT[wt];
        }
        return(new WandDefinition());
    }

    static public List<ItemDefinition> GET_ITEM_POOL(ItemPool pool) {
        if (ITEM_DICT.ContainsKey(pool)) {
            return ITEM_DICT[pool];
        }
        return null;
    }
    static public ItemDefinition GET_RANDOM_ITEM(ItemPool pool) {
        List<ItemDefinition> items;
        if (ITEM_DICT.ContainsKey(pool)) {
            items = ITEM_DICT[pool];
            return items[Random.Range(0,items.Count)];
        }
        return null;
    }

    static public GameObject GET_PLAYER() {
        return S.player;
    }

    static public void GameOver() {
        SceneManager.LoadScene("GameOverScreen");
    }

    
}
