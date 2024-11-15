using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wandProjectile : MonoBehaviour
{
    public float                    dmg;
    public List<ElementDefinition> eleDefs = new List<ElementDefinition>();
    private Rigidbody               rigid;
    public GameObject               go;
    // public Color projectileColor; // Variable to hold projectile color

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    static public void BasicShoot(elementTypes[] elements, Vector3 vec, Transform pj_anc, Transform shotPointTrans)
    {
        GameObject go;

        float tempDmg = 0;
        float delay = 0;
        Vector3 vel = new Vector3(0,0,0);
        WandDefinition currentWand = Main.GET_WAND_DEFINITION(Inventory.I.playerWeapon);
        // Calculate velocity and damage
        Debug.Log(elements.Length);
        for (int i = 0; i < elements.Length; ++i) {
            vel     = (((1f/(i+1) * Main.GET_ELEMENT_DEFINITION(elements[i]).velocity) + vel.magnitude + Inventory.I.playerElementLevels[elements[i]]) * vec.normalized) * currentWand.velocityMult;
            tempDmg = ((1f/(i+1) * Main.GET_ELEMENT_DEFINITION(elements[i]).damageOnHit) + tempDmg + Inventory.I.playerElementLevels[elements[i]]) * currentWand.damageMult;
            delay   = (Main.GET_ELEMENT_DEFINITION(elements[i]).delayBetweenShots + delay) * currentWand.delayMult;
        }
        // vel = vel * vec;
        delay = delay / (elements.Length * 1.1f);

        for (int wandLevel = 1; wandLevel <= Inventory.I.playerWandLevel; ++wandLevel) {
            if (Inventory.I.playerWeapon == weaponType.singleElementFocusWand) {
                go = Instantiate(Main.GET_ELEMENT_DEFINITION(elements[0]).projectilePrefab, pj_anc.position, Quaternion.identity);
            }
            else {
                go = Instantiate(Main.GET_ELEMENT_DEFINITION(elements[wandLevel - 1]).projectilePrefab, pj_anc.position, Quaternion.identity);
            }

            go.GetComponent<wandProjectile>().dmg = tempDmg;
            // o(n^2) :(
            for (int j = 0; j < elements.Length; ++j) {
                go.GetComponent<wandProjectile>().eleDefs.Insert(j, Main.GET_ELEMENT_DEFINITION(elements[j]));
            }

            // Set the projectile's position
            Vector3 pos = shotPointTrans.position;
            pos.z = 0; // Ensure z is 0 if it's a 2D game
            int offset = wandLevel;
            go.transform.position = pos + new Vector3(0,offset/2,0); 

            // Get the Rigidbody component of the new projectile
            Rigidbody goRigid = go.GetComponent<Rigidbody>();
            if (goRigid != null)
            {
                goRigid.velocity = vel; // Apply the calculated velocity
            }
        }

        weapon.nextShotTime = Time.time + delay;
    }


    public void OnCollisionEnter(Collision c) {
        GameObject gob = c.gameObject;
        if(gob.layer == LayerMask.NameToLayer("Ground")) {
            Destroy(go);
        }

    }

    public void OnTriggerEnter(Collider c) {
        GameObject gob = c.gameObject;
        if(gob.layer == LayerMask.NameToLayer("Ground")) {
            Destroy(go);
        }

    }
}
