using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wandProjectile : MonoBehaviour
{
    static public ElementDefinition def1;
    static public ElementDefinition def2;
    static public Vector3           vel;
    public float                    dmg;
    private Rigidbody               rigid;
    public bool                     deleteThis;
    public GameObject               go;
    // public Color projectileColor; // Variable to hold projectile color

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    static public void Shoot(elementTypes ele1, elementTypes ele2, Vector3 vec, Transform pj_anc, Transform shotPointTrans)
    {
        GameObject go;
        def1 = Main.GET_ELEMENT_DEFINITION(ele1);
        def2 = Main.GET_ELEMENT_DEFINITION(ele2);

        // Calculate velocity and damage
        vel = def1.velocity * (0.25f * def2.velocity) * vec;
        float tempDmg = def1.damageOnHit + (0.5f * def2.damageOnHit);

        // Instantiate the projectile prefab
        go = Instantiate(def1.projectilePrefab, pj_anc.position, Quaternion.identity);
        go.GetComponent<wandProjectile>().dmg = tempDmg;

        // Set the projectile's position
        Vector3 pos = shotPointTrans.position;
        pos.z = 0; // Ensure z is 0 if it's a 2D game
        go.transform.position = pos;

        // Get the Rigidbody component of the new projectile
        Rigidbody goRigid = go.GetComponent<Rigidbody>();
        if (goRigid != null)
        {
            goRigid.velocity = vel; // Apply the calculated velocity
        }

        // Schedule next shot
        // Assuming you have a way to manage the time for shooting
        weapon.nextShotTime = Time.time + Mathf.Min(def1.delayBetweenShots, def2.delayBetweenShots);
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
