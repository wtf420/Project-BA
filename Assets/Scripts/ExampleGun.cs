using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleGun : Gun
{
    public override void Trigger(Vector3 coordinate)
    {
        coordinate = ProcessInput(coordinate);
        base.Trigger(coordinate);
    }

    // Update is called once per frame
    public override void Fire(Vector3 coordinate)
    {
        base.Fire(coordinate);

        Vector3 direction = coordinate - this.transform.position;
        direction = direction.normalized;
        Bullet bulletClone = Instantiate(bullet, spawnPoint.transform.position, this.transform.rotation);
        Rigidbody body = bulletClone.GetComponent<Rigidbody>();

        body.AddForce(direction * bulletSpeed, ForceMode.Impulse);
        bulletClone.Damage = damage;
    }
}
