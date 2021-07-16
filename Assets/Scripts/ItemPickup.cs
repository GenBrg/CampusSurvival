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
    public AudioClip pickupSound;
    
    private Backpack backpack;
    private bool _destroyed = false;

    public bool Destroyed
    {
        get => _destroyed;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        backpack = FindObjectOfType<Backpack>();
        Destroy(gameObject, remainTime);
        Debug.Assert(spawnItem != null || item.Valid);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Destroyed)
        {
            return;
        }

        if (other.GetComponent<CharacterMovement>())
        {
            // Pickup items
            if (item == null || !item.Valid)
            {
                item = spawnItem.SpawnItem();
            }

            int origAmount = amount;
            amount = backpack.AddItem(item, amount);

            if (amount < origAmount)
            {
                GameManager.PlaySound(pickupSound);
            }

            if (amount == 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // Merge same items
            ItemPickup itemPickup = other.GetComponent<ItemPickup>();
            if (itemPickup && !itemPickup.Destroyed && itemPickup.item.Equals(item))
            {
                amount += itemPickup.amount;
                Destroy(itemPickup.gameObject);
                itemPickup._destroyed = true;
            }
        }
    }
}
