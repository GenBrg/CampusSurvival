using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {

    }

    //public bool CheckRequirement(int level)
    //{
    //    //MaterialRequirement matReq 


    //}

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
}
