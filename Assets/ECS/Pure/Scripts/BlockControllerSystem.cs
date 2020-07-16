using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Burst;
using Unity.Collections;

public class BlockControllerSystem // : JobComponentSystem
{
    //private struct BlockControlJob : IJobForEach<Translation, Rotation, BlockControllerData>
    //{
    //    public float UpperBound;
    //    public float LowerBound;
    //    public float LeftBound;
    //    public float RightBound;
    //    public float DeltaTime;

    //    public void Execute([ReadOnly]ref Translation c0, ref Rotation c1, 
    //        ref BlockControllerData c2)
    //    {
    //        Quaternion rot = c1.Value;
    //        rot = c1.Value * Quaternion.AngleAxis(180 *
    //            c2.RotateSpeed * DeltaTime, c2.RotationDirection);

    //        c1.Value = rot;
    //    }
    //}

    //protected override JobHandle OnUpdate(JobHandle inputDeps)
    //{
    //    float delta = Time.DeltaTime;
    //    BlockControlJob blockControl = new BlockControlJob
    //    {
    //        UpperBound = GameManagerPure.Instance.upperBound,
    //        LowerBound = GameManagerPure.Instance.lowerBound,
    //        LeftBound = GameManagerPure.Instance.leftBound,
    //        RightBound = GameManagerPure.Instance.rightBound,
    //        DeltaTime = delta
    //    };

    //    JobHandle blockHandle = blockControl.Schedule(this, inputDeps);
    //    return blockHandle;
    //}
}
