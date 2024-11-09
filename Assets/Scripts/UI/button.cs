using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used for choosing elements from chest and managing chest pop up, plus destroying chest upon element pickup
public class button : MonoBehaviour
{
    public GameObject chestObject;
    static public void ChosenElementOne() 
    {
        // existing element, level up!
        for(int i = 0; i < weapon.w.eleTypes.Length; ++i) {
            if(weapon.w.eleTypes[i] == chest.c.chosenElements[0]) {
                Inventory.I.elementLevelUp(weapon.w.eleTypes[i], 1);
                break;
            }
        }

        // new element
        for(int i = 0; i < weapon.w.eleTypes.Length; ++i) 
        {
            if(weapon.w.eleTypes[i] == elementTypes.None) 
            {
                weapon.w.eleTypes[i] = chest.c.chosenElements[0];   // set player element to chosen
                chest.c.uiPopup.SetActive(false);                   // deactivate popup
                Destroy(chest.c.gameObject);                        // destroy chest
                Time.timeScale = 1;
                weapon.w.UpdateColor(weapon.w.eleTypes); // update color
                Inventory.I.elementLevelUp(weapon.w.eleTypes[i], 1); // set to level 1
                break;
            }
        }
    }
    static public void ChosenElementTwo()
    {
        // existing element
        for(int i = 0; i < weapon.w.eleTypes.Length; ++i) {
            if(weapon.w.eleTypes[i] == chest.c.chosenElements[1]) {
                Inventory.I.elementLevelUp(weapon.w.eleTypes[i], 1);
                break;
            }
        }

        // new element
        for(int i = 0; i < weapon.w.eleTypes.Length; ++i) 
        {
            if(weapon.w.eleTypes[i] == elementTypes.None) 
            {
                weapon.w.eleTypes[i] = chest.c.chosenElements[1];
                chest.c.uiPopup.SetActive(false);
                Destroy(chest.c.gameObject);
                Time.timeScale = 1;
                weapon.w.UpdateColor(weapon.w.eleTypes); // update color
                Inventory.I.elementLevelUp(weapon.w.eleTypes[i], 1); // set to level 1
                break;
            }
        }       
    }
}
