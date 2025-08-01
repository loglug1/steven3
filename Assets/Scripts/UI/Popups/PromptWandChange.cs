using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptWandChange : MonoBehaviour
{
    public GameObject wandPickerPopupPrefab;
    List<ItemDefinition> wands;
    // void Awake()
    // {
    //     Popup();
    // }
    public void Popup()
    {
        GameObject popup = Instantiate(wandPickerPopupPrefab);
        wands = Main.GET_ITEM_POOL(ItemPool.weaponChoice);
        ChestCanvasController canvasController = popup.GetComponent<ChestCanvasController>();
        if (canvasController != null)
        {
            canvasController.items[0].item = wands[0];
            canvasController.items[1].item = wands[1];
            canvasController.items[2].item = wands[2];
        }
    }
}
