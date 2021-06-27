using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Gun")]
public class GunPrototype : ItemPrototype
{
    public int magazineSize;
    public float damage;
    public float fireInterval;
    public bool semiAuto;

    public AmmoPrototype Ammo;
} 
