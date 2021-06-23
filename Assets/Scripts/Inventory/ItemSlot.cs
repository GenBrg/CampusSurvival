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

    private void ClearSlot()
    {
        amount = 0;
        holdingItem = null;
        itemIcon.sprite = null;
        amountText.text = "";
    }

    private void Start()
    {
        itemIcon = GetComponentsInChildren<Image>()[1];
        amountText = GetComponentInChildren<TextMeshProUGUI>();

        if (holdingItem)
        {
            itemIcon.sprite = holdingItem.icon;
            amountText.text = "x" + amount;
        } 
        else
        {
            itemIcon.sprite = null;
            amountText.text = "";
        }
    }
}
