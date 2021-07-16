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
            && GetTechRequirement(level).CheckRequirements();
    }

    public bool CheckLevel(int level)
    {
        return level >= 1 && level <= MaxLevel;
    }

    public bool TryConsumeRequirement(int level)
    {
        return GetMaterialRequirement(level).TryConsumeRequirement();
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

    public string ToString(int level)
    {
        if (!CheckLevel(level))
        {
            return "";
        }

        StringBuilder sb = new StringBuilder();
        
        sb.AppendLine(GetMaterialRequirement(level).ToString());
        sb.AppendLine(GetTechRequirement(level).ToString());
        sb.AppendLine();

        return sb.ToString();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < MaxLevel; ++i)
        {
            sb.AppendLine("Level: " + (i + 1));
            sb.AppendLine(ToString(i + 1));
        }

        return sb.ToString();
    }
}
