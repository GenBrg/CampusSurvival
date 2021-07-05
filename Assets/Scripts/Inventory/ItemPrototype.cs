using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Item/Item")]
public class ItemPrototype : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite icon;
    public int maxStackSize;
    public GameObject model;
    public GameObject pickup;
}
