using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WandSlotButton : MonoBehaviour
{
    [Header("Inscribed")]
    public int slot;
    public ItemDefinition newElement;
    public TMP_Text title;
    public Image icon;
    private ElementDefinition _element;
    public ElementDefinition element {
        get { return _element; }
        set { SetElement(value); }
    }
    private void SetElement(ElementDefinition newElement) {
        _element = newElement;
        icon.sprite = newElement.sprite;
    }

    public void SelectSlot() {
        Debug.Log("Button Pressed");
        Inventory.ReplaceElement(newElement, slot);
        PopupController.ClosePopup();
    }
}
