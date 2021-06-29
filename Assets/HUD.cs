using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI hintUI;
    public TextMeshProUGUI healthUI;
    public TextMeshProUGUI ammoUI;

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
    }
}
