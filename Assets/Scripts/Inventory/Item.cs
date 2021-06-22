using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Prototype for an item in the inventory
 * 
 * @author Jiasheng Zhou
 */
[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public string name;
    public string description;
    public Sprite icon;
    public GameObject model;
    public int maxStackSize;
}
