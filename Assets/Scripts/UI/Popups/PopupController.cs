using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    static Stack<PopupController> S;
    public bool isClosable = true;

    void Awake() {
        if (S == null) S = new Stack<PopupController>();
        S.Push(this);
        Time.timeScale = 0;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && isClosable) {
            ClosePopup();
        }
    }

    void OnDestroy() {
        if (S.Count < 1) {
            Time.timeScale = 1;
        }
    }

    static public void ClosePopup() {
        Destroy(S.Pop().gameObject);
    }
}
