using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Hand : MonoBehaviour
{
    public GameObject itemDescription;
    public TextMeshProUGUI itemTitle;
    public TextMeshProUGUI itemDescriptionText;

    private ItemSlot handSlot;
    private Backpack backpack;

    void Awake()
    {
        handSlot = GetComponentInChildren<ItemSlot>(true);
        backpack = FindObjectOfType<Backpack>();

        handSlot.gameObject.SetActive(false);

        backpack.onBackPackOpen += () =>
        {
            handSlot.gameObject.SetActive(true);
        };

        backpack.onBackPackClose += () =>
        {
            handSlot.DropAll();
            handSlot.gameObject.SetActive(false);
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (!backpack.IsOpen)
        {
            return;
        }

        handSlot.transform.position = Input.mousePosition;
        ItemSlot slot = GetSlotUnderMouse(out bool withinBackpack);

        if (slot && slot.HoldingItem != null)
        {
            itemDescription.SetActive(true);
            itemDescription.GetComponent<RectTransform>().position = Input.mousePosition;
            itemTitle.text = slot.HoldingItem.Name;
            itemTitle.text = slot.HoldingItem.Description;
        }
        else
        {
            itemDescription.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (slot)
            {
                if (handSlot.IsEmpty)
                {
                    // Extract items from the slot
                    slot.TransferAllTo(handSlot);
                }
                else
                {
                    // Merge hand items to the slot
                    handSlot.TransferAllTo(slot);
                }
            }
            else
            {
                if (!handSlot.IsEmpty && !withinBackpack)
                {
                    // Drop all items on hand
                    handSlot.DropAll();
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (slot)
            {
                if (handSlot.IsEmpty)
                {
                    // Extract half of all items from the slot
                    slot.TransferTo(handSlot, Mathf.CeilToInt(slot.Amount / 2.0f));
                }
                else
                {
                    // Merge 1 hand item to the slot
                    handSlot.TransferTo(slot, 1);
                }   
            }
            else
            {
                if (!handSlot.IsEmpty && !withinBackpack)
                {
                    // Drop 1 items on hand
                    handSlot.Drop(1);
                }
            }
        }
    }

    public static List<RaycastResult> GetObjectsUnderMouse()
    {
        GraphicRaycaster rayCaster = FindObjectOfType<GraphicRaycaster>();
        PointerEventData eventData = new PointerEventData(EventSystem.current);

        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();

        rayCaster.Raycast(eventData, results);
        return results;
    }

    ItemSlot GetSlotUnderMouse(out bool withinBackpack)
    {
        ItemSlot resultSlot = null;
        withinBackpack = false;

        foreach (RaycastResult result in GetObjectsUnderMouse())
        {
            ItemSlot slot = result.gameObject.GetComponent<ItemSlot>();

            if (slot && slot != handSlot)
            {
                resultSlot = slot;
            }

            if (result.gameObject.name == "Background")
            {
                withinBackpack = true;
            }
        }

        return resultSlot;
    }


}
