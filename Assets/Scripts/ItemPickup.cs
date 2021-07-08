using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

/** 
 * A stack of items to be picked up in the scene
 * 
 * @author Jiasheng Zhou
 */
public class ItemPickup : MonoBehaviour
{
    public int amount;
    public Item item;
    public ISpawnItem spawnItem;
    public float remainTime = 30.0f;
    
    private Backpack backpack;
    
    // Start is called before the first frame update
    void Start()
    {
        backpack = FindObjectOfType<Backpack>();
        DestroyObject(gameObject, remainTime);
        Debug.Assert(spawnItem != null || item.Valid);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterMovement>())
        {
            if (item == null || !item.Valid)
            {
                item = spawnItem.SpawnItem();
            }

            amount = backpack.AddItem(item, amount);

            if (amount == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
