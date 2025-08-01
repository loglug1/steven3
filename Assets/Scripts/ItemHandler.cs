using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ItemType {
    potion,
    elementCrystal,
    weaponUpgrade,
    wand,
}
public enum PotionType {
    none,
    health,
}
[System.Serializable]
public class PoolItem {
    public string item;
    [Tooltip("Number of Times to Add Item to Specific Pool")]
    public int weight;
    public ItemDefinition GetItem() {
        return Main.GET_ITEM_DEFITION(item);
    }
}
[System.Serializable]
public class ItemDefinition {
    public ItemType type;
    public elementTypes elementType;
    public PotionType potionType;
    public weaponType wandType;
    public int level;
    public string name;
    public string flavorText;
    [TextArea(0,2)]
    public string description;
    public Sprite icon;
    public int price;
}
public class ItemHandler : MonoBehaviour
{
    static public void UseItem(ItemDefinition item) {
        GameObject player = Main.GET_PLAYER();
        //Debug.Log("Bought " + item.name);
        switch(item.type) {
            case ItemType.weaponUpgrade:
                Inventory.I.wandLevelUp(item.level);
                break;
            case ItemType.elementCrystal:
                //add element
                Inventory.ObtainElementCrystal(item);
                AudioController.PlayClip("obtain-element");
                break;
            case ItemType.potion:
                switch(item.potionType) {
                    case PotionType.health:
                        player.GetComponent<HealthController>().Heal((float)item.level);
                        break;
                }
                AudioController.PlayClip("drink-potion");
                break;
            case ItemType.wand:
                // used for wand selector at start, potentially use for buying wand from shop later
                // checks scene since same logic is used in main menu wand save, else logic will be unused for now unless we add buying from shop
                Scene scene = SceneManager.GetActiveScene();
                if (scene.name == "StartScene")
                {
                    PlayerPrefs.SetString("PlayerWeapon", item.wandType.ToString());
                    // Debug.Log((weaponType)System.Enum.Parse(typeof(weaponType), PlayerPrefs.GetString("PlayerWeapon")));
                }
                else
                {
                    Inventory.I.playerWeapon = item.wandType;
                    Inventory.I.SetUpWandAtStart();
                }
                break;
        }
    }
}
