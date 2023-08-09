using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleGernadeThrower : GernadeThrower
{
    protected override void ChildUpdate()
    {
        base.ChildUpdate();
        //do nothing
    }

    // Start is called before the first frame update
    public override void Trigger(Vector3 coordinate)
    {
        coordinate = ProcessInput(coordinate);
        base.Trigger(coordinate);
    }

    // Update is called once per frame
    public override void Fire(Vector3 coordinate)
    {
        base.Fire(coordinate);

        Vector3 direction = coordinate = this.gameObject.transform.position;
        throwForce = direction.magnitude * multiplier;
        direction = direction.normalized;
        direction.y = 0;

        GameObject ballClone = Instantiate(gernade, spawnPoint.transform.position, this.transform.rotation);
        Rigidbody body = ballClone.GetComponent<Rigidbody>();

        if (scaleWithPlayerMovement)
        {
            if (this.gameObject.GetComponentInParent<CharacterController>())
            {
                CharacterController cc = this.gameObject.GetComponentInParent<CharacterController>();
                direction += cc.velocity * scaleWithPlayerMovementMultiplier;
            }
        }

        //throw a little bit upward.
        body.AddForce( (direction + new Vector3(0, 0.2f, 0)) * throwForce, ForceMode.Impulse);
    }
}
