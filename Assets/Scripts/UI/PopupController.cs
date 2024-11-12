using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    static PopupController S;

    void Awake() {
        S = this;
        Time.timeScale = 0;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ClosePopup();
        }
    }

    void OnDestroy() {
        S = null;
        Time.timeScale = 1;
    }

    static public void ClosePopup() {
        Destroy(S.gameObject);
    }
}
