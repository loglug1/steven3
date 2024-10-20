using System.Security.Cryptography;
using UnityEngine;

public class BossMinionSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public elementTypes enemyElement;

    public void SpawnEnemy() {
        enemyPrefab.GetComponent<EnemyElement>().enemyElementChance = new elementTypes[1] {enemyElement}; //forces the chance to the element to be 100%
        GameObject e = Instantiate(enemyPrefab, transform);
        EnemyController eC = e.GetComponent<EnemyController>();
        eC.canSeePlayer = true;
        eC.objectPermanence = 3f;
    }
}
