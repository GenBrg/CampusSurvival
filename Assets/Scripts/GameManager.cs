using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool win;

    // Start is called before the first frame update
    void Start()
    {
        TimeManager.Instance.RegisterOnetimeRoutine(1, 0, 0, () =>
        {
            win = true;
            SceneManager.LoadScene("EndScene");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
