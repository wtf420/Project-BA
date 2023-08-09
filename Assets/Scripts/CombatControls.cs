using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatControls : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;

    private float throwForce;

    [SerializeField]
    [Range(0.5f, 5.0f)]
    private float multiplier;

    [SerializeField]
    public GameObject spawnPoint;

    [SerializeField]
    private bool scaleWithPlayerMovement;

    public void Throwball(Vector3 direction)
    {
        throwForce = direction.magnitude * multiplier;
        direction = direction.normalized;
        direction.y = 0; 

        GameObject ballClone = Instantiate(ball, spawnPoint.transform.position, this.transform.rotation);
        Rigidbody body = ballClone.GetComponent<Rigidbody>();

        if (scaleWithPlayerMovement)
            body.velocity += direction;

        body.AddForce((direction + new Vector3(0, 0.2f, 0)) * throwForce, ForceMode.Impulse);
    }
}
