using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(HealthController))]
[RequireComponent(typeof(SpriteController))]
public class StatusController : MonoBehaviour
{
    [Header("Inscribed")]
    public Color hitColor = new Color(1, 0, 0);
    public float hitBlinkTime = 0.25f;
    [Header("Dynamic")]
    public HealthController healthController;
    public MovementController  movementController;
    public ElementHandler     elementHandling;
    public SpriteController spriteController;
    public bool isRooted   = false;
    public bool isBurning  = false;
    public bool isSundered = false;
    public bool isWeak     = false;
    public bool isStrong   = false;
    public float rootedCoolDown;
    public float burnCoolDown;
    private float rootTime;
    private float burnTime;
    private float sunderedTime;
    public GameObject burningUI;
    public GameObject sunderedUI;
    public GameObject rootedUI;
    public int activeSunderedCount = 1;

    void Awake() {
        healthController = GetComponent<HealthController>();
        movementController = GetComponent<MovementController>();
        elementHandling = GetComponent<ElementHandler>();
        spriteController = GetComponent<SpriteController>();
        rootTime     =   0.0f;
        sunderedTime =   0.0f;
        burnTime     =   0.0f;
        burningUI.SetActive(false);
        sunderedUI.SetActive(false);
        rootedUI.SetActive(false);
    }

    void Update() {
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

    public void ApplyEffect(List<ElementDefinition> wandElems) { //uses list for elements in order to accommodate for more than two elements in the future
        for (int wandSlot = 0; wandSlot < wandElems.Count; wandSlot++) {
            ElementDefinition elemDef = wandElems[wandSlot];
            switch(elemDef.element) {
                case elementTypes.Grass:
                    if (rootedCoolDown <= 0 && !isRooted) {
                        rootTime += elemDef.effectDurations[wandSlot];
                        //spriteController.Tint(elemDef.color, rootTime);
                        isRooted = true;
                        rootedUI.SetActive(true);
                    }
                    break;
                case elementTypes.Fire:
                    if (!isBurning) {
                        burnTime += elemDef.effectDurations[wandSlot];
                        spriteController.Tint(hitColor, burnTime);
                        isBurning = true;
                        burningUI.SetActive(true);
                    }
                    break;
                case elementTypes.Water:
                    if (!isSundered) {
                        sunderedTime += elemDef.effectDurations[wandSlot];
                        //spriteController.Tint(elemDef.color, sunderedTime);
                        isSundered = true;
                        sunderedUI.SetActive(true);
                    }
                    break;
            }
        }
    }

    // keeps checking for rooted status
    void Rooted()
    {
        movementController.canMove = false;
        rootTime -= Time.deltaTime;
        if (rootTime <= 0) {
            rootedUI.SetActive(false);
            isRooted = false;
            rootedCoolDown = 3.0f;          // wait 3 secs to root again
            movementController.canMove = true;
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
        if (activeSunderedCount > 0) {
            activeSunderedCount = activeSunderedCount - 1;
            movementController.walkingSpeed = movementController.walkingSpeed * 0.5f;
            movementController.diveSpeed = movementController.diveSpeed * 0.5f;
            movementController.jumpPower = movementController.jumpPower * 0.5f;
        }

        if (sunderedTime <= 0) {
            activeSunderedCount = 1;
            isSundered = false;
            sunderedUI.SetActive(false);
            movementController.walkingSpeed = movementController.walkingSpeed * 2f;
            movementController.diveSpeed = movementController.diveSpeed * 2f;
            movementController.jumpPower = movementController.jumpPower * 2f;
        }
    }
}
