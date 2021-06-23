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
    public Item item;
    public int amount;

    private Backpack backpack;

    // Start is called before the first frame update
    void Start()
    {
        backpack = FindObjectOfType<Backpack>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
