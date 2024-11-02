using UnityEngine;


public class BossMusicController : MonoBehaviour
{
    bool playing = false;
    public AudioClip bossMusic;
    void FixedUpdate() {
        if (CameraMovement.IsOnScreen(transform.position) && !playing) {
            playing = true;
            AudioSource aS = Camera.main.GetComponent<AudioSource>();
            aS.clip = bossMusic;
            aS.Play();
        }
    }
}
