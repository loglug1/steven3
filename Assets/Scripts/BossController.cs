using UnityEngine;

public enum eAttackTypes {
    spawnMinions,
    dropRocks,
    shootProjectiles,
    riseLava
}

public class BossController : MonoBehaviour
{
    [Header("Inscribed")]
    public float[] attackLengthRange;
    public eAttackTypes[] attackWeights;
    public float minionSpawnDelay = 1f;
    public float rockSpawnDelay = 1f;
    public float projectileDelay = 1f;

    [Header("Dynamic")]
    HealthController healthController;
    BossDamageHandler damageHandler;
    BossMinionSpawner fireMinionSpawner;
    BossMinionSpawner grassMinionSpawner;
    RockSpawner rockSpawner;
    BossMagicProjectiles projectileSpawner;
    eAttackTypes currentAttack = eAttackTypes.shootProjectiles;
    public int currentPhase = 0;
    public float changeAttackCountDown = 0f;
    public float minionCoolDown = 0f;
    public float rockSpawnCooldown = 0f;
    public float projectileCoolDown = 0f;

    void Awake() {
        healthController = GetComponent<HealthController>();
        damageHandler = transform.GetChild(0).GetComponent<BossDamageHandler>();
        projectileSpawner = transform.GetChild(0).GetComponent<BossMagicProjectiles>();
        rockSpawner = transform.GetChild(2).GetComponent<RockSpawner>();
        fireMinionSpawner = transform.GetChild(3).GetComponent<BossMinionSpawner>();
        grassMinionSpawner = transform.GetChild(4).GetComponent<BossMinionSpawner>();
    }
    void FixedUpdate() {
        if (healthController.currHealth < healthController.maxHealth * 0.5f) {
            currentPhase = 1;
        }

        changeAttackCountDown -= Time.deltaTime;

        switch(currentPhase) {
            case 0:
                if (changeAttackCountDown < 0f) {
                    changeAttackCountDown = Random.Range(attackLengthRange[0], attackLengthRange[1]);
                    ChangeAttack();
                }
                Attack();
                break;
            case 1:
                if (changeAttackCountDown < 0f) {
                    changeAttackCountDown = Random.Range(attackLengthRange[0] * 0.5f, attackLengthRange[1] * 0.5f);
                    ChangeAttack();
                }
                Move();
                break;
        }
    }

    void Attack() {
        switch(currentAttack) {
            case eAttackTypes.spawnMinions:
                minionCoolDown -= Time.deltaTime;
                if (minionCoolDown < 0f) {
                    if (Random.Range(0,2) == 0) {
                        fireMinionSpawner.SpawnEnemy();
                    } else {
                        grassMinionSpawner.SpawnEnemy();
                    }
                    minionCoolDown = minionSpawnDelay;
                }
                break;
            case eAttackTypes.dropRocks:
                rockSpawnCooldown -= Time.deltaTime;
                if (rockSpawnCooldown < 0f) {
                    rockSpawner.SpawnRock();
                    rockSpawnCooldown = rockSpawnDelay;
                }
                break;
            case eAttackTypes.shootProjectiles:
                projectileCoolDown -= Time.deltaTime;
                if (projectileCoolDown < 0f) {
                    projectileSpawner.LaunchProjectileAtPlayer(damageHandler.bossType);
                    projectileCoolDown = Main.GET_ELEMENT_DEFINITION(damageHandler.bossType).delayBetweenShots;
                }
                break;
            case eAttackTypes.riseLava:
                break;
        }
    }

    void ChangeAttack() {
        currentAttack = attackWeights[Random.Range(0, attackWeights.Length)];
    }

    void Move() {

    }
}
