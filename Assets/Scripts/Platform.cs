using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Dynamic")]
    public PlayerController player;
    public float distToTop;
    public Collider coll;

    void Awake() {
        player = Main.GET_PLAYER();
        coll = GetComponent<Collider>();
        distToTop = coll.bounds.extents.y;
    }

    void FixedUpdate() {
        if ((Input.GetAxis("Vertical") != -1) && (player.transform.position.y > transform.position.y + distToTop)) { //if player is above platform and not holding down
            coll.enabled = true;
        } else {
            coll.enabled = false;
        }
    }
}
