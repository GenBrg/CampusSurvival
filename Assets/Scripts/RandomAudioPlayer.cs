using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    [Range(0.0f, 1.0f)]
    public float minVolume = 1.0f;
    [Range(0.0f, 1.0f)]
    public float maxVolume = 1.0f;

    public void Play()
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = Random.Range(minVolume, maxVolume);
        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.Play();
        Destroy(audioSource, audioSource.clip.length);
    }
}
