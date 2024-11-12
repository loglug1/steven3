using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject shopPopupPrefab;
    [Header("Dynamic")]
    public List<ItemDefinition> items;

    void Awake() {
        items.Add(GetRandomItem(Main.GET_ITEM_POOL(ItemType.potion)));
        items.Add(GetRandomItem(Main.GET_ITEM_POOL(ItemType.elementCrystal)));
        items.Add(GetRandomItem(Main.GET_ITEM_POOL(ItemType.weaponUpgrade)));
    }
    void OnTriggerEnter(Collider c) {
        //StartCoroutine(ShopGreeting("Want to see my wares?"));
        showShop();
    }

    IEnumerator ShopGreeting(string message) {
        Debug.Log(message);
        yield return new WaitForSecondsRealtime(3);
        showShop();
    }

    void showShop() {
        GameObject popup = Instantiate(shopPopupPrefab);
        ShopCanvasController canvasController = popup.GetComponent<ShopCanvasController>();
        if (canvasController != null) {
            canvasController.items[0].item = items[0];
            canvasController.items[1].item = items[1];
            canvasController.items[2].item = items[2];
        }
        
    }

    ItemDefinition GetRandomItem(List<ItemDefinition> itemPool) {
        return itemPool[Random.Range(0,itemPool.Count)];
    }
}
