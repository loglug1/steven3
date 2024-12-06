using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject slotChooserPrefab;
    [Header("Dynamic")]
    public weaponType     playerWeapon;
    public WandDefinition playerWandDef;
    private int            _jewels = 0;
    public TMP_Text       jewelText;
    public TMP_Text       HPText;
    static public         Inventory I;
    public elementTypes[] playerElements;
    public int playerWandLevel = 1;
    public Dictionary<elementTypes, int> playerElementLevels = new Dictionary<elementTypes, int>();
    
    // int bc not expected to ever have more than one wand, easy change to dict if ever changed
    public int jewels {
        set {_jewels = value; UpdateCurrency(); }
        get {return _jewels; }
    }
    void Awake()
    {
        I = this;

        // will be set by weapon choosing screen
    }

    public void UpdateCurrency() 
    {
        jewelText.SetText(jewels.ToString("00"));
    }
    public void UpdateHealth(float currentPlayerHealth) 
    {
        currentPlayerHealth = (int)currentPlayerHealth;
        if (HPText != null) HPText.SetText(currentPlayerHealth.ToString("00"));
        
    }

    public void elementLevelUp(elementTypes elementToLevel, int amount) 
    {
        // add desired level to desired element if exists, else add it to the dictionary
        if (playerElementLevels.ContainsKey(elementToLevel)) {
            playerElementLevels[elementToLevel] += amount;
            if (elementToLevel != elementTypes.None) {
                PlayerElementUI.UIhelper.UpdateElementUI();
            }
        }
        else {
            playerElementLevels.Add(elementToLevel, amount);
        }
        
    
    }
    public void wandLevelUp(int level) 
    {
        playerWandLevel += level;
    }

    public static void ObtainElementCrystal(ItemDefinition item) {
        // player already has it
        bool exists = false;
        for(int i = 0; i < Inventory.I.playerElements.Length; ++i) {
            if(Inventory.I.playerElements[i] == item.elementType) {
                Inventory.I.elementLevelUp(Inventory.I.playerElements[i], 1);
                exists = true;
                break;
            }
        }
        // player is purchasing as a new element for a wand with an empty slot
        if (!exists) {
            for (int i = 0; i < Inventory.I.playerElements.Length; ++i) {
                if(Inventory.I.playerElements[i] == elementTypes.None) {
                    Inventory.I.playerElements[i] = item.elementType;
                    exists = true;
                    weapon.w.UpdateColor(Inventory.I.playerElements);
                    Inventory.I.elementLevelUp(item.elementType, item.level);
                    break;
                }
            }
        }

        // since there is only one just replace and skip the popup
        if (I.playerElements.Length == 1) {
            ReplaceElement(item, 0);
            return;
        }

        // show popup to replace element
        if (!exists) {
            WandSlotCanvasController slotChooser = Instantiate(I.slotChooserPrefab).GetComponent<WandSlotCanvasController>();
            Debug.Log(slotChooser != null);
            for (int i = 0; i < Inventory.I.playerElements.Length; i++) {
                WandSlotButton button = slotChooser.AddButton();
                button.element = Main.GET_ELEMENT_DEFINITION(Inventory.I.playerElements[i]);
                button.slot = i;
                button.newElement = item;
                button.title.text = I.playerElementLevels[Inventory.I.playerElements[i]].ToString("00");
            }
        }
    }
    public static void ReplaceElement(ItemDefinition item, int slot) {
        Inventory.I.playerElements[slot] = item.elementType;
        weapon.w.UpdateColor(Inventory.I.playerElements);
        Inventory.I.elementLevelUp(item.elementType, item.level);
    }
    public void SetUpWandAtStart() 
    {
        playerWandDef = Main.GET_WAND_DEFINITION(playerWeapon);
        playerElements  = new elementTypes[playerWandDef.maxElementTypes];
        for (int i = 0; i < playerElements.Length; i++) {
            playerElements[i] = elementTypes.None;
            // Debug.Log(playerElements[i]);
        }  


        foreach(elementTypes ele in playerElements) {
            elementLevelUp(ele, 0);
        }
    }
}
