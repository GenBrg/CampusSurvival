using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public AmmoPrototype prototype;

    private GameObject _owner;

    private float initialSpeed = 100.0f;
    private float lifeTime = 3.0f;
    private GameObject bulletImpact;

    private Vector3 velocity;

    public GameObject Owner
    {
        get => _owner;
        set => _owner = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        initialSpeed = prototype.initialSpeed;
        lifeTime = prototype.lifeTime;
        bulletImpact = prototype.bulletImpact;
        velocity = transform.forward * initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime <= Time.deltaTime)
        {
            transform.position += velocity * lifeTime;
            Destroy(gameObject);
        }

        lifeTime -= Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(bulletImpact, transform.position, transform.rotation);
        Damagable[] damagables = other.gameObject.GetComponents<Damagable>();
        foreach (Damagable damagable in damagables)
        {
            damagable.OnDamage(prototype.damage);
        }
        Destroy(gameObject);
    }
}
