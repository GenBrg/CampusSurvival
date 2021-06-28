using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Ammo")]
public class AmmoPrototype : ItemPrototype
{
    public GameObject bullet;
    public float initialSpeed = 100.0f;
    public float lifeTime = 3.0f;

    public GameObject bulletImpact;
}
