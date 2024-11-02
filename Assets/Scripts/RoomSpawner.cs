using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 -> needs bottom
    // 2 -> needs top
    // 3 -> needs left
    // 4 -> needs right

    void Update(){
        if(openingDirection == 1){
            // bottom door needed
        }else if(openingDirection == 2){
            // top door needed
        }else if(openingDirection == 3){
            // left door needed
        }else if(openingDirection == 4){
            // right door needed
        }
    }
}
