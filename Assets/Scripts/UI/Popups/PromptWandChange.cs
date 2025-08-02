using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptWandChange : MonoBehaviour
{
    public GameObject wandPickerPopupPrefab;
    List<PoolItem> wands;
    // void Awake()
    // {
    //     Popup();
    // }
    public void Popup()
    {
        GameObject popup = Instantiate(wandPickerPopupPrefab);
        wands = Main.GET_ITEM_POOL("pool_weapon_choice");
        ChestCanvasController canvasController = popup.GetComponent<ChestCanvasController>();
        if (canvasController != null) {
            for (int i = 0; i < wands.Count; i++)
            {
                canvasController.items[i].item = wands[i].GetItem();
                
            }

        }
    }
}
