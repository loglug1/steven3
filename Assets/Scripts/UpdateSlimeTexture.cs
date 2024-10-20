using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSlimeTexture : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Image>().material.color = Main.PlayerColor;
    }
}
