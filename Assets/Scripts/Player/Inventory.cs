using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public weaponType     playerWeapon;
    public int            jewels = 0;
    static public         Inventory I;
    public elementTypes[] playerElements;
    public Dictionary<elementTypes, int> playerElementLevels = new Dictionary<elementTypes, int>();

    // Start is called before the first frame update
    void Start()
    {
        I = this;
        playerWeapon    = weapon.w.type;
        playerElements  = weapon.w.eleTypes;

        foreach(elementTypes ele in playerElements) {
            elementLevelUp(ele, 0);
        }
    }

    public void elementLevelUp(elementTypes elementToLevel, int amount) {
        // add desired level to desired element if exists, else add it to the dictionary
        if (playerElementLevels.ContainsKey(elementToLevel)) {
            playerElementLevels[elementToLevel] += amount;
        }
        else {
            playerElementLevels.Add(elementToLevel, amount);
        }
        
    }
}
