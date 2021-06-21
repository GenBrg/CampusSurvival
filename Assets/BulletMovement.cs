using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float initialSpeed = 100.0f;
    public float lifeTime = 3.0f;

    public GameObject bulletImpact;

    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
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
        print(other.name);
        Instantiate(bulletImpact, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
