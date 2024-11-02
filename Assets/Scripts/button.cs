using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used for choosing elements from chest and managing chest pop up, plus destroying chest upon element pickup
public class button : MonoBehaviour
{
    public GameObject chestObject;
    static public void ChosenElementOne() 
    {
        for(int i = 0; i < weapon.w.eleTypes.Length; ++i) 
        {
            if(weapon.w.eleTypes[i] == elementTypes.None) 
            {
                weapon.w.eleTypes[i] = chest.c.chosenElements[0];   // set player element to chosen
                chest.c.uiPopup.SetActive(false);                   // deactivate popup
                Destroy(chest.c.gameObject);                        // destroy chest
                Time.timeScale = 1;
                weapon.w.UpdateColor(Main.GET_ELEMENT_DEFINITION(weapon.w.eleTypes[0]), Main.GET_ELEMENT_DEFINITION(weapon.w.eleTypes[1])); // update color
                break;
            }
        }
    }
    static public void ChosenElementTwo()
    {
        for(int i = 0; i < weapon.w.eleTypes.Length; ++i) 
        {
            if(weapon.w.eleTypes[i] == elementTypes.None) 
            {
                weapon.w.eleTypes[i] = chest.c.chosenElements[1];
                chest.c.uiPopup.SetActive(false);
                Destroy(chest.c.gameObject);
                Time.timeScale = 1;
                weapon.w.UpdateColor(Main.GET_ELEMENT_DEFINITION(weapon.w.eleTypes[0]), Main.GET_ELEMENT_DEFINITION(weapon.w.eleTypes[1])); // update color
                break;
            }
        }       
    }
}
