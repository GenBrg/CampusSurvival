using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StructureUIController : MonoBehaviour
{
    private GameObject UI;
    private GameObject structureDescription;

    private static StructureUIController _instance;

    public static StructureUIController Instance
    {
        get => _instance;
    }

    public bool IsOpen
    {
        get => UI.activeInHierarchy;
    }

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        UI = GameObject.Find("Structures");
        structureDescription = GameObject.Find("Structure Description");
        UI.SetActive(false);
    }

    public void OpenStructureUI()
    {
        UI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;
    }

    public void CloseStructureUI()
    {
        UI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
        structureDescription.SetActive(false);
    }

    void Update()
    {
        // Open/close structure UI
        if (Input.GetButtonDown("Structure"))
        {
            if (!IsOpen)
            {
                OpenStructureUI();
            }
            else
            {
                CloseStructureUI();
            }
        }

        if (!IsOpen)
        {
            return;
        }

        bool hitOption = false;

        foreach (RaycastResult raycastResult in Hand.GetObjectsUnderMouse())
        {
            StructureOption structureOption = raycastResult.gameObject.GetComponent<StructureOption>();

            if (structureOption)
            {
                // Show hint
                structureDescription.SetActive(true);
                structureDescription.GetComponent<RectTransform>().position = Input.mousePosition;
                hitOption = true;
                structureOption.OnMouseHover();

                if (Input.GetMouseButton(0))
                {
                    structureOption.OnClick();
                }

                break;
            }
        }

        if (!hitOption)
        {
            structureDescription.SetActive(false);
        }
    }
}
