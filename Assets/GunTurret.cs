using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurret : IStructure
{
    public float detectionRadius;
    public float fireInterval;
    public float rotationSpeed;
    public RandomAudioPlayer fireSFX;
    public GameObject gunBullet;
    public Transform turretTransform;
    public Transform firePoint;

    private RateLimiter fireRateLimiter;
    private const float maxAngleError = 1.0f;

    private void Fire()
    {
        GameObject bullet = GameObject.Instantiate(gunBullet, firePoint.position, firePoint.rotation);
        bullet.GetComponent<BulletMovement>().Owner = gameObject;
        fireSFX.Play();
    }

    private void Awake()
    {
        base.Awake();
        fireRateLimiter = new RateLimiter(fireInterval, Fire);

    }

    protected override void OnLevelUp()
    {
        base.OnLevelUp();


    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            // Find closest enemy
            float closest = float.MaxValue;
            Vector3 closestDir = Vector3.zero;
            foreach (GameObject enemy in enemies)
            {
                Vector3 dir = enemy.transform.position - transform.position;
                float dist = dir.magnitude;
                if (dist < closest)
                {
                    closest = dist;
                    closestDir = dir;
                }
            }

            if (closest < detectionRadius)
            {
                // Align rotation
                if (Vector3.Angle(turretTransform.forward, closestDir) < maxAngleError)
                {
                    fireRateLimiter.Invoke();
                }
                else
                {
                    turretTransform.rotation = Quaternion.RotateTowards(turretTransform.rotation, Quaternion.LookRotation(closestDir, Vector3.up), Time.deltaTime * rotationSpeed);
                }
            }
        }
    }
}
