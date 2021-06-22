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
    public Item item;
    public int amount;
    public bool enabled;

    private Image itemIcon;
    private TextMeshProUGUI amountText;

    private void Start()
    {
        itemIcon = GetComponentsInChildren<Image>()[1];
        amountText = GetComponentInChildren<TextMeshProUGUI>();

        itemIcon.sprite = item.icon;
        amountText.text = "x666";
    }
}
