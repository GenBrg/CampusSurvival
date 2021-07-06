using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEnemyBehavior : MonoBehaviour
{
    public abstract void Fire();
    public abstract void Die();
    public abstract void OnChase();
    public abstract void OnStopChase();
}
