using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

[System.Serializable]
public class FireHelper
{
    public Func<bool> prefireCheck;
    public Transform firePoint;
    public ParticleSystem muzzleFlash;
    public RandomAudioPlayer fireAudioPlayer;
    public GameObject owner;
    public GameObject bulletPrefab;
    
    public void Fire()
    {
        if (prefireCheck != null && !prefireCheck())
        {
            return;
        }

        muzzleFlash.Play();

        GameObject bullet = GameObject.Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<BulletMovement>().Owner = owner;
        fireAudioPlayer.Play();
    }
}
