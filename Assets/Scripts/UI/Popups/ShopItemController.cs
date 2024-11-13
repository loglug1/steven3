using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ShopItemController : MonoBehaviour
{
    public TMP_Text itemName;
    public TMP_Text flavorText;
    public TMP_Text description;
    public TMP_Text price;
    public Image icon;
    private ItemDefinition _item;
    private bool canAfford = true;
    private bool soldOut = false;
    public ItemDefinition item {
        get { return _item; }
        set { SetItem(value); }
    }
    private void SetItem(ItemDefinition newItem) {
        _item = newItem;
        itemName.text = newItem.name;
        flavorText.text = newItem.flavorText;
        description.text = newItem.description;
        canAfford = newItem.price <= Inventory.I.jewels;
        price.text = newItem.price.ToString();
        if (newItem.type == ItemType.weaponUpgrade) {
            soldOut = Inventory.I.playerWandLevel >= Main.GET_WAND_DEFINITION(Inventory.I.playerWeapon).maxWandLevel; // don't allow buying a weapon upgrade if the wand is max level
        }
        if (canAfford) {
            price.color = Color.white;
        } else {
            price.color = Color.red;
        }
        if (soldOut) {
            icon.color = new Color(0.1f, 0.1f, 0.1f);
            price.text = "Sold out!";
        }
        icon.sprite = newItem.icon;
    }
    public void BuyItem() {
        if (canAfford && !soldOut) {
            Inventory.I.jewels -= item.price;
            ItemHandler.UseItem(item);
            PopupController.ClosePopup();
        }
    }
}
