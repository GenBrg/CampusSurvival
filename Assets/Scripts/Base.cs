using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public int level = 1;
    public int healAmount = 10;
    public float healInterval = 3;

    private RateLimiter healRateLimiter;
    private Health health;

    void HealPlayer()
    {
        health.Heal(healAmount);
    }

    // Start is called before the first frame update
    void Awake()
    {
        healRateLimiter = new RateLimiter(healInterval, HealPlayer);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterMovement>() && level >= 2)
        {
            health = other.gameObject.GetComponent<Health>();
            healRateLimiter.Invoke();
        }
    }
}
