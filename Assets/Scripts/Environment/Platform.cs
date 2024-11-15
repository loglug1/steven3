using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Platform : MonoBehaviour
{
    [Header("Inscribed")]
    public bool platformToGround = false;
    [Tooltip("If true, once the player is above it, the platform transforms into a solid ground object.")]
    public GameObject groundToReplace;

    [Header("Dynamic")]
    public GameObject player;
    public float distToTop;
    public Collider platformCollider;
    public Vector3 lowerPoint;

    void Awake() {
                
        if ((player = Main.GET_PLAYER()) && (player != null)) {
            platformCollider = GetComponent<Collider>();
            distToTop = platformCollider.bounds.extents.y;
        } else {
            Debug.LogError("Could not find player! (Is the player instance linked in the Main script on the camera?)");
        }
    }
    
    public void FixedUpdate() {
        Collider[] nearbyColliders = Physics.OverlapBox(transform.position, transform.localScale, Quaternion.identity, LayerMask.GetMask("Player", "Enemies"));
        foreach (Collider c in nearbyColliders) {
            MovementController movementController = c.GetComponent<MovementController>();
            if (movementController == null) continue; //skip object if it doesn't have a movement controller
            if ((movementController.prevVAxis != -1) && (c.gameObject.transform.position.y > transform.position.y + distToTop)) {
                Physics.IgnoreCollision(platformCollider, c, false); //enable collision if not holding down and entity is above platform
            } else {
                Physics.IgnoreCollision(platformCollider, c, true);
            }
        }
    }
}