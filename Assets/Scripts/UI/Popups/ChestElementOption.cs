using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used for choosing elements from chest and managing chest pop up, plus destroying chest upon element pickup
public class ChestElementOption : MonoBehaviour
{
    static public void ChosenElementOne() 
    {
        bool exists = false;
        // existing element, level up!
        for(int i = 0; i < Inventory.I.playerElements.Length; ++i) {
            if(Inventory.I.playerElements[i] == chest.c.chosenElements[0]) {
                Inventory.I.elementLevelUp(Inventory.I.playerElements[i], 1);
                exists = true;
                break;
            }
        }

        // new element, open wand
        if (!exists) {
            for(int i = 0; i < Inventory.I.playerElements.Length; ++i) 
            {
                if(Inventory.I.playerElements[i] == elementTypes.None) 
                {
                    Inventory.I.playerElements[i] = chest.c.chosenElements[0];   // set player element to chosen
                    exists = true;
                    Time.timeScale = 1;
                    weapon.w.UpdateColor(Inventory.I.playerElements); // update color
                    Inventory.I.elementLevelUp(Inventory.I.playerElements[i], 1); // set to level 1
                    break;
                }
            }
        }
        // new element, non-open wand
        if (!exists) {
            int eleAmt = Inventory.I.playerElements.Length;
            Inventory.I.playerElements[eleAmt - 1] = chest.c.chosenElements[0];
            weapon.w.UpdateColor(Inventory.I.playerElements);
            Inventory.I.elementLevelUp(chest.c.chosenElements[0], 1);
        }
        Destroy(chest.c.gameObject);                        // destroy chest
        PopupController.ClosePopup(); // deactivate popup
    }
    static public void ChosenElementTwo()
    {
        bool exists = false;
        // existing element
        for(int i = 0; i < Inventory.I.playerElements.Length; ++i) {
            if(Inventory.I.playerElements[i] == chest.c.chosenElements[1]) {
                Inventory.I.elementLevelUp(Inventory.I.playerElements[i], 1);
                exists = true;
                break;
            }
        }

        // new element
        // new element, open wand
        if (!exists) {
            for(int i = 0; i < Inventory.I.playerElements.Length; ++i) 
            {
                if(Inventory.I.playerElements[i] == elementTypes.None) 
                {
                    Inventory.I.playerElements[i] = chest.c.chosenElements[1];   // set player element to chosen
                    exists = true;
                    Time.timeScale = 1;
                    weapon.w.UpdateColor(Inventory.I.playerElements); // update color
                    Inventory.I.elementLevelUp(Inventory.I.playerElements[i], 1); // set to level 1
                    break;
                }
            }
        }
        // new element, non-open wand
        if (!exists) {
            int eleAmt = Inventory.I.playerElements.Length;
            Inventory.I.playerElements[eleAmt - 1] = chest.c.chosenElements[1];
            weapon.w.UpdateColor(Inventory.I.playerElements);
            Inventory.I.elementLevelUp(chest.c.chosenElements[1], 1);
        }    
        Destroy(chest.c.gameObject);
        PopupController.ClosePopup(); // deactivate popup 
    }
}
