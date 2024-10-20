using UnityEngine;

public class BossMagicProjectiles : MonoBehaviour
{
    [Header("Inscribed")]
    public float launchSpeed;
    public GameObject nonePrefab;
    public GameObject firePrefab;
    public GameObject waterPrefab;
    public GameObject grassPrefab;

    public void LaunchProjectileAtPlayer(elementTypes type) {
        GameObject typePrefab;
        switch(type) {
            case elementTypes.None:
                typePrefab = nonePrefab;
                break;
            case elementTypes.Fire:
                typePrefab = firePrefab;
                break;
            case elementTypes.Grass:
                typePrefab = grassPrefab;
                break;
            case elementTypes.Water:
                typePrefab = waterPrefab;
                break;
            default:
                typePrefab = nonePrefab;
                break;
        }

        GameObject go = Instantiate(typePrefab, transform);
        
        Vector3 direction = Main.GET_PLAYER().transform.position - transform.position;

        go.GetComponent<EnemyProjectile>().vel = direction.normalized * Main.GET_ELEMENT_DEFINITION(type).velocity;
    }
}
