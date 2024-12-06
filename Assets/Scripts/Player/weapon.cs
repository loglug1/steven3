using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum weaponType {
    basicWand,
    singleElementFocusWand,
    multiElementFocusWand
}

[System.Serializable]
public class WandDefinition {
    // make definitions in main, with shop and elem defs, list is already there
    public weaponType wandType;
    public int maxElementTypes;
    public int maxWandLevel; 
    // usually the same as maxElementTypes, but I thought about how this might be different for the single element wand

    // not sure if these will actually have damage/vel/delay mults, but just in case
    [Header("Stat Mults")]
    public float damageMult;
    public float velocityMult;
    public float delayMult;

}

public class weapon : MonoBehaviour
{
    [Header("Dynamic")]
    public weaponType           type;
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
        


        if (PROJECTILE_ANCHOR == null) {
            GameObject go = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = go.transform;
        }            
        shotPointTrans = transform;

    }

    // Update is called once per frame
    void Update()
    {
        // constantly detect input
        DetectInput();
    }
    
    private void ShootProj(Vector3 vec) {
        if (Time.time < nextShotTime) {
            return;
        }
        // different shoot for each wand (placeholder)
        AudioController.PlayClip("shoot");
        switch(type) {
            case weaponType.basicWand:
                wandProjectile.BasicShoot(Inventory.I.playerElements, vec, PROJECTILE_ANCHOR, shotPointTrans);
                break;
            case weaponType.multiElementFocusWand:
                wandProjectile.BasicShoot(Inventory.I.playerElements, vec, PROJECTILE_ANCHOR, shotPointTrans);
                break;
            case weaponType.singleElementFocusWand:
                wandProjectile.BasicShoot(Inventory.I.playerElements, vec, PROJECTILE_ANCHOR, shotPointTrans);                
                break;
        }
    }
    public void DetectInput() {
        // creates direction vector based on directions being held down
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
    public void UpdateColor(elementTypes[] eleTypes) {
        // passes in list of element types upon chosing an element
        // sets color of wand to mix of element colors
        int i;
        for (i = 0; i < eleTypes.Length; ++i) {
            ren.material.color = ren.material.color + Main.GET_ELEMENT_DEFINITION(eleTypes[i]).color;
        }
        ren.material.color = ren.material.color / (float)i;
    }
}
