using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool win;
    private static AudioSource audioSource;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // Add win condition
        TimeManager.Instance.RegisterOnetimeRoutine(7, 0, 0, () =>
        {
            win = TechTree.Instance.IsUnlocked(TechTree.Tech.RADIO_TOWER);
            SceneManager.LoadScene("EndScene");
        });
    }

    public static void PlaySound(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
