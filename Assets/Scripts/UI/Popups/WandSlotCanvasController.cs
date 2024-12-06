using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WandSlotCanvasController : MonoBehaviour
{
    public GameObject wandSlotButtonPrefab;
    public GameObject background;
    private List<WandSlotButton> buttons = new List<WandSlotButton>();
    public WandSlotButton AddButton() {
        WandSlotButton b = Instantiate(wandSlotButtonPrefab, transform).GetComponent<WandSlotButton>();
        buttons.Add(b);
        CenterButtons();
        return b;
    }
    private void CenterButtons() {
        float width = background.GetComponent<RectTransform>().sizeDelta.x;
        float space = width / (buttons.Count + 1);
        for(int i = 0; i < buttons.Count; i++) {
            Vector3 pos = buttons[i].GetComponent<RectTransform>().localPosition;
            pos.x = -(width/2f) + ((i+1) * space);
            buttons[i].GetComponent<RectTransform>().localPosition = pos;
        }
    }
}
