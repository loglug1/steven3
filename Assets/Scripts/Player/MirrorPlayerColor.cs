using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorPlayerColor : MonoBehaviour
{
    void Awake()
    {
        if (GetComponent<Image>() != null) {
            GetComponent<Image>().color = Main.PlayerColor;
        }
        else if (GetComponent<SpriteRenderer>() != null) {
            GetComponent<SpriteRenderer>().color = Main.PlayerColor;
        }
    }
}
