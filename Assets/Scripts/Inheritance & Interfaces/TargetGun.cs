using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TargetGun : MonoBehaviour, IWeapon
{
    public static event Action<TargetGun> OnShoot;

    [Tooltip("Bullet object prefab")]
    [SerializeField] protected ExampleRocket rocket;
    [Tooltip("Where the bullet will spawn")]
    [SerializeField] public GameObject spawnPoint;

    [Header("--Weapon default Variables--")]
    [SerializeField] public float bulletSpeed; //how fast is the bullet travelling
    [SerializeField] public float AmmoCount;
    [SerializeField] protected float DefaultRateOfFire; //Rounds per Minute
    [SerializeField] public float damage; //damage
    [SerializeField] public float range; //range

    [Header("--Weapon targeting Variables--")]
    [Tooltip("Target snapping")]
    [SerializeField] public float sphereCastRadius; //how fast is the bullet travelling
    [Tooltip("Max distance possible from camera")]
    [SerializeField] public float raycastMaxDistance;

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

    public virtual GameObject ProcessInput(Vector3 input) //Default mouse input processing, translating screen point to world
    {
        Ray ray = Camera.main.ScreenPointToRay(input);
        Physics.Raycast(ray, out RaycastHit hit1, raycastMaxDistance);
        RaycastHit[] hit2 = Physics.SphereCastAll(hit1.point, sphereCastRadius, Vector3.up);

        foreach (RaycastHit hit in hit2)
        {
            ITargetable targetable = hit.collider.GetComponent<ITargetable>();
            if (targetable != null && targetable.IsTargetable)
            {
                return hit.collider.gameObject;
            }
        }

        return null;
    }

    public virtual void Trigger(Vector3 parameter) //Attempt to shoot the gun, check delayTimer and stuff
    {
        if (AmmoCount > 0 && delayTimer <= 0.0f)
        {
            Fire(parameter);
        }
    }

    public virtual void Fire(Vector3 parameter) //Weapon shooting, spawn bullet and stuff, defined by individual weapon
    {
        if (OnShoot != null)
            OnShoot(this);
        AmmoCount--;
        delayTimer = delayBetweenShots;
    }
}
