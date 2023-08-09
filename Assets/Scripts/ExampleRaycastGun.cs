using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleRaycastGun : RaycastGun
{
    public override void Trigger(Vector3 coordinate)
    {
        coordinate = ProcessInput(coordinate);
        GameObject bulletClone = Instantiate(a, coordinate, this.transform.rotation);
        base.Trigger(coordinate);
    }

    // Update is called once per frame
    public override void Fire(Vector3 coordinate)
    {
        base.Fire(coordinate);

        Vector3 direction = coordinate - spawnPoint.transform.position;
        RaycastHit[] hits = Physics.RaycastAll(spawnPoint.transform.position, direction.normalized, range);
        foreach (RaycastHit hit in hits)
        {
            Debug.Log("Hit!");
            IDamagable damagable = hit.collider.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(damage);
            }
        }

        Debug.Log(coordinate + " " + (spawnPoint.transform.position + direction));
        Debug.DrawLine(spawnPoint.transform.position, spawnPoint.transform.position + direction.normalized * range, Color.red, 10f);
    }
}
