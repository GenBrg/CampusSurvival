using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureRequirement : ScriptableObject
{
    public MaterialRequirement[] materialRequirements;


    private void Awake()
    {

    }

    public MaterialRequirement GetMaterialRequirement(int level)
    {
        if (level < 1 || level > materialRequirements.Length)
        {
            return null;
        }

        return materialRequirements[level - 1];
    }
}
