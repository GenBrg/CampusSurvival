using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI hintUI;
    public TextMeshProUGUI healthUI;
    public TextMeshProUGUI ammoUI;
    public TextMeshProUGUI timeUI;

    // Start is called before the first frame update
    void Awake()
    {
        HideHint();
        HideAmmo();

        Health playerHealth = GameObject.Find("Player").GetComponent<Health>();
        playerHealth.onHealthChange += (float currentHealth, float maxHealth) =>
        {
            SetHealth((int)currentHealth, (int)maxHealth);
        };
    }

    private void Start()
    {
        TimeManager.Instance.onTimeChange += (int day, int hour, int minute) => SetTime(day, hour, minute);
    }

    public void ShowHint(string text)
    {
        hintUI.gameObject.SetActive(true);
        hintUI.text = text;
    }

    public void HideHint()
    {
        hintUI.gameObject.SetActive(false);
    }

    public void ShowAmmo(int currentAmmo, int maxAmmo)
    {
        ammoUI.gameObject.SetActive(true);
        ammoUI.text = "Ammo: " + currentAmmo + " / " + maxAmmo;
    }

    public void HideAmmo()
    {
        ammoUI.gameObject.SetActive(false);
    }

    public void SetHealth(int currentHealth, int maxHealth)
    {
        healthUI.text = "HP: " + currentHealth + " / " + maxHealth;
        healthUI.color = Color.Lerp(Color.red, Color.green, (float)currentHealth / maxHealth);
    }

    public void SetTime(int day, int hour, int minute)
    {
        timeUI.text = string.Format("Day {0} {1, -2} : {2, 2}", day, hour, minute);
    }
}
