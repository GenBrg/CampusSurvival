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
    private InputManager input;

    // Start is called before the first frame update
    void Start()
    {
        fireRateLimiter = new RateLimiter(fireInterval, Fire);
        input = FindObjectOfType<InputManager>();
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

        if (input.AutoFire1)
        {
            fireRateLimiter.Invoke();
        }
    }

}
