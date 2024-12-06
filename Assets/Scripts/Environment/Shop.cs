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
        items.Add(Main.GET_RANDOM_ITEM(ItemPool.shopPotions));
        items.Add(Main.GET_RANDOM_ITEM(ItemPool.shopElements));
        items.Add(Main.GET_RANDOM_ITEM(ItemPool.shopWeaponUpgrades));
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
}
