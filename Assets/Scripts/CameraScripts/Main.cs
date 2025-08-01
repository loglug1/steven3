using System.Collections.Generic;
using SerializableDictionary.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static Main S;
    static public Color PlayerColor;
    static public Color MenuPlayerColor;
    static public string macho = "HAPPY";
    [SerializeField]
    private SerializableDictionary<string, ItemDefinition> ITEM_DICT;
    [SerializeField]
    private SerializableDictionary<string, List<PoolItem>> ITEM_POOLS;
    static private Dictionary<elementTypes, ElementDefinition> ELE_DICT;
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

    static public ItemDefinition GET_ITEM_DEFITION(string itemId) {
        if (S.ITEM_DICT.ContainsKey(itemId)) {
            return S.ITEM_DICT.Get(itemId);
        }
        Debug.LogError(itemId + " is not an item!");
        return null;
    }

    static public List<PoolItem> GET_ITEM_POOL(string pool) {
        if (S.ITEM_POOLS.ContainsKey(pool)) {
            return S.ITEM_POOLS.Get(pool);
        }
        Debug.LogError(pool + " is not an item pool!");
        return null;
    }
    static public ItemDefinition GET_RANDOM_ITEM(string pool) {
        List<PoolItem> poolItems = GET_ITEM_POOL(pool);
        if (poolItems == null) return null;

        List<string> items = new List<string>();
        foreach (PoolItem item in poolItems) {
            for (int i = 0; i < item.weight; i++) {
                items.Add(item.item);
            }
        }

        return GET_ITEM_DEFITION(items[Random.Range(0, items.Count)]);
    }

    static public GameObject GET_PLAYER() {
        return S.player;
    }

    static public void GameOver() {
        SceneManager.LoadScene("GameOverScreen");
    }

    
}
