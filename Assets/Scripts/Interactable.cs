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

    public string Hint
    {
        set {
            hint = value;
            HUD.Instance.SetHintText(value);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && showHint && hint != null)
        {
            HUD.Instance.ShowHint(hint);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            HUD.Instance.HideHint();
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
