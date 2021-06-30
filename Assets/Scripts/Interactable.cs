using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Interactable : MonoBehaviour
{
    public KeyCode interactKey;
    public string hint;
    public bool showHint;
    public UnityAction onInteract;

    private HUD hud;

    private void Awake()
    {
        hud = GameObject.Find("HUD").GetComponent<HUD>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && showHint && hint != null)
        {
            hud.ShowHint("Press '" + interactKey + "' to interact");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            hud.HideHint();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" && Input.GetKeyDown(interactKey))
        {
            if (onInteract != null)
            {
                onInteract();
            }
        }
    }
}
