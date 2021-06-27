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

        input = InputManager.Instance;
        backpack = Backpack.Instance;
        fireRateLimiter = new RateLimiter(prototype.fireInterval, Fire);
    }

    public override void OnEquip()
    {
        base.OnEquip();
    }

    void Reload()
    {
        if (Ammo == prototype.magazineSize)
        {
            return;
        }

        if (backpack.UseItem(ammoName))
        {
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

        prototype.muzzleFlash.Play();
        // Audio
        GameObject.Instantiate(prototype.bullet, prototype.muzzleOffset.position, prototype.muzzleOffset.rotation);
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
    }
}
