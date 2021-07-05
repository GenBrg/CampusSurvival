using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TechTree : MonoBehaviour
{
    private static TechTree _instance;
    public static TechTree Instance
    {
        get => _instance;
    }

    public enum Tech
    {
        BASE2,
        BASE3,
        RADIO_TOWER
    }

    private IDictionary<Tech, bool> status;

    private void Awake()
    {
        status = new Dictionary<Tech, bool>();
        foreach (Tech tech in Enum.GetValues(typeof(Tech)))
        {
            status[tech] = false;
        }
    }

    public void Unlock(Tech tech)
    {
        status[tech] = true;
    }

    public bool IsUnlocked(Tech tech)
    {
        return status[tech];
    }
}
