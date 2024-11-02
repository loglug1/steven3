using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public HealthController healthController;
    public MovementController  enemyController;
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
    public GameObject burningUI;
    public GameObject sunderedUI;
    public GameObject rootedUI;
    public int count = 1;
    // Start is called before the first frame update
    void Awake()
    {
        healthController = GetComponent<HealthController>();
        enemyController = GetComponent<MovementController>();
        enemyElement = GetComponent<EnemyElement>();
        waitTime     =   0.0f;
        sunderedTime =   0.0f;
        burnTime     =   0.0f;
        burningUI.SetActive(false);
        sunderedUI.SetActive(false);
        rootedUI.SetActive(false);
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
        // probably move statuses to new file   
        // primary grass
        if (wProj != null) {
            if (wandProjectile.def1.name == "Grass" && rootedCoolDown <= 0 && !isRooted) {
                waitTime += 1.5f;        // rooted time for primary
                isRooted = true;
                rootedUI.SetActive(true);

            }
            // secondary grass
            if (wandProjectile.def2.name == "Grass" && rootedCoolDown <= 0 && !isRooted) {
                waitTime += 0.5f;        // rooted time for secondary
                isRooted = true;
                rootedUI.SetActive(true);
            }  
            // primary fire
            if (wandProjectile.def1.name == "Fire" && !isBurning) {
                burnTime += 2.0f;        // burning time for primary
                isBurning = true;
                burningUI.SetActive(true);
            }
            // secondary fire
            if (wandProjectile.def2.name == "Fire" && !isBurning) {
                burnTime += 0.5f;        // burning time for secondary
                isBurning = true;
                burningUI.SetActive(true);

            }
            if (wandProjectile.def1.name == "Water" && !isSundered) {
                sunderedTime += 1.8f;    // slowing time for primary
                isSundered = true;
                sunderedUI.SetActive(true);
            }
            if (wandProjectile.def2.name == "Water" && !isSundered) {
                sunderedTime += 0.6f;    // slowing time for secondary
                isSundered = true;
                sunderedUI.SetActive(true);
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
            rootedUI.SetActive(false);
            isRooted = false;
            rootedCoolDown = 3.0f;          // wait 3 secs to root again
            enemyController.canMove = true;
        }
    }
    void Burning()
    {
        burnTime -= Time.deltaTime;
        healthController.Damage(6.0f * Time.deltaTime);
        if (burnTime <= 0) {
            isBurning = false;
            burningUI.SetActive(false); 
        }
    }
    void Sundered()
    {
        sunderedTime -= Time.deltaTime;
        if (count > 0) {
            count = count - 1;
            enemyController.walkingSpeed = enemyController.walkingSpeed * 0.5f;
            enemyController.diveSpeed = enemyController.diveSpeed * 0.5f;
            enemyController.jumpPower = enemyController.jumpPower * 0.5f;
        }

        if (sunderedTime <= 0) {
            count = 1;
            isSundered = false;
            sunderedUI.SetActive(false);
            enemyController.walkingSpeed = enemyController.walkingSpeed * 2f;
            enemyController.diveSpeed = enemyController.diveSpeed * 2f;
            enemyController.jumpPower = enemyController.jumpPower * 2f;
        }
    }
}
