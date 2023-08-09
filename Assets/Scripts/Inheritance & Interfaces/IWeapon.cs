using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface IWeapon
{
    void Trigger(Vector3 parameter);
    void Fire(Vector3 parameter);
}
