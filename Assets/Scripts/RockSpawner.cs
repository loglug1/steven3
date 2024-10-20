using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject rockPrefab;
    public float leftBound = -10f;
    public float rightBound = 10f;
    public void SpawnRock() {
        float xOffset = Random.Range(leftBound, rightBound);
        Instantiate(rockPrefab, transform.position + Vector3.right * xOffset, transform.rotation, transform);
    }
}
