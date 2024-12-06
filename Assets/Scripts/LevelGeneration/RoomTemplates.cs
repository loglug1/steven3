using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject closedRoom;
    public List<GameObject> rooms;
    public float waitTime;
    public bool spawnedBoss;
    public GameObject boss;
    public bool acceptedRoomGen;
    //public int prevRoomCount;
    void Update(){
        if(waitTime<=0 && spawnedBoss==false){
            for(int i = 0; i < rooms.Count; i++){
                if(i == rooms.Count-1){
                    Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    spawnedBoss = true;
                    if(rooms.Count < 8 || rooms.Count >35){
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }else if(rooms.Count >= 8 && rooms.Count <=35){
                        acceptedRoomGen=true;
                        validateRooms();
                    }
                }
            }
        }else if (!spawnedBoss){ 
            waitTime -= Time.deltaTime;
        }
        // }else if (prevRooms == rooms.Count){
        //     waitTime = -1; // if at any point the rooms.Count has not updated, this means the 
        // }
        //prevRoomCount = rooms.Count; //this prob wont work, we need to find the invoked function
        //update prevRoom number
    }

    void validateRooms(){
        for(int i = 0; i < rooms.Count-1; i++){
            List<GameObject> rP = rooms[i].GetComponent<RoomData>().roomValidationPrefabs;
            if(rP.Count==0){
                //hello
            }else{
                Instantiate(rP[Random.Range(0,rP.Count)], rooms[i].transform.position, Quaternion.identity);
            }
        }
    }


}
