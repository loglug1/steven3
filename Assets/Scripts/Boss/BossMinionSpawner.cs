using System.Security.Cryptography;
using UnityEngine;

public class BossMinionSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public elementTypes enemyElement;
    public ElementHandler elementHandler;

    public void SpawnEnemy() {
        
        GameObject e = Instantiate(enemyPrefab, transform);
        elementHandler.SetEnemyElement(enemyElement);
        EnemyController eC = e.GetComponent<EnemyController>();
        eC.canSeePlayer = true;
        eC.objectPermanence = 3f;
    }
}
