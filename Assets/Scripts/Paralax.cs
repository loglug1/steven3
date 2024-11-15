using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    private float length, startpos;
    public GameObject camden;//camera
    public float parallaxEffect;
    void Start()
    {
        startpos=transform.position.x;
        length=GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (camden.transform.position.x * (1 - parallaxEffect));
        float distance=(camden.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startpos+distance, transform.position.y, transform.position.z);

        if(temp>startpos+length) startpos += length;
        else if(temp<startpos-length) startpos-=length;
    }
}
