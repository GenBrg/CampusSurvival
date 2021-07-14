using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Prototype for an item in the inventory
 * 
 * @author Jiasheng Zhou
 */
[System.Serializable]
public class Item
{
    public ItemPrototype prototype;

    private static IDictionary<ItemPrototype, Item> cachedItems = new Dictionary<ItemPrototype, Item>();

    public string Name => prototype.name;

    public string Description => prototype.description;

    public Sprite Icon => prototype.icon;

    public int MaxStackSize => prototype.maxStackSize;

    public GameObject Model => prototype.model;

    public GameObject Pickup => prototype.pickup;

    public bool Valid => prototype != null;

    public virtual void OnEquip() { }
    public virtual bool OnHandUpdate() { return false; }
    public virtual void OnUnequip() { }

    protected Item(ItemPrototype prototype)
    {
        this.prototype = prototype;
    }

    public static Item SpawnItem(ItemPrototype prototype)
    {
        if (!cachedItems.ContainsKey(prototype))
        {
            cachedItems[prototype] = new Item(prototype);
        }

        return cachedItems[prototype];
    }

    public override bool Equals(object other)
    {
        if (other is ItemPrototype)
        {
            return other.Equals(prototype);
        }

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
