using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public bool WALL; //is this a wall?
    public bool wallSpawned;
    public GameObject wallToSpawn;
    public bool topBottom; //is this wall top/bottom, then true

    void Update(){
        // if(wallSpawned == false && )
    }

    void OnTriggerEnter(Collider other){
        Debug.Log("triggered");
        if((other.gameObject.GetComponent<WallCheck>().WALL != WALL)&&(wallSpawned==false)){
            if(!topBottom){
                Instantiate(wallToSpawn, other.transform.position, Quaternion.Euler(0, 0, 0));
                wallSpawned=true;
            }else{
                Instantiate(wallToSpawn, other.transform.position, Quaternion.Euler(0, 0, 90));
                wallSpawned=true;
            }
        }
    }

}
