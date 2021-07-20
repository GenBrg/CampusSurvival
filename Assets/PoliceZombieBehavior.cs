using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceZombieBehavior : ZombieBehavior
{
    public FireHelper fireHelper;

    protected override void FireImpl()
    {
        fireHelper.Fire();
    }

    private void Update()
    {
        fireHelper.firePoint.LookAt(CharacterMovement.Instance.transform.position + Vector3.up * 0.6f);
    }
}
