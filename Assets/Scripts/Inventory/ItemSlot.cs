using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine.UI;
using UnityEngine;

/**
 * A slot in an item container(backpack / hotbar, etc)
 * 
 * @author Jiasheng Zhou
 */
public class ItemSlot : MonoBehaviour
{
    public Item holdingItem;
    public int amount;
    public bool enabled;

    private Image itemIcon;
    private TextMeshProUGUI amountText;

    public int Amount
    {
        get => amount;
        set
        {
            if (value <= 0)
            {
                ClearSlot();
            }
            else
            {
                amount = value;
                amountText.text = "x" + value;
            }
        }
    }

    public Item HoldingItem
    {
        get => holdingItem;
        set
        {
            if (!value)
            {
                ClearSlot();
            }
            else
            {
                holdingItem = value;
                itemIcon.sprite = value.icon;
            }
        }
    }

    public bool IsEmpty
    {
        get => !holdingItem || amount == 0;
    }

    public bool IsFull
    {
        get => !IsEmpty && (HoldingItem.maxStackSize == Amount);
    }

    private void ClearSlot()
    {
        amount = 0;
        holdingItem = null;
        itemIcon.sprite = null;
        amountText.text = "";
    }

    private void Awake()
    {
        itemIcon = GetComponentsInChildren<Image>()[1];
        amountText = GetComponentInChildren<TextMeshProUGUI>();

        ClearSlot();
    }

    // @return amount actually deducted from the item slot.
    public int DeductItem(int amountToDeduct)
    {
        if (IsEmpty || amountToDeduct == 0)
        {
            return 0;
        }

        amountToDeduct = Mathf.Min(Amount, amountToDeduct);

        if (amountToDeduct == Amount)
        {
            ClearSlot();
        }
        else
        {
            Amount -= amountToDeduct;
        }

        return amountToDeduct;
    }

    // @return amount actually added to the item slot
    public int AddItem(Item item, int amountToAdd)
    {
        if ((!IsEmpty && item != HoldingItem) || amountToAdd == 0 || !item)
        {
            return 0;
        }

        HoldingItem = item;
        amountToAdd = Mathf.Min(item.maxStackSize - Amount, amountToAdd);
        Amount += amountToAdd;

        return amountToAdd;
    }

    public void Drop(int amountToDrop)
    {
        Item itemToDrop = HoldingItem;
        amountToDrop = DeductItem(amountToDrop);

        // TODO Project items from player
    }

    public void TransferTo(ItemSlot slot, int amountToTransfer)
    {
        Item itemToTransfer = HoldingItem;
        amountToTransfer = DeductItem(amountToTransfer);
        amountToTransfer -= slot.AddItem(itemToTransfer, amountToTransfer);
        AddItem(itemToTransfer, amountToTransfer);
    }
}
