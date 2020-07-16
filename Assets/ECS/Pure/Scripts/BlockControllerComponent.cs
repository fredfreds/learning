using System;
using UnityEngine;
using Unity.Entities;

[Serializable]
public struct BlockControllerData : IComponentData
{
    public float RotateSpeed;
    public Vector3 RotationDirection;
}

public class BlockControllerComponent : ComponentDataProxy<BlockControllerData>
{
 
}
