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

    private TextMeshProUGUI hintText;


    private void Awake()
    {
        hintText = GameObject.Find("Hint").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (showHint && hint != null)
        {
            hintText.gameObject.SetActive(true);
            hintText.text = hint;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(interactKey) && onInteract != null)
        {
            onInteract();
        }
    }
}
