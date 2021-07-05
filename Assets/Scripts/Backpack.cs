using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
 * Player backpack UI and backend code
 *  
 * @author Jiasheng Zhou
 */
public class Backpack : MonoBehaviour
{
    public GameObject UI;
    public ItemSlot[] itemSlots;

    public UnityAction onBackPackOpen;
    public UnityAction onBackPackClose;

    private static Backpack instance;

    public static Backpack Instance
    {
        get => instance;
    }

    public bool IsOpen
    {
        get => UI.activeInHierarchy;
    }

    //private SortedSet<int> emptySlots;
    //private IDictionary<Item, int> inventory;

    private void Awake()
    {
        instance = this;
    }

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

        if (onBackPackOpen != null)
        {
            onBackPackOpen();
        }
    }

    void CloseBackpack()
    {
        UI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;

        if (onBackPackClose != null)
        {
            onBackPackClose();
        }
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

    public bool TryUseItem(ItemPrototype item, int amountToUse, bool useAnyway)
    {
        if (amountToUse == 0)
        {
            return true;
        }

        int amountInInventory = 0;
        Dictionary<ItemSlot, int> slotsToUse = new Dictionary<ItemSlot, int>();
        bool succeed = false;

        // Scan backpack
        foreach (ItemSlot slot in itemSlots)
        {
            if (!slot.IsEmpty && slot.HoldingItem.Equals(item))
            {
                if (amountInInventory + slot.Amount >= amountToUse)
                {
                    slotsToUse[slot] = amountToUse - amountInInventory;
                    succeed = true;
                    break;
                } 
                else
                {
                    slotsToUse[slot] = slot.Amount;
                    amountInInventory += slot.Amount;
                }
            }
        }

        // Use items
        if (succeed || useAnyway)
        {
            foreach (var entry in slotsToUse)
            {
                entry.Key.DeductItem(entry.Value);
            }
        }

        return succeed;
    }

    public bool UseItem(ItemPrototype item)
    {
        ItemSlot slot = FindSlot(item);

        if (slot)
        {
            int itemUsed = slot.DeductItem(1);
            return itemUsed == 1;
        }
        else
        {
            return false;
        }
    }

    public ItemSlot FindSlot(ItemPrototype item)
    {
        foreach (ItemSlot slot in itemSlots)
        {
            if (!slot.IsEmpty && slot.HoldingItem.Equals(item))
            {
                return slot;
            }
        }

        return null;
    }

    /**
     * @brief Add item stack to backpack.
     * @return amount remain in the stack.
     */
    public int AddItem(Item item, int amount)
    {
        if (item == null || amount <= 0)
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
