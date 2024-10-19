using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Dynamic")]
    public PlayerController player;
    public float distToTop;
    public Collider coll;

    void Awake() {
        if ((player = Main.GET_PLAYER()) && (player != null)) {
            coll = GetComponent<Collider>();
            distToTop = coll.bounds.extents.y;
        } else {
            Debug.LogError("Could not find player! (Is the player instance linked in the Main script on the camera?)");
        }
    }

    void FixedUpdate() {
        if ((Input.GetAxis("Vertical") != -1) && (player.transform.position.y > transform.position.y + distToTop)) { //if player is above platform and not holding down
            coll.enabled = true;
        } else {
            coll.enabled = false;
        }
    }
}
