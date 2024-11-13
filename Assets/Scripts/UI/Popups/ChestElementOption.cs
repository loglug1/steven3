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
        for(int i = 0; i < weapon.w.eleTypes.Length; ++i) {
            if(weapon.w.eleTypes[i] == chest.c.chosenElements[0]) {
                Inventory.I.elementLevelUp(weapon.w.eleTypes[i], 1);
                exists = true;
                break;
            }
        }

        // new element, open wand
        if (!exists) {
            for(int i = 0; i < weapon.w.eleTypes.Length; ++i) 
            {
                if(weapon.w.eleTypes[i] == elementTypes.None) 
                {
                    weapon.w.eleTypes[i] = chest.c.chosenElements[0];   // set player element to chosen
                    exists = true;
                    Time.timeScale = 1;
                    weapon.w.UpdateColor(weapon.w.eleTypes); // update color
                    Inventory.I.elementLevelUp(weapon.w.eleTypes[i], 1); // set to level 1
                    break;
                }
            }
        }
        // new element, non-open wand
        if (!exists) {
            int eleAmt = weapon.w.eleTypes.Length;
            weapon.w.eleTypes[eleAmt - 1] = chest.c.chosenElements[0];
            weapon.w.UpdateColor(weapon.w.eleTypes);
            Inventory.I.elementLevelUp(chest.c.chosenElements[0], 1);
        }
        Destroy(chest.c.gameObject);                        // destroy chest
        PopupController.ClosePopup(); // deactivate popup
    }
    static public void ChosenElementTwo()
    {
        bool exists = false;
        // existing element
        for(int i = 0; i < weapon.w.eleTypes.Length; ++i) {
            if(weapon.w.eleTypes[i] == chest.c.chosenElements[1]) {
                Inventory.I.elementLevelUp(weapon.w.eleTypes[i], 1);
                exists = true;
                break;
            }
        }

        // new element
        // new element, open wand
        if (!exists) {
            for(int i = 0; i < weapon.w.eleTypes.Length; ++i) 
            {
                if(weapon.w.eleTypes[i] == elementTypes.None) 
                {
                    weapon.w.eleTypes[i] = chest.c.chosenElements[1];   // set player element to chosen
                    exists = true;
                    Time.timeScale = 1;
                    weapon.w.UpdateColor(weapon.w.eleTypes); // update color
                    Inventory.I.elementLevelUp(weapon.w.eleTypes[i], 1); // set to level 1
                    break;
                }
            }
        }
        // new element, non-open wand
        if (!exists) {
            int eleAmt = weapon.w.eleTypes.Length;
            weapon.w.eleTypes[eleAmt - 1] = chest.c.chosenElements[1];
            weapon.w.UpdateColor(weapon.w.eleTypes);
            Inventory.I.elementLevelUp(chest.c.chosenElements[1], 1);
        }    
        Destroy(chest.c.gameObject);
        PopupController.ClosePopup(); // deactivate popup 
    }
}
