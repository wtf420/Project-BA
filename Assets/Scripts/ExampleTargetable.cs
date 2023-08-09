using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleTargetable : MonoBehaviour, ITargetable
{
    [SerializeField]
    bool isTargetable;
    public bool IsTargetable {
        get { return isTargetable; }
        private set { IsTargetable = value; }
    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }
}
