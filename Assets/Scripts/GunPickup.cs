using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : ItemPickup
{
    public int initialAmmo;

    protected override IItem SpawnItem()
    {
        return new Gun(prototype as GunPrototype, initialAmmo);
    }
}
