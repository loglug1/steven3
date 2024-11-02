using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyElement : MonoBehaviour
{
    public ElementHandler elementHandler;
    void Awake() {
        elementHandler = GetComponent<ElementHandler>();
        elementHandler.BasicEnemySpawn();
    }
}
