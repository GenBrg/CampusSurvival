using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEnemyBehavior : MonoBehaviour
{
    public abstract bool IsDead { get;  }

    public abstract void Fire();
    public abstract void Die();
    public abstract void OnChase();
    public abstract void OnStopChase();
    public abstract void OnMoving();
    public abstract void OnNotMoving();
}
