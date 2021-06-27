using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public ItemSlot[] itemSlots;
    public Image selectionBox;

    private int index;
    private const int kHotbarSize = 10;
    private const int kSlotWidth = 110;
    

    void Awake()
    {
        index = 0;
    }

    void SwitchToIndex(int idx)
    {
        if (idx == index || idx < 0 || idx >= kHotbarSize)
        {
            return;
        }

        
        selectionBox.rectTransform.anchoredPosition = new Vector2(idx * kSlotWidth, 0.0f);

        // Unequip item
        if (!itemSlots[index].IsEmpty)
        {
            itemSlots[index].HoldingItem.OnUnequip();
        }

        // Equip item
        if (!itemSlots[idx].IsEmpty)
        {
            itemSlots[idx].HoldingItem.OnEquip();
        }

        index = idx;
    }

    // Positive a mod b
    int PositiveMod(int a, int b)
    {
        return (a % b + b) % b;
    }

    // Update is called once per frame
    void Update()
    {
        for (KeyCode k = KeyCode.Alpha0; k <= KeyCode.Alpha9; k++)
        {
            if (Input.GetKeyDown(k))
            {
                SwitchToIndex(PositiveMod(k - KeyCode.Alpha0 - 1, kHotbarSize));
                return;
            }
        }

        if (Input.mouseScrollDelta.y < 0.0f)
        {
            SwitchToIndex(PositiveMod(index + 1, kHotbarSize));
        }
        else if (Input.mouseScrollDelta.y > 0.0f)
        {
            SwitchToIndex(PositiveMod(index - 1, kHotbarSize));
        }

        if (!itemSlots[index].IsEmpty)
        {
            itemSlots[index].HoldingItem.OnHandUpdate();
        }
    }
}
