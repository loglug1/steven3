using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    potion,
    elementCrystal,
    weaponUpgrade,
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
        Debug.Log("Bought " + item.name);
        switch(item.type) {
            case ItemType.weaponUpgrade:
                Inventory.I.wandLevelUp(item.level);
                break;
            case ItemType.elementCrystal:
                // player already has it
                bool exists = false;
                for(int i = 0; i < Inventory.I.playerElements.Length; ++i) {
                    if(Inventory.I.playerElements[i] == item.elementType) {
                        Inventory.I.elementLevelUp(Inventory.I.playerElements[i], 1);
                        exists = true;
                        break;
                    }
                }
                // player is purchasing as a new element for a wand with an empty slot
                if (!exists) {
                    for (int i = 0; i < Inventory.I.playerElements.Length; ++i) {
                        if(Inventory.I.playerElements[i] == elementTypes.None) {
                            Inventory.I.playerElements[i] = item.elementType;
                            exists = true;
                            weapon.w.UpdateColor(Inventory.I.playerElements);
                            Inventory.I.elementLevelUp(item.elementType, item.level);
                            break;
                        }
                    }
                }
                // choose to replace last element
                if (!exists) {
                    int eleAmt = Inventory.I.playerElements.Length;
                    Inventory.I.playerElements[eleAmt - 1] = item.elementType;
                    weapon.w.UpdateColor(Inventory.I.playerElements);
                    Inventory.I.elementLevelUp(item.elementType, item.level);
                }
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
