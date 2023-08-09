using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffExample : MonoBehaviour
{
    private void OnThingHappened(Gun gun)
    {
        // any logic that responds to event goes here
        Debug.Log("Observer responds");
        gun.AmmoCount++;
    }

    private void OnThingHappened(RaycastGun gun)
    {
        // any logic that responds to event goes here
        Debug.Log("Observer responds");
        gun.AmmoCount++;
    }

    private void OnThingHappened(GernadeThrower gernadeThrower)
    {
        // any logic that responds to event goes here
        Debug.Log("Observer responds");
        gernadeThrower.GernadeCount++;
    }

    private void Awake()
    {
        Gun.OnShoot += OnThingHappened;
        RaycastGun.OnShoot += OnThingHappened;
        GernadeThrower.OnThrow += OnThingHappened;
    }

    private void OnDestroy()
    {
        Gun.OnShoot -= OnThingHappened;
        RaycastGun.OnShoot -= OnThingHappened;
        GernadeThrower.OnThrow -= OnThingHappened;
    }
}
