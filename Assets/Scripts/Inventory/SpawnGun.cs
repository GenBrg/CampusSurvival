using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGun : ISpawnItem
{
    public GunPrototype gunPrototype;
    public int initialAmmoNum;

    public override Item SpawnItem()
    {
        return new Gun(gunPrototype, initialAmmoNum);
    }
}
