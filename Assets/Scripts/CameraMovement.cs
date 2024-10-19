using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Inscribed")]
    public float cameraIncrementX = 39.5f;
    public float cameraIncrementY = 22f;
    public GameObject player;
    
    [Header("Dynamic")]
    public float camWidth;
    public float camHeight;
    public Vector3 camPosition;//gameObject.transform.position
    public float radiusX;
    public float radiusY;
    public Vector3 playerPos;

    void Awake()
    {
        camHeight=Camera.main.orthographicSize;
        camWidth=camHeight*Camera.main.aspect;
        camPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        if(playerPos.x > camPosition.x + camWidth){
            camPosition.x += cameraIncrementX;
            transform.position = camPosition;
            Debug.Log("Player Left Screen Right");
        }
        if(playerPos.x < camPosition.x - camWidth){
            camPosition.x -= cameraIncrementX;
            transform.position = camPosition;
            Debug.Log("Player Left Screen Left");
        }
        if(playerPos.y > camPosition.y + camHeight){
            camPosition.y += cameraIncrementY;
            transform.position = camPosition;
            Debug.Log("Player Left Screen Up");
        }
        if(playerPos.y < camPosition.y - camHeight){
            camPosition.y -= cameraIncrementY;
            transform.position = camPosition;
            Debug.Log("Player Left Screen Down");
        }
    }
}