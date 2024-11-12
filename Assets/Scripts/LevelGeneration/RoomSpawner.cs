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

    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;
    public bool boundBox;

    void Start(){
        //Debug.Log("Started.");
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.5f);
    }

    void Spawn(){
        //Debug.Log("Spawn invoked.");
        if(spawned==false){
            if(openingDirection == 1){
                rand = Random.Range(0,templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                // bottom door needed
            }else if(openingDirection == 2){
                rand = Random.Range(0,templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                // top door needed
            }else if(openingDirection == 3){
                rand = Random.Range(0,templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                // left door needed
            }else if(openingDirection == 4){
                rand = Random.Range(0,templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                // right door needed
            }else if (openingDirection == 5){
                return; //for bounding box
            }
            spawned = true;
        }
    }

    void OnTriggerEnter(Collider other){
        if(boundBox==true){
            return;
        }
        //Debug.Log("Triggered");
        if(other.CompareTag("SpawnPoint")){
            if(other.GetComponent<RoomSpawner>().spawned == false && spawned == false){
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Debug.Log("Destroyed");
            }
            spawned = true;
            // Debug.Log("Destroyed");
            // Destroy(gameObject);
        }
    }
}
