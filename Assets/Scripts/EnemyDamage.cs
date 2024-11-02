using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public HealthController healthController;
    public EnemyController  enemyController;
    public ElementHandler     elementHandling;
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

    // variables that hold current elements to make code shorter lol
    public ElementDefinition currentDef1;
    public ElementDefinition currentDef2;
    void Awake()
    {
        healthController = GetComponent<HealthController>();
        enemyController = GetComponent<EnemyController>();
        elementHandling = GetComponent<ElementHandler>();
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
         
        
        if (wProj != null) {

            currentDef1 = wandProjectile.def1;
            currentDef2 = wandProjectile.def2;
            // primary grass
            if (currentDef1.name == "Grass" && rootedCoolDown <= 0 && !isRooted) {
                waitTime += 1.5f;        // rooted time for primary
                isRooted = true;
                rootedUI.SetActive(true);

            }
            // secondary grass
            if (currentDef2.name == "Grass" && rootedCoolDown <= 0 && !isRooted) {
                waitTime += 0.5f;        // rooted time for secondary
                isRooted = true;
                rootedUI.SetActive(true);
            }  
            // primary fire
            if (currentDef1.name == "Fire" && !isBurning) {
                burnTime += 2.0f;        // burning time for primary
                isBurning = true;
                burningUI.SetActive(true);
            }
            // secondary fire
            if (currentDef2.name == "Fire" && !isBurning) {
                burnTime += 0.5f;        // burning time for secondary
                isBurning = true;
                burningUI.SetActive(true);

            }
            if (currentDef1.name == "Water" && !isSundered) {
                sunderedTime += 1.8f;    // slowing time for primary
                isSundered = true;
                sunderedUI.SetActive(true);
            }
            if (currentDef2.name == "Water" && !isSundered) {
                sunderedTime += 0.6f;    // slowing time for secondary
                isSundered = true;
                sunderedUI.SetActive(true);
            }

            EnemyDamageCalculation(currentDef1, currentDef2, wProj); 

           
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
            enemyController.moveDownSpeed = enemyController.moveDownSpeed * 0.5f;
            enemyController.jumpPower = enemyController.jumpPower * 0.5f;
        }

        if (sunderedTime <= 0) {
            count = 1;
            isSundered = false;
            sunderedUI.SetActive(false);
            enemyController.walkingSpeed = enemyController.walkingSpeed * 2f;
            enemyController.moveDownSpeed = enemyController.moveDownSpeed * 2f;
            enemyController.jumpPower = enemyController.jumpPower * 2f;
        }
    }

    public float EnemyDamageCalculation(ElementDefinition currentDef1, ElementDefinition currentDef2, wandProjectile wProj)
    {
        if (currentDef1.weakElement == elementHandling.enemyElementofChoice) {
            wProj.dmg = wProj.dmg * elementHandling.resMult;
        }
        if (currentDef2.weakElement == elementHandling.enemyElementofChoice) {
            wProj.dmg = wProj.dmg * elementHandling.secondaryResMult;
        }
        if (currentDef1.strElement == elementHandling.enemyElementofChoice) {
            wProj.dmg = wProj.dmg * elementHandling.strMult;
        }
        if (currentDef2.strElement == elementHandling.enemyElementofChoice) {
            wProj.dmg = wProj.dmg * elementHandling.secondaryStrMult;        
        }
        return(wProj.dmg);
    }
}
