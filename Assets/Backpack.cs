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

    void Update()
    {
        // Open/close backpack
        if (Input.GetButtonDown("Backpack"))
        {
            UI.SetActive(!UI.activeInHierarchy);
        }
    }

    /**
     * @brief Add item stack to backpack.
     * @return amount remain in the stack.
     */
    public int AddItem(Item item, int amount)
    {
        // Fill stackable slots
        foreach (ItemSlot slot in itemSlots)
        {
            if (!slot.item || slot.amount == 0 || slot.item != item)
            {
                continue;
            }

            if (slot.amount + amount <= item.maxStackSize)
            {
                slot.amount += amount;
                return 0;
            } 
            else
            {
                slot.amount = item.maxStackSize;
                amount -= item.maxStackSize - slot.amount;
            }
        }

        // Fill empty slots
        foreach (ItemSlot slot in itemSlots)
        {
            if (!slot.item)
            {
                slot.item = item;
                if (amount <= item.maxStackSize)
                {
                    
                    slot.amount = amount;
                    return 0;
                }
                else
                {
                    slot.amount = item.maxStackSize;
                    amount -= item.maxStackSize;
                }
            }
        }

        return amount;
    }
}
