using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject triggerSprite;
    public GameObject bossPrefab;
    [Header("Dynamic")]
    bool triggered = false;

    public void OnTriggerEnter(Collider c) {
        Debug.Log("Triggered");
        if (c.gameObject.GetComponent<PlayerController>() == null || triggered) {
            Debug.Log("Ended");
            return;
        }
        Debug.Log("Run");
        triggered = true;
        StartCoroutine(WakeUpBoss());
    }
    IEnumerator WakeUpBoss() {
        float totalAnimationTime = 1f;
        float animationStepTime = Time.smoothDeltaTime;
        float numAnimationSteps = totalAnimationTime/animationStepTime;
        float pumpkinStartSize = triggerSprite.transform.localScale.x;
        float pumpkinEndSize = 1.75f;
        float triggerStartY = triggerSprite.transform.position.y;
        float triggerEndY = -0.53f;
        Debug.Log(animationStepTime);
        for (int i = 0; i < numAnimationSteps; i ++) {
            float frameScale = Mathf.Lerp(pumpkinStartSize, pumpkinEndSize, animationStepTime*i/totalAnimationTime);
            float PosY = Mathf.Lerp(triggerStartY, triggerEndY, animationStepTime*i/totalAnimationTime);
            transform.localScale = Vector3.one * frameScale;
            Vector3 pos = triggerSprite.transform.position;
            pos.y = PosY;
            triggerSprite.transform.position = pos;
            yield return new WaitForSeconds(animationStepTime);
        }
        Instantiate(bossPrefab);
        Destroy(gameObject);
    }
}
