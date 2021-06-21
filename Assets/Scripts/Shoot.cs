using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float fireInterval = 0.1f;
    public Transform muzzle;
    public float range = 100.0f;
    public GameObject bullet;
    public ParticleSystem muzzleFlash;

    private RateLimiter fireRateLimiter;

    // Start is called before the first frame update
    void Start()
    {
        fireRateLimiter = new RateLimiter(fireInterval, Fire);
    }

    private void Fire()
    {
        muzzleFlash.Play();
        Instantiate(bullet, muzzle.position, muzzle.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        bool hit = Physics.Raycast(new Ray(transform.parent.position, transform.parent.forward), out RaycastHit hitInfo, range);
        if (hit)
        {
            transform.LookAt(hitInfo.point);
        } else
        {
            transform.LookAt(transform.parent.position + transform.parent.forward * range);
        }

        if (Input.GetButton("Fire1"))
        {
            fireRateLimiter.Invoke();
        }
    }

}
