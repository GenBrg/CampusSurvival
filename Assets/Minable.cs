using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Minable : MonoBehaviour
{
    public ItemPrototype itemPrototype;
    public int remainingAmount = 10;
    public int minMineAmount = 2;
    public int maxMineAmount = 4;
    public RandomAudioPlayer mineSFX;
    public UnityEvent onMined;

    private void OnMine()
    {
        int minedAmount = Mathf.Min(Random.Range(minMineAmount, maxMineAmount), remainingAmount);
        minedAmount = minedAmount - Backpack.Instance.AddItem(Item.SpawnItem(itemPrototype), minedAmount);

        remainingAmount -= minedAmount;

        if (minedAmount > 0)
        {
            mineSFX.Play();
        }

        if (remainingAmount <= 0)
        {
            Destroy(gameObject);
            onMined.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(minMineAmount <= maxMineAmount);
    }

    private void OnTriggerEnter(Collider other)
    {
        AOE aoe = other.gameObject.GetComponent<AOE>();
        if (aoe)
        {
            if (aoe.type == AOE.Type.PUNCH && aoe.owner.tag == "Player")
            {
                OnMine();
            }
        }
    }
}
