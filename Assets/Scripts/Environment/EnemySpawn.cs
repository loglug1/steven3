using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject enemyPrefab;
    [Header("Dynamic")]
    bool active;

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(1, 2, 1));
    }

    void FixedUpdate() {
        if (!active && CameraMovement.IsOnScreen(transform.position)) { // only enable when on the current screen
            foreach(Transform child in transform) { //gets transform of children (no idea why this works)
                child.gameObject.SetActive(true);
            }
            active = true;
        }
        if (active && !CameraMovement.IsOnOrNeighboringScreen(transform.position)) { // only disable when the camera is two rooms away
            foreach(Transform child in transform) { //gets transform of children (no idea why this works)
                child.gameObject.SetActive(false);
            }
            active = false;
        }
    }

    void Awake() {
        active = false;
        GameObject enemy1 = Instantiate(enemyPrefab, transform);
        enemy1.SetActive(false);
    }
}
