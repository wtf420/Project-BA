using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static event Action<Bullet> OnHit;

    [Header("--Bullet default Variables--")]
    public float Damage;
    [SerializeField] protected bool isPenetrative; //can this bullet penetrate things?

    new protected Collider collider;
    new protected Rigidbody rigidbody;

    void Awake()
    {
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void OnTriggerEnter(Collider otherCollider)
    {
        if (!isPenetrative)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
