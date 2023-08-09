using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RaycastGun : MonoBehaviour, IWeapon
{
    public static event Action<RaycastGun> OnShoot;

    public GameObject a;

    [Tooltip("Where the bullet will spawn")]
    [SerializeField] public GameObject spawnPoint;

    [Header("--Weapon default Variables--")]
    [SerializeField] public float AmmoCount;
    [SerializeField] protected float DefaultRateOfFire; //Rounds per Minute
    [SerializeField] public float damage; //damage
    [SerializeField] public float range;

    protected float currentRateOfFire; //With modifiers applied
    protected float delayBetweenShots; //Reload between each shot, defined by currentRateOfFire
    protected float delayTimer; //timer

    public float CurrentRateOfFire
    {
        get { return currentRateOfFire; }
        set
        {
            currentRateOfFire = value;
            //recalculate delay between shots
            if (currentRateOfFire != 0)
            {
                delayBetweenShots = 1.0f / (currentRateOfFire / 60.0f);
            }
            else
            {
                delayBetweenShots = 0.0f;
            }
        }
    }

    void Start()
    {
        currentRateOfFire = DefaultRateOfFire;
        if (currentRateOfFire != 0)
        {
            delayBetweenShots = 1.0f / (currentRateOfFire / 60.0f);
        }
        else
        {
            delayBetweenShots = 0.0f;
        }
        delayTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (delayTimer >= 0f)
        {
            delayTimer -= Time.deltaTime;
        }

        ChildUpdate();
    }

    protected virtual void ChildUpdate()
    {

    }

    public virtual Vector3 ProcessInput(Vector3 input) //Default mouse input processing, translating screen point to world
    {
        Plane plane = new Plane(Vector3.up, this.gameObject.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(input);

        plane.Raycast(ray, out float distance);

        return ray.GetPoint(distance);
    }

    public virtual void Trigger(Vector3 direction) //Attempt to shoot the gun, check delayTimer and stuff
    {
        if (AmmoCount > 0 && delayTimer <= 0.0f)
        {
            Fire(direction);
        }
    }

    public virtual void Fire(Vector3 direction) //Weapon shooting, spawn bullet and stuff, defined by individual weapon
    {
        if (OnShoot != null)
            OnShoot(this);
        AmmoCount--;
        delayTimer = delayBetweenShots;
    }
}
