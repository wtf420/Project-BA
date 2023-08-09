using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleRocketLauncher : TargetGun
{
    public override void Trigger(Vector3 coordinate)
    {
        base.Trigger(coordinate);
    }

    // Update is called once per frame
    public override void Fire(Vector3 coordinate)
    {
        base.Fire(coordinate);

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.x += 90;
        ExampleRocket rocketClone = Instantiate(rocket, spawnPoint.transform.position, Quaternion.Euler(rotation));

        GameObject target = ProcessInput(coordinate);
        if (target != null)
        {
            rocketClone.target = target;
        }
    }
}
