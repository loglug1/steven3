using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject enemyPrefab;
    [Header("Dynamic")]
    bool onScreen;

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(1, 2, 1));
    }

    void FixedUpdate() {
        if (onScreen != CameraMovement.IsOnScreen(transform.position)) { //if the state of being on screen is different from last frame
            foreach(Transform child in transform) { //gets transform of children (no idea why this works)
                child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
            onScreen = !onScreen;
        }
    }

    void Awake() {
        onScreen = false;
        GameObject enemy1 = Instantiate(enemyPrefab, transform);
        enemy1.SetActive(false);
    }
}
