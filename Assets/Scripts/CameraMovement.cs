using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Inscribed")]
    public float CameraIncrementX = 39.5f;
    public float CameraIncrementY = 22f;
    
    [Header("Dynamic")]
    public float camWidth;
    public float camHeight;

    void Awake()
    {
        camHeight=Camera.main.orthographicSize;
        camWidth=camHeight*Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
