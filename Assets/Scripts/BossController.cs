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
    public float moveSpeed;
    public Vector3[] phaseOnePositions;
    public Vector3[] phaseTwoPositions;
    HealthController healthController;
    BossDamageHandler damageHandler;
    BossMinionSpawner fireMinionSpawner;
    BossMinionSpawner grassMinionSpawner;
    RockSpawner rockSpawner;
    BossMagicProjectiles projectileSpawner;
    public Transform stevenTransform;
    [Header("Dynamic")]
    public eAttackTypes currentAttack = eAttackTypes.shootProjectiles;
    public int currentPhase = 0;
    public float changeAttackCountDown = 0f;
    public float minionCoolDown = 0f;
    public float rockSpawnCooldown = 0f;
    public float projectileCoolDown = 0f;
    public int moveStep;
    public bool speedIncreased = false;

    void Awake() {
        healthController = GetComponent<HealthController>();
        damageHandler = transform.GetChild(0).GetComponent<BossDamageHandler>();
        projectileSpawner = transform.GetChild(0).GetComponent<BossMagicProjectiles>();
        rockSpawner = transform.GetChild(2).GetComponent<RockSpawner>();
        fireMinionSpawner = transform.GetChild(3).GetComponent<BossMinionSpawner>();
        grassMinionSpawner = transform.GetChild(4).GetComponent<BossMinionSpawner>();
        stevenTransform = transform.GetChild(0);

        GeneratePhaseTwoPositions();
    }
    void FixedUpdate() {
        if (healthController.currHealth < healthController.maxHealth * 0.5f) {
            currentPhase = 1;
        }
        if (healthController.currHealth < healthController.maxHealth * 0.05f && !speedIncreased) {
            moveSpeed *= 2f;
            speedIncreased = true;
        }

        changeAttackCountDown -= Time.deltaTime;

        switch(currentPhase) {
            case 0:
                if (changeAttackCountDown < 0f) {
                    changeAttackCountDown = Random.Range(attackLengthRange[0], attackLengthRange[1]);
                    ChangeAttack();
                }
                Attack();
                Move(phaseOnePositions);
                break;
            case 1:
                if (changeAttackCountDown < 0f) {
                    changeAttackCountDown = Random.Range(attackLengthRange[0] * 0.5f, attackLengthRange[1] * 0.5f);
                    ChangeAttack();
                }
                Attack();
                Move(phaseTwoPositions);
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

    void Move(Vector3[] targets) {
        Vector3 toTarget = targets[moveStep % targets.Length] - stevenTransform.localPosition;
        if (toTarget.magnitude <= 0.05f || moveSpeed * Time.deltaTime > toTarget.magnitude) {
            moveStep++;
            return;
        } else {
            stevenTransform.localPosition += Time.deltaTime * moveSpeed * toTarget.normalized;
        }
    }

    void GeneratePhaseTwoPositions() {
        Vector3 basePoint = phaseTwoPositions[0];
        int numPoints = 20;
        phaseTwoPositions = new Vector3[numPoints * 2];
        int numPeriods = 5;
        for (int i = 0; i < numPoints; i++) {
            float x, y;
            x = i;
            y = 2.5f * Mathf.Sin(i * 2f * numPeriods * Mathf.PI/numPoints);
            phaseTwoPositions[i] = new Vector3(basePoint.x + x, basePoint.y + y, 0);
        }
        basePoint = phaseTwoPositions[numPoints - 1];
        //generate double points, this is the return set
        for (int i = 0; i < numPoints; i++) {
            float x, y;
            x = -i;
            y = 2.5f * Mathf.Sin(i * 2f * numPeriods * Mathf.PI/numPoints);
            phaseTwoPositions[numPoints + i] = new Vector3(basePoint.x + x, basePoint.y + y, 0);
        }
    }
}
