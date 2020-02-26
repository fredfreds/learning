using UnityEngine;

#region Serializable classes
[System.Serializable]
public class Point
{
    public float x;
    public float y;

    public Point() { }

    public Point(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
}

[System.Serializable]
public class Circle
{
    public Point Center;
    public float Radius;
}
#endregion

public class Calculator
{
    #region Points Functions
    public float Slope(Point one, Point two)
    {
        // slope = m = (y2 - y1) / (x2 - x1)
        float r = (two.y - one.y) / (two.x - one.x);
        return r;
    }

    public float PerpendicularSlope(float slope)
    {
        // m1 = -1 / m2 or m2 = -1 / m1
        float r = -1 / slope;
        return r;
    }

    public bool ArePerpendicular(float s1, float s2)
    {
        // m1 * m2 = -1
        bool r = s1 * s2 == -1;
        return r;
    }

    public bool AreParallel(float s1, float s2)
    {
        // m1 = m2
        bool r = s1 == s2;
        return r;
    }

    public Point LineIntersect(Point one, Point two, float s1, float s2)
    {
        // m1 != m2, one solution
        // m1 = m2, if b1 != b2, zero solutions. if b1 = b2, infinite solutions.

        // y = mx + b

        // y - y1 = m * (x - x1)
        // y = m * (x - x1) + y1
        // y = m * (x - x2) + y2
        // x = (m1 * x1 - m2 * x2 + y2 - y1) / (m1 - m2)
        float x = (s1 * one.x - s2 * two.x + two.y - one.y) / (s1 - s2);
        float y = s1 * (x - one.x) + one.y;
        return new Point(x, y);
    }
    #endregion

    #region Geometry Snippets
    public float Distance(Point one, Point two)
    {
        // distance = p1p2 = sqrt((x2 - x1)^2 + (y2 - y1)^2)
        float r = Mathf.Sqrt((two.x - one.x) * (two.x - one.x) + (two.y - one.y) * (two.y - one.y));
        return r;
    }

    public float DistanceTriangle(Point one, Point two)
    {
        // distance = p1p2 = (x2 - x1)^2 + (y2 - y1)^2
        float r = (two.x - one.x) * (two.x - one.x) + (two.y - one.y) * (two.y - one.y);
        return r;
    }

    public bool CheckRightTriangle(Point one, Point two, Point three)
    {
        // a^2 + b^2 = c^2
        float r1 = DistanceTriangle(one, two);
        float r2 = DistanceTriangle(two, three);
        float r3 = DistanceTriangle(three, one);
        return r3 == r1 + r2;
    }

    public Point MidPoint(Point one, Point two)
    {
        // M((x1 + x2) / 2, (y1 + y2) / 2)
        float x = (one.x + two.x) / 2;
        float y = (one.y + two.y) / 2;
        return new Point(x, y);
    }

    // Parabola 
    // y = a * (x - h)^2 + k - vertical axis
    // x = a * (y - k)^2 + h - horizontal axis

    // Circle
    // x^2 + y^2 = r^2 - centered at the origin
    // (x - h)^2 + (y - k)^2 = r^2

    public bool IsColliding(Circle one, Circle two)
    {
        // (h2 - h1)^2 + (k2 - k1)^2 <= (r1 + r2)^2
        return ((two.Center.x - one.Center.x) * (two.Center.x - one.Center.x) +
            (two.Center.y - one.Center.y) * (two.Center.y - one.Center.y) <
            (two.Radius + one.Radius) * (two.Radius + one.Radius));
    }
    #endregion

    #region Trigonometry

    // Degrees to Radians, 0.01745...f
    // (PI / 180)
    // Radians to Degrees, 57.2957...f
    // (180 / PI)

    public float AngleAtan(Point a, Point b)
    {
        float r = Mathf.Atan((b.y - a.y) / (b.x - a.x)) * Mathf.Rad2Deg;
        if (b.x < a.x && b.y > a.y)
            return r + 180f;
        else if (b.x < a.x && b.y < a.y)
            return r + 180f;
        else if (b.x > a.x && b.y < a.y)
            return r + 360f;
        else
            return r;
    }

    public float Sin(float opp, float hyp)
    {
        float r = opp / hyp;
        return r;
    }

    public float SinD(float opp)
    {
        float r = Mathf.Asin(opp) * Mathf.Rad2Deg;
        return r;
    }

    public float Cos(float adj, float hyp)
    {
        float r = adj / hyp;
        return r;
    }

    public float CosD(float adj)
    {
        float r = Mathf.Acos(adj) * Mathf.Rad2Deg;
        return r;
    }

    public float Tan(float opp, float adj)
    {
        float r = opp / adj;
        return r;
    }

    public float TanD(float adj)
    {
        float r = Mathf.Atan(adj) * Mathf.Rad2Deg;
        return r;
    }

    public float Csc(float hyp, float opp)
    {
        float r = hyp / opp;
        return r;
    }

    public float Sec(float hyp, float adj)
    {
        float r = hyp / adj;
        return r;
    }

    public float Cot(float adj, float opp)
    {
        float r = adj / opp;
        return r;
    }

    //cos^2 A + sin^2 A = 1

    // tan A = sin A / cos A
    // sin (A1 + A2) = sin A1 cos A2 + cos A1 sin A2
    // sin (A1 - A2) = sin A1 cos A2 - cos A1 sin A2

    // cos (A1 + A2) = cos A1 cos A2 + sin A1 sin A2
    // cos (A1 - A2) = cos A1 cos A2 - sin A1 sin A2
    #endregion
}

public class PointsAndLines : MonoBehaviour
{
    #region Variables
    public Calculator Calc = new Calculator();

    public Point OneGreen;
    public Point TwoRed;
    public Point ThreeBlue;
    public Point FourYellow;

    public Point IntersectPoint;
    public Point MidPoint;

    public Transform P1;
    public Transform P2;
    public Transform P3;
    public Transform P4;

    public float PointRadius;
    public float Distance;
    public float Slope1;
    public float Slope2;
    public float PerpendicularSlope1;
    public float PerpendicularSlope2;

    public bool ArePerpendicular;
    public bool AreParallel;
    public bool AreRightTriangle;

    public float LA;
    public float LB;
    public float LC;

    public float Sin;
    public float SinDegrees;
    public float Cos;
    public float CosDegrees;
    public float Tan;
    public float TanDegrees;
    public float Csc;
    public float Sec;
    public float Cot;
    public float AngleAtan;

    public Circle C1;
    public Circle C2;

    public bool IsColliding;
    #endregion

    #region Functions
    private void Setup()
    {
        OneGreen = new Point(P1.position.x, P1.position.y);
        TwoRed = new Point(P2.position.x, P2.position.y);
        ThreeBlue = new Point(P3.position.x, P3.position.y);
        FourYellow = new Point(P4.position.x, P4.position.y);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector2(TwoRed.x, TwoRed.y), new Vector2(OneGreen.x, OneGreen.y));
        Gizmos.color = Color.white;
        Gizmos.DrawLine(new Vector2(ThreeBlue.x, ThreeBlue.y), new Vector2(TwoRed.x, TwoRed.y));
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(new Vector2(FourYellow.x, FourYellow.y), new Vector2(ThreeBlue.x, ThreeBlue.y));
        Gizmos.color = Color.white;
        Gizmos.DrawLine(new Vector2(OneGreen.x, OneGreen.y), new Vector2(FourYellow.x, FourYellow.y));

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector2(OneGreen.x, OneGreen.y), PointRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector2(TwoRed.x, TwoRed.y), PointRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(new Vector2(ThreeBlue.x, ThreeBlue.y), PointRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector2(FourYellow.x, FourYellow.y), PointRadius);

        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(new Vector2(IntersectPoint.x, IntersectPoint.y), PointRadius);

        Gizmos.color = Color.grey;
        Gizmos.DrawSphere(new Vector2(MidPoint.x, MidPoint.y), PointRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(new Vector2(C1.Center.x, C1.Center.y), C1.Radius);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(new Vector2(C2.Center.x, C2.Center.y), C2.Radius);
    }

    private void Points()
    {
        Slope1 = Calc.Slope(OneGreen, TwoRed);
        Slope2 = Calc.Slope(ThreeBlue, FourYellow);
        PerpendicularSlope1 = Calc.PerpendicularSlope(Slope1);
        PerpendicularSlope2 = Calc.PerpendicularSlope(Slope2);

        ArePerpendicular = Calc.ArePerpendicular(PerpendicularSlope1, PerpendicularSlope2);
        AreParallel = Calc.AreParallel(Slope1, Slope2);
    }

    private void Geometry()
    {
        Distance = Calc.Distance(OneGreen, TwoRed);
        AreRightTriangle = Calc.CheckRightTriangle(OneGreen, TwoRed, ThreeBlue);
        MidPoint = Calc.MidPoint(OneGreen, TwoRed);
        IntersectPoint = Calc.LineIntersect(OneGreen, ThreeBlue, Slope1, Slope2);
        IsColliding = Calc.IsColliding(C1, C2);
    }

    private void Trigonometry()
    {
        LA = Calc.Distance(OneGreen, TwoRed);
        LB = Calc.Distance(TwoRed, ThreeBlue);
        LC = Calc.Distance(OneGreen, ThreeBlue);

        Sin = Calc.Sin(LB, LC);
        Cos = Calc.Cos(LA, LC);
        Tan = Calc.Tan(LB, LA);
        Csc = Calc.Csc(LC, LB);
        Sec = Calc.Sec(LC, LA);
        Cot = Calc.Cot(LA, LB);

        SinDegrees = Calc.SinD(Sin);
        CosDegrees = Calc.CosD(Cos);
        TanDegrees = Calc.TanD(Tan);

        AngleAtan = Calc.AngleAtan(TwoRed, ThreeBlue);
    }
    #endregion

    private void OnDrawGizmos()
    {
        Setup();
        Points();
        Geometry();
        Trigonometry();
    }
}