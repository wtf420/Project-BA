using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetable
{
    bool IsTargetable { get; }
    public Vector3 GetPosition();
}
