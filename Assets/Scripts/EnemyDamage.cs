using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public HealthController healthController;
    public EnemyController  enemyController;
    public EnemyElement     enemyElement;
    public bool isRooted   = false;
    public bool isBurning  = false;
    public bool isSundered = false;
    public bool isWeak     = false;
    public bool isStrong   = false;
    public float rootedCoolDown;
    public float burnCoolDown;
    private float waitTime;
    private float burnTime;
    private float sunderedTime;
    public int count = 1;
    // Start is called before the first frame update
    void Awake()
    {
        healthController = GetComponent<HealthController>();
        enemyController = GetComponent<EnemyController>();
        enemyElement = GetComponent<EnemyElement>();
        waitTime     =   0.0f;
        sunderedTime =   0.0f;
        burnTime     =   0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (rootedCoolDown > 0) {
            rootedCoolDown -= Time.deltaTime;
        }
        if (isRooted) {
            Rooted();
        }
        if (isBurning) {
            Burning();
        }
        if (isSundered) {
            Sundered();
        }
    }
    //meant for water projectiles
    void OnCollisionEnter(Collision c) {
        GameObject otherGO = c.gameObject;
        wandProjectile wProj = otherGO.GetComponent<wandProjectile>();

        HandleProjectile(wProj, otherGO);
    }

    //meant for all other projectiles
    void OnTriggerEnter(Collider c) {
        GameObject otherGO = c.gameObject;
        wandProjectile wProj = otherGO.GetComponent<wandProjectile>();

        HandleProjectile(wProj, otherGO);
    }
    void HandleProjectile(wandProjectile wProj, GameObject otherGO)
    {
        // primary grass
        if (wProj != null) {
            if (wandProjectile.def1.name == "Grass" && rootedCoolDown <= 0 && !isRooted) {
                waitTime += 1.5f;        // rooted time for primary
                isRooted = true;
            }
            // secondary grass
            if (wandProjectile.def2.name == "Grass" && rootedCoolDown <= 0 && !isRooted) {
                waitTime += 0.5f;        // rooted time for secondary
                isRooted = true;
            }  
            // primary fire
            if (wandProjectile.def1.name == "Fire" && !isBurning) {
                burnTime += 1.5f;        // burning time for primary
                isBurning = true;
            }
            // secondary fire
            if (wandProjectile.def2.name == "Fire" && !isBurning) {
                burnTime += 0.5f;        // burning time for secondary
                isBurning = true;
            }
            if (wandProjectile.def1.name == "Water" && !isSundered) {
                sunderedTime += 2.5f;    // slowing time for primary
                isSundered = true;
            }
            if (wandProjectile.def2.name == "Water" && !isSundered) {
                sunderedTime += 1.0f;    // slowing time for secondary
                isSundered = true;
            }
            if (wandProjectile.def1.weakElement == enemyElement.enemyElementofChoice) {
                wProj.dmg = wProj.dmg * enemyElement.resMult;
            }
            if (wandProjectile.def2.weakElement == enemyElement.enemyElementofChoice) {
                wProj.dmg = wProj.dmg * enemyElement.secondaryResMult;
            }
            if (wandProjectile.def1.strElement == enemyElement.enemyElementofChoice) {
                wProj.dmg = wProj.dmg * enemyElement.strMult;
            }
            if (wandProjectile.def2.strElement == enemyElement.enemyElementofChoice) {
                wProj.dmg = wProj.dmg * enemyElement.secondaryStrMult;
            }
            healthController.Damage(wProj.dmg);
            Destroy(otherGO);
        }
    }
    // keeps checking for rooted status
    void Rooted()
    {
        enemyController.canMove = false;
        waitTime -= Time.deltaTime;
        if (waitTime <= 0) {
            isRooted = false;
            rootedCoolDown = 3.0f;          // wait 3 secs to root again
            enemyController.canMove = true;
        }
    }
    void Burning()
    {
        burnTime -= Time.deltaTime;
        healthController.Damage(4.0f * Time.deltaTime);
        if (burnTime <= 0) {
            isBurning = false;
        }
    }
    void Sundered()
    {
        sunderedTime -= Time.deltaTime;
        if (count > 0) {
            count = count - 1;
            enemyController.walkingSpeed = enemyController.walkingSpeed * 0.5f;
            enemyController.moveDownSpeed = enemyController.moveDownSpeed * 0.5f;
            enemyController.jumpPower = enemyController.jumpPower * 0.5f;
        }

        if (sunderedTime <= 0) {
            count = 1;
            isSundered = false;
            enemyController.walkingSpeed = enemyController.walkingSpeed * 2f;
            enemyController.moveDownSpeed = enemyController.moveDownSpeed * 2f;
            enemyController.jumpPower = enemyController.jumpPower * 2f;
        }
    }
}
