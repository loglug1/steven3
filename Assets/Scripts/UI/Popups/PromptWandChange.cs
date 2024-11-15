using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptWandChange : MonoBehaviour
{
    public GameObject wandPickerPopupPrefab;
    List<ItemDefinition> wands;
    void Awake() {
        GameObject popup = Instantiate(wandPickerPopupPrefab);
        wands = Main.GET_ITEM_POOL(ItemType.wand);
        ShopCanvasController canvasController = popup.GetComponent<ShopCanvasController>();
        if (canvasController != null) {
            canvasController.items[0].item = wands[0];
            canvasController.items[1].item = wands[1];
            canvasController.items[2].item = wands[2];
        }
    }
}
