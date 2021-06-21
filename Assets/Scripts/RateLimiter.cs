using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
 * A helper class that limits the rate of a function call
 */
public class RateLimiter
{
    private float lastInvokeTime;
    private float interval;
    private UnityAction action;

    // interval in seconds
    public RateLimiter(float interval, UnityAction action)
    {
        lastInvokeTime = (float)-1e10;
        this.interval = interval;
        this.action = action;
    }

    public bool Invoke()
    {
        if (Time.time - lastInvokeTime > interval)
        {
            action.Invoke();
            lastInvokeTime = Time.time;
            return true;
        } else
        {
            return false;
        }
    }
}
