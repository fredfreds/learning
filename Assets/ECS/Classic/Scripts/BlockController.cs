using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public float RotateSpeed;
    public Vector3 RotationDirection;

    void Update()
    {
        transform.rotation = transform.rotation * Quaternion.AngleAxis(180 * RotateSpeed * Time.deltaTime, RotationDirection);
    }
}
