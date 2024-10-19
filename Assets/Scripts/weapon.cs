using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum weaponType {
    wand
}

public class weapon : MonoBehaviour
{
    [Header("dynamic")]
    [SerializeField]
    private weaponType          type = weaponType.wand;
    public  elementTypes[]      eleTypes;
    static public float         nextShotTime;
    private Transform           shotPointTrans;
    static public Transform     PROJECTILE_ANCHOR;
    private Renderer            ren;
    static public ElementDefinition def;

    static public weapon w;
    // Start is called before the first frame update
    void Start()
    {
        w = this;
        ren = GetComponent<Renderer>();
        if (PROJECTILE_ANCHOR == null) {
            GameObject go = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = go.transform;
        }            
        shotPointTrans = transform;

    }

    // Update is called once per frame
    void Update()
    {
        // check for power up pick up
        DetectInput();
    }
    
    private void ShootProj(Vector3 vec) {
        if (Time.time < nextShotTime) {
            return;
        }
        switch(type) {
            case weaponType.wand:
                wandProjectile.Shoot(eleTypes[0], eleTypes[1], vec, PROJECTILE_ANCHOR, shotPointTrans);
                break;

        }
    }
    public void DetectInput() {
        Vector3 totalVec = new Vector3(0,0,0);
        if(Input.GetKey("up")) {
            totalVec = totalVec + new Vector3(0,1,0);
        }
        if(Input.GetKey("down")) {
            totalVec = totalVec + new Vector3(0,-1,0);
        }
        if(Input.GetKey("right")) {
            totalVec = totalVec + new Vector3(1,0,0);
        }
        if(Input.GetKey("left")) {
            totalVec = totalVec + new Vector3(-1,0,0);
        }
        if (totalVec.magnitude > 0) {
            ShootProj(totalVec);
        }
    }
}
