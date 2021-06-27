using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Prototype for an item in the inventory
 * 
 * @author Jiasheng Zhou
 */
public abstract class IItem
{
    public abstract string Name
    {
        get;
    }

    public abstract string Description
    {
        get;
    }

    public abstract Sprite Icon
    {
        get;
    }

    public abstract int MaxStackSize
    {
        get;
    }

    public abstract GameObject Model
    {
        get;
    }

    public virtual void OnEquip()
    {

    }

    public virtual void OnHandUpdate()
    {
        
    }

    public virtual void OnUnequip()
    {

    }
}
