using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinLoseUI : MonoBehaviour
{
    private TextMeshProUGUI winLoseText;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        winLoseText = GetComponent<TextMeshProUGUI>();
        if (GameManager.win)
        {
            winLoseText.text = "You Win!";
        }
        else
        {
            winLoseText.text = "You Lose!";
        }
        
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
