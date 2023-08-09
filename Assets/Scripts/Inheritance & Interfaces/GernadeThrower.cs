using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GernadeThrower : MonoBehaviour, IWeapon
{
    public static event Action<GernadeThrower> OnThrow;

    [Tooltip("Gernade object prefab")]
    [SerializeField] protected GameObject gernade;
    [Tooltip("Where the gernade will spawn")]
    [SerializeField] public GameObject spawnPoint;

    protected float throwForce; //throwForce = direction.magnitude * multiplier;
    [Header("--Throwing Variables--")]
    [SerializeField] [Range(0.5f, 5.0f)]
        protected float multiplier;
    [SerializeField] protected bool scaleWithPlayerMovement; //whether moving characters affect gernade ballistic
    [SerializeField] [Range(0f, 1.0f)]
        protected float scaleWithPlayerMovementMultiplier; // and how much

    [Header("--Weapon default Variables--")]
    public int GernadeCount;
    [SerializeField] protected float DefaultRateOfFire; //Rounds per Minute

    protected float currentRateOfFire; //With modifiers applied
    protected float delayBetweenThrow; //Reload between each shot, defined by currentRateOfFire
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
                delayBetweenThrow = 1.0f / (currentRateOfFire / 60.0f);
            }
            else
            {
                delayBetweenThrow = 0.0f;
            }
        }
    }

    void Start()
    {
        currentRateOfFire = DefaultRateOfFire;
        if (currentRateOfFire != 0)
        {
            delayBetweenThrow = 1.0f / (currentRateOfFire / 60.0f);
        }
        else
        {
            delayBetweenThrow = 0.0f;
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
        Plane plane = new Plane(Vector3.up, this.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(input);

        plane.Raycast(ray, out float distance);

        return ray.GetPoint(distance);
    }

    public virtual void Trigger(Vector3 coordinate) //Attempt to shoot the gun, check delayTimer and stuff
    {
        if (GernadeCount > 0 && delayTimer <= 0.0f)
        {
            Fire(coordinate);
        }
    }

    public virtual void Fire(Vector3 coordinate) //Weapon shooting, spawn bullet and stuff, defined by individual weapon
    {
        if (OnThrow != null)
            OnThrow(this);
        GernadeCount--;
        delayTimer = delayBetweenThrow;
    }
}
