using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureRequirement : MonoBehaviour
{
    public MaterialRequirement[] materialRequirements;
    public string configPath;

    private void Awake()
    {
        //JsonUtility.FromJsonOverwrite(, this);
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
