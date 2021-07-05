using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Gun : Item
{
    public int ammo;
    
    private InputManager input;
    private Backpack backpack;
    private RateLimiter fireRateLimiter;

    private GameObject model;
    private Transform muzzleOffset;
    private ParticleSystem muzzleFlash;
    private Transform cameraTransform;

    private GunPrototype gunPrototype;
    private UnityAction onAmmoNumChange;
    private HUD hud;
    private GameObject player;
    private RandomAudioPlayer fireAudioPlayer;
    private RandomAudioPlayer reloadAudioPlayer;

    public int Ammo
    {
        get => ammo;
        set {
            ammo = value;
            if (onAmmoNumChange != null)
            {
                onAmmoNumChange();
            }
        }
    }

    public float Range
    {
        get
        {
            return gunPrototype.Ammo.initialSpeed * gunPrototype.Ammo.lifeTime;
        }
    }

    private void Start()
    {
        gunPrototype = prototype as GunPrototype;
        
        input = Object.FindObjectOfType<InputManager>();
        backpack = Object.FindObjectOfType<Backpack>();
        hud = Object.FindObjectOfType<HUD>();

        onAmmoNumChange += () =>
        {
            hud.ShowAmmo(ammo, gunPrototype.magazineSize);
        };

        fireRateLimiter = new RateLimiter(gunPrototype.fireInterval, Fire);
        player = GameObject.Find("Player");
    }

    public override void OnEquip()
    {
        base.OnEquip();
        cameraTransform = GameObject.Find("Main Camera").GetComponent<Transform>();
        model = GameObject.Instantiate(prototype.model, GameObject.Find("Weapon Socket").GetComponent<Transform>());
        fireAudioPlayer = model.GetComponentsInChildren<RandomAudioPlayer>()[0];
        reloadAudioPlayer = model.GetComponentsInChildren<RandomAudioPlayer>()[1];
        muzzleOffset = model.transform.Find("Muzzle");
        muzzleFlash = model.GetComponentInChildren<ParticleSystem>();

        if (onAmmoNumChange != null)
        {
            onAmmoNumChange();
        }
    }

    void Reload()
    {
        if (Ammo == gunPrototype.magazineSize)
        {
            return;
        }

        if (backpack.UseItem(gunPrototype.Ammo))
        {
            reloadAudioPlayer.Play();
            Ammo = gunPrototype.magazineSize;
        }
    }

    void Fire()
    {
        // Check ammo
        if (Ammo == 0)
        {
            if (input.SemiFire1)
            {
                Reload();
            }
            return;
        }

        --Ammo;

        muzzleFlash.Play();

        GameObject bullet = GameObject.Instantiate(gunPrototype.Ammo.bullet, muzzleOffset.position, muzzleOffset.rotation);
        bullet.GetComponent<BulletMovement>().Owner = player;
        fireAudioPlayer.Play();
    }

    void Aim()
    {

    }

    public override bool OnHandUpdate()
    {
        base.OnHandUpdate();

        bool hit = Physics.Raycast(new Ray(cameraTransform.position, cameraTransform.forward), out RaycastHit hitInfo, Range);
        if (hit)
        {
            model.transform.LookAt(hitInfo.point);
        }
        else
        {
            model.transform.LookAt(cameraTransform.position + cameraTransform.forward * Range);
        }

        if (input.Reload)
        {
            Reload();
        }

        if (!gunPrototype.semiAuto && input.AutoFire1)
        {
            Fire();
        } else if (gunPrototype.semiAuto && input.SemiFire1)
        {
            Fire();
        }

        if (input.Fire2)
        {
            Aim();
        }

        return false;
    }

    public override void OnUnequip()
    {
        base.OnUnequip();

        GameObject.Destroy(model);
        model = null;
        muzzleOffset = null;
        muzzleFlash = null;
        fireAudioPlayer = null;

        hud.HideAmmo();
    }
}
