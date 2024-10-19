using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Inscribed")]
    public bool platformToGround = false;
    [Tooltip("If true, once the player is above it, the platform transforms into a solid ground object.")]
    public GameObject groundToReplace;

    [Header("Dynamic")]
    public PlayerController player;
    public float distToTop;
    public Collider coll;
    public Vector3 lowerPoint;

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
            if(platformToGround==true){
                lowerPoint= transform.position;
                lowerPoint.y-=0.25f;
                Instantiate(groundToReplace,lowerPoint,transform.rotation);
                Destroy(this);
            }
        } else {
            coll.enabled = false;
        }
    }
}
