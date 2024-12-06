using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used for choosing elements from chest and managing chest pop up, plus destroying chest upon element pickup
public class ChestElementOption : MonoBehaviour
{
    static public void ChosenElementOne() 
    {
        Inventory.ObtainElementCrystal(chest.c.items[0]);
        PopupController.ClosePopup(); // deactivate popup
        Destroy(chest.c.gameObject); // destroy chest
    }
    static public void ChosenElementTwo()
    {
        Inventory.ObtainElementCrystal(chest.c.items[1]);
        PopupController.ClosePopup(); // deactivate popup
        Destroy(chest.c.gameObject); // destroy chest
    }
}