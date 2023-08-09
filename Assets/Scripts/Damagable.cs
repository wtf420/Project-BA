using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour, IDamagable
{
    public float HP;

    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0.0f)
            GameObject.Destroy(this.gameObject);
    }
}
