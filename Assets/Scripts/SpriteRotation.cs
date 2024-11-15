using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotation : MonoBehaviour
{
    public float degreesPerSecond = 180.0f;
    public float time; //
    void Update()
    {
        transform.Rotate(0, 0, degreesPerSecond * Time.deltaTime);
        time = Time.deltaTime;    
    }
}
