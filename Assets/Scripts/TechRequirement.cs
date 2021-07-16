using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

[System.Serializable]
public class TechRequirement
{
    public TechTree.Tech[] techs;

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder("Tech: ");
        sb.AppendLine(string.Join(", ", techs));
        return sb.ToString();
    }

    public bool CheckRequirements()
    {
        foreach (TechTree.Tech tech in techs)
        {
            if (!TechTree.Instance.IsUnlocked(tech))
            {
                return false;
            }
        }

        return true;
    }

    public void FulfillRequirements()
    {
        foreach (TechTree.Tech tech in techs)
        {
            TechTree.Instance.Unlock(tech);
        }
    }
}
