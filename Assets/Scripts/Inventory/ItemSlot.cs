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
    public IItem holdingItem;
    public int amount;

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

    public IItem HoldingItem
    {
        get => holdingItem;
        set
        {
            if (value == null)
            {
                ClearSlot();
            }
            else
            {
                holdingItem = value;
                itemIcon.sprite = value.Icon;
                Color c = itemIcon.color;
                c.a = 1.0f;
                itemIcon.color = c;
            }
        }
    }

    public bool IsEmpty
    {
        get => holdingItem == null || amount == 0;
    }

    public bool IsFull
    {
        get => !IsEmpty && (HoldingItem.MaxStackSize == Amount);
    }

    private void ClearSlot()
    {
        amount = 0;
        holdingItem = null;
        itemIcon.sprite = null;
        amountText.text = "";

        Color c = itemIcon.color;
        c.a = 0.0f;
        itemIcon.color = c;
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
    public int AddItem(IItem item, int amountToAdd)
    {
        if ((!IsEmpty && !item.Equals(HoldingItem)) || amountToAdd == 0 || item == null)
        {
            return 0;
        }

        HoldingItem = item;
        amountToAdd = Mathf.Min(item.MaxStackSize - Amount, amountToAdd);
        Amount += amountToAdd;

        return amountToAdd;
    }

    public void Drop(int amountToDrop)
    {
        IItem itemToDrop = HoldingItem;
        amountToDrop = DeductItem(amountToDrop);

        // TODO Project items from player
    }

    public void DropAll()
    {
        Drop(Amount);
    }

    public void TransferTo(ItemSlot slot, int amountToTransfer)
    {
        IItem itemToTransfer = HoldingItem;
        amountToTransfer = DeductItem(amountToTransfer);
        amountToTransfer -= slot.AddItem(itemToTransfer, amountToTransfer);
        AddItem(itemToTransfer, amountToTransfer);
    }

    public void TransferAllTo(ItemSlot slot)
    {
        TransferTo(slot, Amount);
    }
}
