using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    PlayerController player;

    void Awake() {
        player = transform.parent.GetComponent<PlayerController>();
        setColorPlayer();
    }
    void FixedUpdate() {
        if (player.rBody.velocity.x * transform.localScale.x < 0) {
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1f;
            transform.localScale = tempScale;
        }
    }

    void setColorPlayer(){
        player.transform.GetChild(2).GetChild(0).GetComponent<Renderer>().material.color = Main.PlayerColor;
    }
}
