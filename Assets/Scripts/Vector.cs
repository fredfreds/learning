using UnityEngine;

#region Serializable Classes
[System.Serializable]
public class Vec2
{
    public float x;
    public float y;

    public Vec2() { }

    public Vec2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
}

[System.Serializable]
public class Vec3
{
    public float x;
    public float y;
    public float z;

    public Vec3() { }

    public Vec3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}
#endregion

public class VCalc
{
    public float Angle(Vec2 one, Vec2 two)
    {
        // A = atan((y2 - y1) / (x2 - x1))
        float r = Mathf.Atan((two.y - one.y) / (two.x - one.x)) * Mathf.Rad2Deg;
        return r;
    }

    public float Magnitude(Vec2 a)
    {
        // m = sqrt(ax^2 + ay^2)
        float r = Mathf.Sqrt(a.x * a.x + a.y * a.y);
        return r;
    }

    public float Magnitude(Vec3 a)
    {
        // m = sqrt(ax^2 + ay^2 + az^2)
        float r = Mathf.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
        return r;
    }

    public float Displacement(Vec2 one, Vec2 two)
    {
        // d = sqrt((x2 - x1)^2 + (y2 - y1)^2)
        float r = Mathf.Sqrt((two.x - one.x) * (two.x - one.x) 
            + (two.y - one.y) * (two.y - one.y));
        return r;
    }

    #region Vector
    public Vec2 PolarToComp(float mag, float ang)
    {
        // A.x = |A| * cos(angle)
        // A.y = |A| * sin(angle)
        float x = mag * Mathf.Cos(ang * Mathf.Deg2Rad);
        float y = mag * Mathf.Sin(ang * Mathf.Deg2Rad);
        Vec2 r = new Vec2(x, y);
        return r;
    }

    public Vec2 AddVec(Vec2 a, Vec2 b)
    {
        // A + B = (a1 + b1)i + (a2 + b2)j
        Vec2 r = new Vec2(a.x + b.x, a.y + b.y);
        return r;
    }

    public Vec2 SubtVec(Vec2 a, Vec2 b)
    {
        // A - B = (a1 - b1)i + (a2 - b2)j
        Vec2 r = new Vec2(a.x - b.x, a.y - b.y);
        return r;
    }

    public Vec2 ScalarMult(float scalar, Vec2 v)
    {
        // c * A = c * |A| * angle
        // c * A = c * ai + c * aj
        Vec2 r = new Vec2(v.x * scalar, v.y * scalar);
        return r;
    }

    public Vec2 Normalize(Vec2 v)
    {
        // N = ax / sqrt(ax^2 + ay^2), ay / sqrt(ax^2 + ay^2)
        Vec2 r = new Vec2(v.x / Mathf.Sqrt(v.x * v.x + v.y * v.y), v.y / Mathf.Sqrt(v.x * v.x + v.y * v.y));
        return r;
    }

    public Vec3 Normalize(Vec3 v)
    {
        // N = ax / sqrt(ax^2 + ay^2 + az^2), ay / sqrt(ax^2 + ay^2 + az^2), az / sqrt(ax^2 + ay^2 + az^2)
        Vec3 r = new Vec3(v.x / Magnitude(v), v.y / Magnitude(v), v.z / Magnitude(v));
        return r;
    }

    public float Dot(Vec2 a, Vec2 b)
    {
        // A * B = a1 * b1 + a2 * b2
        // if A * B = 0 - perpendicular
        // if < 0 - negative, 0 > 90
        // if > 0 - positive, 0 < 90
        float r = a.x * b.x + a.y * b.y;
        return r;
    }

    public float Dot(Vec3 a, Vec3 b)
    {
        // A . B = a1 * b1 + a2 * b2 + a3 * b3
        // if A . B = 0 - perpendicular
        // if < 0 - negative, 0 > 90
        // if > 0 - positive, 0 < 90
        float r = a.x * b.x + a.y * b.y + a.z * b.z;
        return r;
    }

    public bool InView(float d)
    {
        if (d > 0)
            return true;
        else 
            return false;
    }

    public float AngleBetween(Vec2 a, Vec2 b)
    {
        // angle = cos^-1((ax * bx + ay * by) / sqrt(ax * ax + ay * ay) * sqrt(bx * bx + by * by))
        float r = Mathf.Acos((a.x * b.x + a.y * b.y) / (Magnitude(a) * Magnitude(b))) * Mathf.Rad2Deg;
        if ((a.x < 0 && a.y < 0) || (b.x < 0 && b.y < 0))
            return 360 - r;
        else if ((a.x >= 0 && a.y < 0) || (b.x >= 0 && b.y < 0))
            return 360 - r;
        else
            return r;
    }

    public float AngleBetween(Vec3 a, Vec3 b)
    {
        // angle = cos^-1((ax * bx + ay * by + az * bz) / sqrt(ax * ax + ay * ay + az * az) * sqrt(bx * bx + by * by + bz * bz))
        float r = Mathf.Acos((a.x * b.x + a.y * b.y + a.z * b.z) / (Magnitude(a) * Magnitude(b))) * Mathf.Rad2Deg;
        if ((a.x < 0 && a.y < 0) || (b.x < 0 && b.y < 0))
            return 360 - r;
        else if ((a.x >= 0 && a.y < 0) || (b.x >= 0 && b.y < 0))
            return 360 - r;
        else
            return r;
    }

    public Vec3 Cross(Vec3 a, Vec3 b)
    {
        // A x B != B x A
        // A x B = -(B x A)
        // C = ay * bz - az * by, az * bx - ax * bz, ax * by - ay * bx
        Vec3 r = new Vec3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        return r;
    }

    public Vec3 SurfaceNormal(Vec3 a, Vec3 b)
    {
        // (A ^x B) = A x B / |A x B|
        // (A ^x B) = x / |A x B|, y / |A x B|, z / |A x B|
        Vec3 c = new Vec3((a.y * b.z - a.z * b.y), (a.z * b.x - a.x * b.z), (a.x * b.y - a.y * b.x));
        Vec3 r = new Vec3(c.x / Magnitude(c), c.y / Magnitude(c), c.z / Magnitude(c));
        return r;
    }

    public float AngleSin(Vec3 a, Vec3 b)
    {
        // |A x B| = |A| * |B| * sin
        // sin^-1(|A x B| / (|A| * |B|)
        float r = Mathf.Asin(Magnitude(Cross(a, b)) / (Magnitude(a) * Magnitude(b))) * Mathf.Rad2Deg;
        return r;
    }

    public float AngleCos(Vec3 a, Vec3 b)
    {
        // A . B = |A| * |B| * cos
        // cos^-1(A . B / (|A| * |B|)
        float r = Mathf.Acos(Dot(a, b) / (Magnitude(a) * Magnitude(b))) * Mathf.Rad2Deg;
        return r;
    }
    #endregion
}

public class Vector : MonoBehaviour
{
    public VCalc Calc = new VCalc();
    public Transform Point1;
    public Transform Point2;

    public float Mag;
    public float Ang;
    public Vec2 ToCartesian;

    public float Mag2;
    public float Ang2;
    public Vec2 ToCartesian2;

    public Vec2 Start;
    public Vec2 One;
    public Vec2 Two;
    public Vec2 Three;
    public Vec2 Four;
    public bool AB;

    public float Scalar;
    public Vec2 Normalize;
    public Vec2 NormalizeValue;

    public float DotProduct;
    public bool InView;
    public float AngleBetween;
    public float AngleBetween3;

    public Vec3 One3;
    public Vec3 Two3;

    public Vec3 Cross;
    public Vec3 Cross2;

    public Vec3 Surface;
    public Vec3 Surface2;

    public float AngleSin;
    public float AngleCos;

    private void Setup()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(new Vector2(ToCartesian.x, ToCartesian.y), new Vector2(Start.x, Start.y));

        Gizmos.color = Color.black;
        Gizmos.DrawLine(new Vector2(ToCartesian2.x, ToCartesian2.y), new Vector2(Start.x, Start.y));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(One.x, One.y), new Vector2(Start.x, Start.y));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(Two.x, Two.y), new Vector2(Start.x, Start.y));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector2(Three.x, Three.y), new Vector2(Start.x, Start.y));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector2(Three.x, Three.y), new Vector2(One.x, One.y));
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector2(Four.x, Four.y), new Vector2(Start.x, Start.y));
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector2(Four.x, Four.y), new Vector2(One.x, One.y));
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector2(Normalize.x, Normalize.y), new Vector2(0, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(One3.x, One3.y, One3.z), new Vector3(0, 0, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(Two3.x, Two3.y, Two3.z), new Vector3(0, 0, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(Cross.x, Cross.y, Cross.z), new Vector3(0, 0, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(Cross2.x, Cross2.y, Cross2.z), new Vector3(0, 0, 0));
    }

    private void VectorCalc()
    {
        ToCartesian2 = new Vec2(Point1.position.x, Point1.position.y);
        One = new Vec2(Point1.position.x, Point1.position.y);
        Two = new Vec2(Point2.position.x, Point2.position.y);

        ToCartesian = Calc.PolarToComp(Mag, Ang);

        Ang2 = Calc.Angle(new Vec2(Start.x, Start.y), ToCartesian2);
        Mag2 = Calc.Displacement(new Vec2(Start.x, Start.y), ToCartesian2);

        Three = Calc.AddVec(One, Two);

        if(AB)
            Four = Calc.SubtVec(One, Two);
        else
            Four = Calc.SubtVec(Two, One);

        ToCartesian = Calc.ScalarMult(Scalar, ToCartesian);

        Normalize = Calc.Normalize(NormalizeValue);

        DotProduct = Calc.Dot(One, Two);

        InView = Calc.InView(DotProduct);

        AngleBetween3 = Calc.AngleBetween(One3, Two3);
        AngleBetween = Calc.AngleBetween(One, Two);

        Cross = Calc.Cross(One3, Two3);
        Cross2 = Calc.Cross(Two3, One3);

        Surface = Calc.SurfaceNormal(One3, Two3);
        Surface2 = Calc.Normalize(Calc.Cross(One3, Two3));

        AngleSin = Calc.AngleSin(One3, Two3);
        AngleCos = Calc.AngleCos(One3, Two3);
    }

    private void OnDrawGizmos()
    {
        Setup();
        VectorCalc();
    }
}
