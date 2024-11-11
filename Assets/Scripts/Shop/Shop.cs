using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject messagePrefab;
    void OnTriggerEnter(Collider c) {
        StartCoroutine(ShopGreeting("Want to see my wares?"));
    }

    IEnumerator ShopGreeting(string message) {
        Debug.Log(message);
        yield return new WaitForSecondsRealtime(3);
        showShop();
    }

    void showShop() {

    }
}
