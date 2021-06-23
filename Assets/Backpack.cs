using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Player backpack UI and backend code
 *  
 * @author Jiasheng Zhou
 */
public class Backpack : MonoBehaviour
{
    public GameObject UI;
    public ItemSlot[] itemSlots;

    public bool IsOpen
    {
        get => UI.activeInHierarchy;
    }

    //private SortedSet<int> emptySlots;
    //private IDictionary<Item, int> inventory;

    private void Start()
    {
        UI.SetActive(false);
        
        //emptySlots = new SortedSet<int>();
        //inventory = new Dictionary<Item, int>();
        //for (int i = 0; i < itemSlots.Length; ++i)
        //{
        //    emptySlots.Add(i);
        //}
    }

    void OpenBackpack()
    {
        UI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;
    }

    void CloseBackpack()
    {
        UI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        // Open/close backpack
        if (Input.GetButtonDown("Backpack"))
        {
            if (IsOpen)
            {
                CloseBackpack();
            }
            else
            {
                OpenBackpack();
            }
        }
    }

    /**
     * @brief Add item stack to backpack.
     * @return amount remain in the stack.
     */
    public int AddItem(Item item, int amount)
    {
        if (!item || amount <= 0)
        {
            return 0;
        }

        // Fill stackable slots
        foreach (ItemSlot slot in itemSlots)
        {
            if (slot.IsEmpty || slot.HoldingItem != item)
            {
                continue;
            }

            amount -= slot.AddItem(item, amount);
            if (amount == 0)
            {
                return 0;
            }
        }

        // Fill empty slots
        foreach (ItemSlot slot in itemSlots)
        {
            if (slot.IsEmpty)
            {
                amount -= slot.AddItem(item, amount);
                if (amount == 0)
                {
                    return 0;
                }
            }
        }

        return amount;
    }
}
