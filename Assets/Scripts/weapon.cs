using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum weaponType {
    wand
}
public enum elementTypes {
    none,
    fire,
    water,
    grass
}
public enum Colors {
    white,
    orange,
    blue,
    green
}

// call specific prefab not color

[System.Serializable]
public class ElementDefinition {
    public elementTypes element             = elementTypes.none;
    public GameObject projectilePrefab;
    public float        damageOnHit         = 0;
    public float        damagePerSec        = 0;
    public float        delayBetweenShots   = 0;
    public float        velocity            = 50;
    public Colors       color;
    
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
    private Renderer ren;
    static public ElementDefinition def;
    // Start is called before the first frame update
    void Start()
    {
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
        if(Input.GetKey("up")) {
            ShootProj(new Vector3(0,1,0));
        }
        else if(Input.GetKey("down")) {
            ShootProj(new Vector3(0,-1,0));
        }
        else if(Input.GetKey("right")) {
            ShootProj(new Vector3(1,0,0));
        }
        else if(Input.GetKey("left")) {
            ShootProj(new Vector3(-1,0,0));
        }
    }
}