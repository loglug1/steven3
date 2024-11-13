using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used for choosing elements from chest and managing chest pop up, plus destroying chest upon element pickup
public class ChestElementOption : MonoBehaviour
{
    static public void ChosenElementOne() 
    {
        Inventory.ObtainElementCrystal(new ItemDefinition{type = ItemType.elementCrystal, elementType = chest.c.chosenElements[0], level = 1});
        PopupController.ClosePopup(); // deactivate popup
        Destroy(chest.c.gameObject);                        // destroy chest
    }
    static public void ChosenElementTwo()
    {
        Inventory.ObtainElementCrystal(new ItemDefinition{type = ItemType.elementCrystal, elementType = chest.c.chosenElements[1], level = 1});
        PopupController.ClosePopup(); // deactivate popup
        Destroy(chest.c.gameObject);                        // destroy chest
    }
}
