using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public GameObject chestObject;
    static public void ChosenElementOne() 
    {
        for(int i = 0; i < weapon.w.eleTypes.Length; ++i) 
        {
            if(weapon.w.eleTypes[i] == elementTypes.None) 
            {
                weapon.w.eleTypes[i] = chest.c.chosenElements[0];
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
                break;
            }
        }       
    }
}
