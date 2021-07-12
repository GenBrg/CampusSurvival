using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IStructure : MonoBehaviour
{
    public StructurePrototype prototype;
    public int level;
    public int hp;

    public string Name
    {
        get => prototype.name;
    }

    public string Description
    {
        get => prototype.description;
    }

    public ItemRequirement Requirement
    {
        get => prototype.requirement;
    }


    public void LevelUp()
    {

    }
}
