using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBossRoom : MonoBehaviour
{
    public bool playerEntered;
    public GameObject encasingWalls;
    void OnTriggerEnter()
    {
        if(!playerEntered){
            Instantiate(encasingWalls, transform.position, Quaternion.identity);
            playerEntered=true;
        }
    }
}
