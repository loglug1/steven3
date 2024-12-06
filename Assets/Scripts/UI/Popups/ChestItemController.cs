using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ChestItemController : MonoBehaviour
{
    public TMP_Text itemName;
    public TMP_Text flavorText;
    public TMP_Text description;
    public Image icon;
    private ItemDefinition _item;
    public ItemDefinition item {
        get { return _item; }
        set { SetItem(value); }
    }
    private void SetItem(ItemDefinition newItem) {
        _item = newItem;
        itemName.text = newItem.name;
        flavorText.text = newItem.flavorText;
        description.text = newItem.description;
        icon.sprite = newItem.icon;
    }
    public void ObtainItem() {
        PopupController.ClosePopup();
        ItemHandler.UseItem(item);
        if (chest.c != null) Destroy(chest.c.gameObject);
    }
}
