using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : IItem
{
    public ItemPrototype prototype;

    public override string Name => prototype.name;

    public override string Description => prototype.description;

    public override Sprite Icon => prototype.icon;

    public override int MaxStackSize => prototype.maxStackSize;

    public override GameObject Model => prototype.model;

    public Item(ItemPrototype prototype)
    {
        this.prototype = prototype;
    }

    public override bool Equals(object other)
    {
        if (!(other is Item))
        {
            return false;
        }

        Item otherItem = other as Item;
        return otherItem.prototype == prototype;
    }

    public override int GetHashCode()
    {
        return prototype.GetHashCode();
    }
}
