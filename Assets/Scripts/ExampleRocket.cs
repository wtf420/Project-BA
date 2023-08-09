using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleRocket : MonoBehaviour
{
    [SerializeField]
    public GameObject target;

    [Header("--Rocket default Variables--")]
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float damage;

    // Update is called once per frame
    void Update()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.up * speed;

        if (target != null)
        {
            Debug.Log("NOT NULL");
            float singleStep = turnSpeed * Time.deltaTime;
            Vector3 targetDirection = target.transform.position - transform.position;
            transform.up = Vector3.RotateTowards(transform.up, targetDirection, singleStep, 0.0f);
        } else
            Debug.Log("NULL");
    }

    void OnTriggerEnter(Collider other)
    {
        Damagable damagable = other.GetComponent<Damagable>();
        if (damagable != null && damagable.HP > 0)
        {
            damagable.TakeDamage(damage);
            GameObject.Destroy(gameObject);
        }
    }
}
