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

    static public void Shoot(elementTypes[] elements, Vector3 vec, Transform pj_anc, Transform shotPointTrans)
    {
        GameObject go;

        float tempDmg = 0;
        float delay = 0;
        Vector3 vel = new Vector3(0,0,0);
        // Calculate velocity and damage
        for (int i = 0; i < elements.Length; ++i) {
            vel     = ((1f/(i+1) * Main.GET_ELEMENT_DEFINITION(elements[i]).velocity) + vel.magnitude) * vec.normalized;
            tempDmg = (1f/(i+1) * Main.GET_ELEMENT_DEFINITION(elements[i]).damageOnHit) + tempDmg;
            delay   = Main.GET_ELEMENT_DEFINITION(elements[i]).delayBetweenShots + delay;
        }
        // vel = vel * vec;
        delay = delay / elements.Length;

        // Instantiate the projectile prefab
        go = Instantiate(Main.GET_ELEMENT_DEFINITION(elements[0]).projectilePrefab, pj_anc.position, Quaternion.identity);
        go.GetComponent<wandProjectile>().dmg = tempDmg;
        for (int i = 0; i < elements.Length; ++i) {
            go.GetComponent<wandProjectile>().eleDefs.Insert(i, Main.GET_ELEMENT_DEFINITION(elements[i]));
        }

        // Set the projectile's position
        Vector3 pos = shotPointTrans.position;
        pos.z = 0; // Ensure z is 0 if it's a 2D game
        go.transform.position = pos - new Vector3(.8f,0,0);

        // Get the Rigidbody component of the new projectile
        Rigidbody goRigid = go.GetComponent<Rigidbody>();
        if (goRigid != null)
        {
            goRigid.velocity = vel; // Apply the calculated velocity
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
