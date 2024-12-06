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
    public StatusBarController statusBarController;
    [Header("Dynamic")]
    public HealthController healthController;
    public MovementController  movementController;
    public ElementHandler     elementHandling;
    public SpriteController spriteController;
    public bool isFrozen   = false;
    public bool isBurning  = false;
    public bool isSundered = false;
    public bool isPoisoned = false;
    public bool isWeak     = false;
    public bool isStrong   = false;
    public int burnStack;
    public int poisonStack;
    public int poisonEffectDmg;
    public float frozenCoolDown;
    public float burnCoolDown;
    private float freezeTime;
    private float burnTime;
    private float poisonTime;
    private float sunderedTime;
    public int activeSunderedCount = 1;

    void Awake() {
        healthController = GetComponent<HealthController>();
        movementController = GetComponent<MovementController>();
        elementHandling = GetComponent<ElementHandler>();
        spriteController = GetComponent<SpriteController>();
        freezeTime     =   0.0f;
        sunderedTime =   0.0f;
        burnTime     =   0.0f;
    }

    void Update() {
        if (frozenCoolDown > 0) {
            frozenCoolDown -= Time.deltaTime;
        }
        if (isFrozen) {
            Frozen();
        }
        if (isBurning) {
            Burning();
        }
        if (isSundered) {
            Sundered();
        }
        if (isPoisoned) {
            Poisoned();
        }
    }

    public void ApplyEffect(List<ElementDefinition> wandElems, Dictionary<elementTypes,int> levels) { //uses list for elements in order to accommodate for more than two elements in the future
        for (int wandSlot = 0; wandSlot < wandElems.Count; wandSlot++) {
            // now actualy uses a list of element definitions for each proj, access each individually
            // to check elem
            ElementDefinition elemDef = wandElems[wandSlot];
            //sets status indicator based on element (elements without a status effect will be removed in default of switch)
            statusBarController.AddIndicator(elemDef.element);
            switch(elemDef.element) {
                case elementTypes.Ice:
                    if (frozenCoolDown <= 0 && !isFrozen) {
                        freezeTime += (elemDef.effectDuration + (0.4f * (float)levels[elementTypes.Ice]));
                        //spriteController.Tint(elemDef.color, freezeTime);
                        isFrozen = true;
                    }
                    break;
                case elementTypes.Fire:
                    if (!isBurning) {
                        burnStack = 0;
                        burnTime += (elemDef.effectDuration + (0.3f * (float)levels[elementTypes.Fire]));
                        spriteController.Tint(hitColor, burnTime);
                        isBurning = true;
                    }
                    if (isBurning && burnStack < 3) {
                        burnStack += 1;
                        burnTime += 0.5f;
                    }
                    break;
                case elementTypes.Water:
                    if (!isSundered) {
                        sunderedTime += (elemDef.effectDuration + (0.2f * (float)levels[elementTypes.Water]));
                        //spriteController.Tint(elemDef.color, sunderedTime);
                        isSundered = true;
                    }
                    break;
                case elementTypes.Poison:
                    if (!isPoisoned) {
                        poisonStack = 0;
                        poisonEffectDmg = 0;
                        poisonTime += (elemDef.effectDuration + (0.5f * (float)levels[elementTypes.Poison]));
                        spriteController.Tint(hitColor,poisonTime);
                        isPoisoned = true;
                        // NEED TO MAKE A POISON UI ELEMENT
                    }
                    if (isPoisoned && poisonStack < 5) {
                        poisonStack += 1;
                        poisonEffectDmg += 1;
                    }
                    break;
                default:
                    //remove status indicator if element doesn't have an effect
                    statusBarController.RemoveIndicator(elemDef.element);
                    break;
            }
        }
    }
    // called in healthcontroller on enemy death
    // any on death effects for enemies
    public void ApplyOnDeathEffects(Dictionary<elementTypes,int> levels) {
        for (int elem = 0; elem < Inventory.I.playerElements.Length; elem++) {
            ElementDefinition elemDef = Main.GET_ELEMENT_DEFINITION(Inventory.I.playerElements[elem]);
            switch(elemDef.element) {
                case elementTypes.Grass:
                    GameObject player = Main.GET_PLAYER();
                    HealthController h = player.GetComponent<HealthController>();
                    // Debug.Log("Healed:" + (elemDef.effectDuration + (float)levels[elementTypes.Grass]));
                    h.Heal(elemDef.effectDuration + (float)levels[elementTypes.Grass]);
                    break;
            }
        }
    }

    // keeps checking for rooted status
    void Frozen()
    {
        movementController.canMove = false;
        freezeTime -= Time.deltaTime;
        if (freezeTime <= 0) {
            isFrozen = false;
            statusBarController.RemoveIndicator(elementTypes.Ice);
            frozenCoolDown = 3.0f;          // wait 3 secs to root again
            movementController.canMove = true;
        }
    }
    void Burning()
    {
        burnTime -= Time.deltaTime;
        healthController.Damage(6.0f * Time.deltaTime);
        if (burnTime <= 0) {
            isBurning = false;
            statusBarController.RemoveIndicator(elementTypes.Fire);
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
            statusBarController.RemoveIndicator(elementTypes.Water);
            movementController.walkingSpeed = movementController.walkingSpeed * 2f;
            movementController.diveSpeed = movementController.diveSpeed * 2f;
            movementController.jumpPower = movementController.jumpPower * 2f;
        }
    }
    void Poisoned()
    {
        poisonTime -= Time.deltaTime;
        healthController.Damage((2.0f + poisonEffectDmg) * Time.deltaTime);
        Debug.Log("poison dmg: " + (4.0f + (poisonEffectDmg * Time.deltaTime)));
        if (poisonTime <= 0) {
            isPoisoned = false;
            statusBarController.RemoveIndicator(elementTypes.Poison);
        }
    }
}
