using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                break;
            case ItemType.potion:
                switch(item.potionType) {
                    case PotionType.health:
                        player.GetComponent<HealthController>().Heal((float)item.level);
                        break;
                }
                break;
        }
    }
}
