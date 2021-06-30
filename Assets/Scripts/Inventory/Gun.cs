using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    private Transform cameraTransform;

    private UnityAction onAmmoNumChange;
    private HUD hud;

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

    public override string Name => prototype.name;

    public override string Description => prototype.description;

    public override Sprite Icon => prototype.icon;

    public override int MaxStackSize => prototype.maxStackSize;

    public override GameObject Model => prototype.model;

    public float Range
    {
        get
        {
            return prototype.Ammo.initialSpeed * prototype.Ammo.lifeTime;
        }
    }

    public Gun(GunPrototype prototype, int initialAmmo)
    {
        this.prototype = prototype;
        ammo = initialAmmo;

        cameraTransform = GameObject.Find("Main Camera").GetComponent<Transform>();
        input = Object.FindObjectOfType<InputManager>();
        backpack = Object.FindObjectOfType<Backpack>();
        hud = Object.FindObjectOfType<HUD>();

        onAmmoNumChange += () =>
        {
            hud.ShowAmmo(ammo, prototype.magazineSize);
        };

        fireRateLimiter = new RateLimiter(prototype.fireInterval, Fire);
    }

    public override void OnEquip()
    {
        base.OnEquip();

        model = GameObject.Instantiate(prototype.model, GameObject.Find("Weapon Socket").GetComponent<Transform>());
        muzzleOffset = model.transform.Find("Muzzle");
        muzzleFlash = model.GetComponentInChildren<ParticleSystem>();

        if (onAmmoNumChange != null)
        {
            onAmmoNumChange();
        }
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

        return false;
    }

    public override void OnUnequip()
    {
        base.OnUnequip();

        GameObject.Destroy(model);
        model = null;
        muzzleOffset = null;
        muzzleFlash = null;

        hud.HideAmmo();
    }
}
