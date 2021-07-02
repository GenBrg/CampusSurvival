using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class ActiveCollider : MonoBehaviour
{
    public bool isColliding;

    private void OnCollisionEnter(Collision collision)
    {
        isColliding = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        isColliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }
}
