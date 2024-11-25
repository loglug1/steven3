using UnityEngine;


public class BossMusicController : MonoBehaviour
{
    public AudioClip bossMusic;
    void Awake() {
        AudioSource aS = Camera.main.GetComponent<AudioSource>();
        aS.clip = bossMusic;
        aS.Play();
    }
}
