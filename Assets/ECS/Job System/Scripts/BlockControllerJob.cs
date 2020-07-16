using UnityEngine;
using UnityEngine.Jobs;

// 1
public struct BlockControllerJob : IJobParallelForTransform
{
    public float UpperBound;
    public float LowerBound;
    public float LeftBound;
    public float RightBound;

    public float RotateSpeed;
    public Vector3 RotationDirection;

    public float DeltaTime;

    public void Execute(int index, TransformAccess transform)
    {
        transform.rotation = transform.rotation * Quaternion.AngleAxis(180 * RotateSpeed * DeltaTime, RotationDirection);
    }
}
