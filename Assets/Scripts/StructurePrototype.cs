using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Structure")]
public class StructurePrototype : ScriptableObject
{
    public new string name;
    public string description;
    public ItemRequirement requirement;
    public TechRequirement[] fulfillment;
    public int[] maxHP;
    public GameObject structureModel;
    public GameObject structurePrefab;

    public int GetMaxHP(int level)
    {
        return maxHP[level - 1];
    }

    public TechRequirement GetFulfillment(int level)
    {
        return fulfillment[level - 1];
    }
}
