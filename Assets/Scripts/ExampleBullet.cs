using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleBullet : Bullet
{
    protected override void OnTriggerEnter(Collider otherCollider)
    {
        IDamagable damagable = otherCollider.gameObject.GetComponent<IDamagable>();
        if (damagable != null )
        {
            damagable.TakeDamage(Damage);
        }
        base.OnTriggerEnter(otherCollider);
    }
}
