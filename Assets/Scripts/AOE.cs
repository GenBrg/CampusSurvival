using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : MonoBehaviour
{
    public float effectStartTime;
    public float effectEndTime;
    public float damage;
    public GameObject owner;
    
    private float effectSpawnTime;
    private ISet<Collider> considered;

    // Start is called before the first frame update
    void Start()
    {
        effectSpawnTime = Time.time;
        considered = new HashSet<Collider>();
        Destroy(gameObject, effectEndTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Time.time < effectSpawnTime + effectStartTime)
        {
            return;
        }

        if (considered.Contains(other))
        {
            return;
        }

        considered.Add(other);
        Damagable[] damagables = other.GetComponentsInChildren<Damagable>();

        foreach (Damagable damagable in damagables)
        {
            damagable.OnDamage(damage, owner);
        }
    }
}
