using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

/** 
 * A stack of items to be picked up in the scene
 * 
 * @author Jiasheng Zhou
 */
public class ItemPickup : MonoBehaviour
{
    public int amount;
    public Item item;
    
    private Backpack backpack;
    
    // Start is called before the first frame update
    void Awake()
    {
        backpack = FindObjectOfType<Backpack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterMovement>())
        {
            amount = backpack.AddItem(item, amount);

            if (amount == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
