using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

[System.Serializable]
public class ItemRequirement
{
    public MaterialRequirement[] materialRequirements;
    public TechRequirement[] techRequirements;

    public int MaxLevel
    {
        get => materialRequirements.Length;
    }

    public bool CheckRequirement(int level)
    {
        return GetMaterialRequirement(level).CheckRequirement()
            && GetTechRequirement(level).CheckRequirement();
    }

    public bool CheckLevel(int level)
    {
        return level >= 1 && level <= MaxLevel;
    }

    public MaterialRequirement GetMaterialRequirement(int level)
    {
        if (!CheckLevel(level))
        {
            return null;
        }

        return materialRequirements[level - 1];
    }

    public TechRequirement GetTechRequirement(int level)
    {
        if (!CheckLevel(level))
        {
            return null;
        }

        return techRequirements[level - 1];
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < MaxLevel; ++i)
        {
            sb.AppendLine("Level: " + (i + 1));
            sb.AppendLine(materialRequirements[i].ToString());
            sb.AppendLine(techRequirements[i].ToString());
            sb.AppendLine();
        }

        return sb.ToString();
    }

    public string FirstLevelToString()
    {
        StringBuilder sb = new StringBuilder();

        if (MaxLevel >= 1)
        {
            sb.AppendLine(materialRequirements[1].ToString());
            sb.AppendLine(techRequirements[1].ToString());
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
