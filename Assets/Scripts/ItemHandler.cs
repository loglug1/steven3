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
    public float level;
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
        Debug.Log("Bought " + item.name);
    }
}
