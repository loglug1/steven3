using UnityEngine;

public class BossMinionSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public elementTypes enemyElement;

    public void SpawnEnemy() {
        enemyPrefab.GetComponent<EnemyElement>().enemyElementChance = new elementTypes[1] {enemyElement}; //forces the chance to the element to be 100%
        Instantiate(enemyPrefab, transform);
    }
}
