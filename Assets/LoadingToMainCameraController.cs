using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingToMainCameraController : MonoBehaviour
{
    public Camera loadingCamera;
    public Camera mainCamera;
    public GameObject roomTemplatesGO;
    public GameObject player;
    
    void Start()
    {
        loadingCamera.enabled=true;
        mainCamera.enabled=false;
        player.GetComponent<Rigidbody>().isKinematic=true;
    }

    void Update()
    {
        if(roomTemplatesGO.GetComponent<RoomTemplates>().acceptedRoomGen){
            loadingCamera.enabled=false;
            mainCamera.enabled=true;
            player.GetComponent<Rigidbody>().isKinematic=false;
        }
    }
}
