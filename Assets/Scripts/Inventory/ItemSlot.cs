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
    [SerializeReference]
    public Item holdingItem;
    public int amount;

    private Image itemIcon;
    private TextMeshProUGUI amountText;

    private const float dropSpeed = 3.0f;
    private const float dropOffset = 1.0f;

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
        if (this == Hotbar.Instance.CurrentSlot && HoldingItem != null)
        {
            HoldingItem.OnUnequip();
        }

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
    public int AddItem(Item item, int amountToAdd)
    {
        if ((!IsEmpty && !item.Equals(HoldingItem)) || amountToAdd == 0 || item == null)
        {
            return 0;
        }

        if (IsEmpty && this == Hotbar.Instance.CurrentSlot)
        {
            item.OnEquip();
        }

        HoldingItem = item;
        amountToAdd = Mathf.Min(item.MaxStackSize - Amount, amountToAdd);
        Amount += amountToAdd;

        return amountToAdd;
    }

    public void Drop(int amountToDrop)
    {
        Item itemToDrop = HoldingItem;
        amountToDrop = DeductItem(amountToDrop);

        if (amountToDrop == 0)
        {
            return;
        }

        Transform playerTransform = CharacterMovement.Instance.transform;
        GameObject pickupObj = Instantiate(itemToDrop.Pickup, playerTransform.position + playerTransform.forward * dropOffset, playerTransform.rotation);
        ItemPickup pickup = pickupObj.GetComponent<ItemPickup>();
        Rigidbody pickupRB = pickupObj.GetComponent<Rigidbody>();

        pickup.amount = amountToDrop;
        pickup.item = itemToDrop;
        pickupRB.velocity = playerTransform.forward * dropSpeed;
    }

    public void DropAll()
    {
        Drop(Amount);
    }

    public void TransferTo(ItemSlot slot, int amountToTransfer)
    {
        Item itemToTransfer = HoldingItem;
        amountToTransfer = DeductItem(amountToTransfer);
        amountToTransfer -= slot.AddItem(itemToTransfer, amountToTransfer);
        AddItem(itemToTransfer, amountToTransfer);
    }

    public void TransferAllTo(ItemSlot slot)
    {
        TransferTo(slot, Amount);
    }
}
