using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Structure")]
public class StructurePrototype : ScriptableObject
{
    public string name;
    public string description;
    public ItemRequirement requirement;
    public TechRequirement[] fulfillment;
    public int[] maxHP;
    public GameObject structureModel;
    public GameObject structurePrefab;
}
