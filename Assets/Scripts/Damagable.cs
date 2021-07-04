using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public abstract class Damagable : MonoBehaviour
{
    public abstract void OnDamage(float damage, GameObject attacker);
}
