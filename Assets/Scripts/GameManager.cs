using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool win;

    void Start()
    {
        // Add win condition
        TimeManager.Instance.RegisterOnetimeRoutine(7, 0, 0, () =>
        {
            win = true;
            SceneManager.LoadScene("EndScene");
        });
    }
}
