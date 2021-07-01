using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * A stack of items to be picked up in the scene
 * 
 * @author Jiasheng Zhou
 */
public class ItemPickup : MonoBehaviour
{
    public ItemPrototype prototype;
    public int amount;

    private Backpack backpack;
    private IItem item;

    // Start is called before the first frame update
    void Awake()
    {
        backpack = FindObjectOfType<Backpack>();
        item = SpawnItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual IItem SpawnItem()
    {
        return new Item(prototype);
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
