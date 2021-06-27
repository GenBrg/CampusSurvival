using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : IItem
{
    public GunPrototype prototype;
    public int ammo;

    private InputManager input;
    private Backpack backpack;
    private RateLimiter fireRateLimiter;

    private GameObject model;
    private Transform muzzleOffset;
    private ParticleSystem muzzleFlash;

    public int Ammo
    {
        get => ammo;
        set {
            ammo = value;
        }
    }

    public override string Name => prototype.name;

    public override string Description => prototype.description;

    public override Sprite Icon => prototype.icon;

    public override int MaxStackSize => prototype.maxStackSize;

    public override GameObject Model => prototype.model;

    public Gun(GunPrototype prototype, int initialAmmo)
    {
        this.prototype = prototype;
        ammo = initialAmmo;

        fireRateLimiter = new RateLimiter(prototype.fireInterval, Fire);
    }

    public override void OnEquip()
    {
        base.OnEquip();

        model = GameObject.Instantiate(prototype.model, GameObject.Find("Weapon Socket").GetComponent<Transform>());
        muzzleOffset = model.transform.Find("Muzzle");
        muzzleFlash = model.GetComponentInChildren<ParticleSystem>();

        input = Object.FindObjectOfType<InputManager>();
        backpack = Object.FindObjectOfType<Backpack>();
    }

    void Reload()
    {
        if (Ammo == prototype.magazineSize)
        {
            return;
        }

        if (backpack.UseItem(prototype.Ammo))
        {
            // TODO Play reload sound
            Ammo = prototype.magazineSize;
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
        // TODO play fire sound
        GameObject.Instantiate(prototype.Ammo.bullet, muzzleOffset.position, muzzleOffset.rotation);
    }

    void Aim()
    {

    }

    public override void OnHandUpdate()
    {
        base.OnHandUpdate();

        if (input.Reload)
        {
            Reload();
        }

        if (!prototype.semiAuto && input.AutoFire1)
        {
            Fire();
        } else if (prototype.semiAuto && input.SemiFire1)
        {
            Fire();
        }

        if (input.Fire2)
        {
            Aim();
        }
    }

    public override void OnUnequip()
    {
        base.OnUnequip();

        GameObject.Destroy(model);
        model = null;
        muzzleOffset = null;
        muzzleFlash = null;
    }
}
