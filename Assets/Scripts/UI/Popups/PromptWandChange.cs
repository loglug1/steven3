using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptWandChange : MonoBehaviour
{
    public GameObject wandPickerPopupPrefab;
    List<PoolItem> wands;
    void Awake() {
        GameObject popup = Instantiate(wandPickerPopupPrefab);
        wands = Main.GET_ITEM_POOL("pool_weapon_choice");
        ChestCanvasController canvasController = popup.GetComponent<ChestCanvasController>();
        if (canvasController != null) {
            canvasController.items[0].item = wands[0].GetItem();
            canvasController.items[1].item = wands[1].GetItem();
            canvasController.items[2].item = wands[2].GetItem();
        }
    }
}
