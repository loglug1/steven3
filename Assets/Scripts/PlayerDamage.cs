using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [Header("Inscribed")]
    public float invincibilityTime = 1f;

    [Header("Dynamic")]
    public HealthController healthController;
    public PlayerController  playerController;
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
        playerController = GetComponent<PlayerController>();
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
    //when enemy hits player
    void OnCollisionEnter(Collision c) {
        GameObject otherGO = c.gameObject;
        EnemyElement eEle = otherGO.GetComponent<EnemyElement>();
        
        //allow invicibility for some time
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), true);
        Invoke("ResetInvicibility", invincibilityTime);

        if (eEle == null) return;
        HandleHit(eEle);
    }

    void ResetInvicibility() {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), false);
    }

    void HandleHit(EnemyElement eEle)
    {
        // primary grass
        if (eEle != null) {
            if (eEle.enemyElementofChoice == elementTypes.Grass && rootedCoolDown <= 0 && !isRooted) {
                waitTime += 1.5f;        // rooted time for primary
                isRooted = true;
            }
            // primary fire
            if (eEle.enemyElementofChoice == elementTypes.Fire && !isBurning) {
                burnTime += 1.5f;        // burning time for primary
                isBurning = true;
            }
            if (eEle.enemyElementofChoice == elementTypes.Water && !isSundered) {
                sunderedTime += 2.5f;    // slowing time for primary
                isSundered = true;
            }

            healthController.Damage(Main.GET_ELEMENT_DEFINITION(elementTypes.None).damageOnHit);
        }
    }
    // keeps checking for rooted status
    void Rooted()
    {
        playerController.canMove = false;
        waitTime -= Time.deltaTime;
        Debug.Log(waitTime);
        if (waitTime <= 0) {
            isRooted = false;
            rootedCoolDown = 3.0f;          // wait 3 secs to root again
            playerController.canMove = true;
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
            playerController.walkingSpeed = playerController.walkingSpeed * 0.5f;
            playerController.moveDownSpeed = playerController.moveDownSpeed * 0.5f;
            playerController.jumpPower = playerController.jumpPower * 0.5f;
        }

        if (sunderedTime <= 0) {
            count = 1;
            isSundered = false;
            playerController.walkingSpeed = playerController.walkingSpeed * 2f;
            playerController.moveDownSpeed = playerController.moveDownSpeed * 2f;
            playerController.jumpPower = playerController.jumpPower * 2f;
        }
    }
}
