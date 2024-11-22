using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class PlayerElementUI : MonoBehaviour
{
    private elementTypes[] playerElementComparison;
    static public PlayerElementUI UIhelper;
    void Start() 
    {
        playerElementComparison = new elementTypes[Inventory.I.playerElements.Length];
        UIhelper = this;
    }
    void FixedUpdate() 
    {
        if (Inventory.I.playerElements.Length != playerElementComparison.Length) {
            playerElementComparison = new elementTypes[Inventory.I.playerElements.Length];
        }
        if (!Enumerable.Equals(playerElementComparison, Inventory.I.playerElements)) {
            UpdateElementUI();
        }
    }
    public void UpdateElementUI() {
        
        Transform parent = this.transform;
        for (int i = 0; i < Inventory.I.playerElements.Length; i++) {
            // picture and color stuff
            Transform slot = parent.GetChild(i);           // slot = box that holds element
            GameObject slotActive = slot.gameObject;
            slotActive.SetActive(true);                    // sets as many slots as wand has as active

            // item = element within that box (child)
            GameObject item = slot.GetChild(0).gameObject;

            // text stuff (grandchild)
            Transform useThisForText = slot.GetChild(0);   // temp transform used to get text child of element
            GameObject tempText = useThisForText.GetChild(0).gameObject; // actual gameobject of the text, used for setting it active later

            item.GetComponent<Image>().sprite = Main.GET_ELEMENT_DEFINITION(Inventory.I.playerElements[i]).sprite; // set sprite to correct ele sprite

            // color setter
            Color col = item.GetComponent<Image>().color;
            col.a = 1f; // not transparent
            item.GetComponent<Image>().color = col;

            // text setter
            TMP_Text levelText = tempText.GetComponent<TMP_Text>();
            levelText.text = Inventory.I.playerElementLevels[Inventory.I.playerElements[i]].ToString("00");
            tempText.SetActive(true); // display level
        }
        Inventory.I.playerElements.CopyTo(playerElementComparison , 0);
        
    }
}
