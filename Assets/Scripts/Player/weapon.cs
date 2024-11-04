using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum weaponType {
    basicWand,
    singleElementFocusWand,
    multiElementFocusWand
}

public class weapon : MonoBehaviour
{
    [Header("inscribed")]
    public weaponType          type;

    [Header("Dynamic")]
    public  elementTypes[]      eleTypes;
    static public float         nextShotTime;
    private Transform           shotPointTrans;
    static public Transform     PROJECTILE_ANCHOR;
    private Renderer            ren;
    // List<ElementDefinition> wandElementDefinitions = new List<ElementDefinition>();

    static public weapon w;
    // Start is called before the first frame update
    void Awake()
    {
        w = this;

        ren = GetComponent<Renderer>();


        // set to gray/basic
        UpdateColor(Main.GET_ELEMENT_DEFINITION(elementTypes.None), Main.GET_ELEMENT_DEFINITION(elementTypes.None));


        if (PROJECTILE_ANCHOR == null) {
            GameObject go = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = go.transform;
        }            
        shotPointTrans = transform;

    }

    // Update is called once per frame
    void Update()
    {
        DetectInput();
    }
    
    private void ShootProj(Vector3 vec) {
        if (Time.time < nextShotTime) {
            return;
        }
        switch(type) {
            case weaponType.basicWand:
                wandProjectile.Shoot(eleTypes, vec, PROJECTILE_ANCHOR, shotPointTrans);
                break;
            case weaponType.multiElementFocusWand:

                break;
            case weaponType.singleElementFocusWand:

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
            ShootProj(totalVec.normalized);
        }
    }
    public void UpdateColor(ElementDefinition def1, ElementDefinition def2) {
        ren.material.color = (def1.color + def2.color) / 2.0f;
    }
}
