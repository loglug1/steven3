using UnityEngine;


public class BossMusicController : MonoBehaviour
{
    bool playing = false;
    public AudioClip bossMusic;
    void FixedUpdate() {
        if (CameraMovement.IsOnScreen(transform.position) && !playing) {
            playing = true;
            Camera.main.GetComponent<AudioSource>().clip = bossMusic;
        }
    }
}
