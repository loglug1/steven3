using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public weaponType playerWeapon;
    public int        jewels = 0;
    static public Inventory I;

    // Start is called before the first frame update
    void Start()
    {
        I = this;
        playerWeapon = weapon.w.type;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
