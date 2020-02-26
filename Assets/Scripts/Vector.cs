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
    }

    private void OnDrawGizmos()
    {
        Setup();
        VectorCalc();
    }
}
