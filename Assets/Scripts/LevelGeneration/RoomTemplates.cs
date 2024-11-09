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
    void Update(){
        if(waitTime<=0 && spawnedBoss==false){
            for(int i = 0; i < rooms.Count; i++){
                if(i == rooms.Count-1){
                    Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    spawnedBoss = true;
                    if(rooms.Count < 8 || rooms.Count >35){
                        SceneManager.LoadScene("Procedural Generation Testing");
                    }else if(rooms.Count >= 8 && rooms.Count <=35){
                        acceptedRoomGen=true;
                        validateRooms();
                    }
                }
            }
        }else if (!spawnedBoss){
            waitTime -= Time.deltaTime;
        }
    }

    void validateRooms(){
        for(int i = 0; i < rooms.Count; i++){
            
        }
    }


}
