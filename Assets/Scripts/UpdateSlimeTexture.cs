using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSlimeTexture : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if(Main.PlayerColor!=new Color(0,0,0,0)){
            GetComponent<Image>().material.color = Main.PlayerColor;}
        else{
            GetComponent<Image>().material.color = new Color(255/255f,147/255f,255/255f,255/255f);
        }
    }
}
