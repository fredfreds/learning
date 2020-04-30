using UnityEngine;

[System.Serializable]
public class Vec4
{
    public int x;
    public int y;
    public int z;

    public Vec4() { }

    public Vec4(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

[System.Serializable]
public class Point3
{
    public int x;
    public int y;
    public int z;

    public Point3() { }

    public Point3(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

[System.Serializable]
public class Rect
{
    public Point3 A, B, C, D;

    public Rect() { }

    public Rect(Point3 a, Point3 b, Point3 c, Point3 d)
    {
        A = a;
        B = b;
        C = c;
        D = d;
    }
}

public class Matrix3x3
{
    public float[,] m;

    public Matrix3x3()
    {
        m = new float[3, 3];
    }

    public Matrix3x3(Vec2 s)
    {
        m = new float[(int)s.x, (int)s.y];
    }

    public void PrintAll(Vec2 v)
    {
        for (int i = 0; i < v.x; i++)
        {
            for (int j = 0; j < v.y; j++)
            {
                Debug.Log(m[i, j]);
            }
        }
    }

    public void Print(Vec2 n)
    {
        Debug.Log("Num at " + n.x + "/" + n.y + " = " + m[(int)n.x, (int)n.y].ToString());
    }
}

public class Matrix4x4
{
    public int[,] m = new int[4, 4];

    public Matrix4x4() { }

    public Matrix4x4(int s)
    {
        m[0, 0] = 0;
        m[0, 1] = 0;
        m[0, 2] = 0;
        m[0, 3] = 0;

        m[1, 0] = 0;
        m[1, 1] = 0;
        m[1, 2] = 0;
        m[1, 3] = 0;

        m[2, 0] = 0;
        m[2, 1] = 0;
        m[2, 2] = 0;
        m[2, 3] = 0;

        m[3, 0] = 0;
        m[3, 1] = 0;
        m[3, 2] = 0;
        m[3, 3] = 0;
    }

    public Matrix4x4(int x, int y, int z, int sx = 1, int sy = 1, int sz = 1) 
    {
        m[0, 0] = sx;
        m[0, 1] = 0;
        m[0, 2] = 0;
        m[0, 3] = x;

        m[1, 0] = 0;
        m[1, 1] = sy;
        m[1, 2] = 0;
        m[1, 3] = y;

        m[2, 0] = 0;
        m[2, 1] = 0;
        m[2, 2] = sz;
        m[2, 3] = z;

        m[3, 0] = 0;
        m[3, 1] = 0;
        m[3, 2] = 0;
        m[3, 3] = 1;
    }
}

public class Matrix4x1
{
    public int[] m = new int[4];

    public Matrix4x1() { }

    public Matrix4x1(int x, int y, int z)
    {
        m[0] = x;
        m[1] = y;
        m[2] = z;
        m[3] = 1;
    }
}

public class MatrixCalculator
{
    public Matrix4x4 Multiply(Matrix4x4 m1, Matrix4x4 m2)
    {
        Matrix4x4 m = new Matrix4x4();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                m.m[i, j] += m1.m[i, j] * m2.m[i, j];
            }
        }

        return m;
    }

    public Matrix4x1 Multiply(Matrix4x4 m4, Matrix4x1 m1)
    {
        //int x = m4.m[0, 0] * m1.m[0] + m4.m[0, 1] * m1.m[1] + m4.m[0, 2] * m1.m[2] + m4.m[0, 3] * m1.m[3];
        //int y = m4.m[1, 0] * m1.m[0] + m4.m[1, 1] * m1.m[1] + m4.m[1, 2] * m1.m[2] + m4.m[1, 3] * m1.m[3];
        //int z = m4.m[2, 0] * m1.m[0] + m4.m[2, 1] * m1.m[1] + m4.m[2, 2] * m1.m[2] + m4.m[2, 3] * m1.m[3];
        //int w = m4.m[3, 0] * m1.m[0] + m4.m[3, 1] * m1.m[1] + m4.m[3, 2] * m1.m[2] + m4.m[3, 3] * m1.m[3];

        Matrix4x1 m = new Matrix4x1();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                m.m[i] += m4.m[i, j] * m1.m[j];
            }
        }

        return m;
    }

    public Matrix4x1 Scale(Matrix4x4 m4, Matrix4x1 m1, int x = 1, int y = 1, int z = 1)
    {
        Matrix4x1 m = new Matrix4x1();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                m.m[i] += m4.m[i, j] * m1.m[j];
            }
        }

        m.m[0] *= x;
        m.m[1] *= y;
        m.m[2] *= z;

        return m;
    }

    public Matrix4x1 RotateY(Matrix4x4 m4, Matrix4x1 m1, float angle)
    {
        int x = (int)Mathf.Cos((Mathf.Deg2Rad * angle)) * m4.m[0, 0] * m1.m[0] + m4.m[0, 1] * m1.m[1] + (int)Mathf.Sin((Mathf.Deg2Rad * angle)) * m4.m[0, 2] * m1.m[2] + m4.m[0, 3] * m1.m[3];
        int y = m4.m[1, 0] * m1.m[0] + m4.m[1, 1] * m1.m[1] + m4.m[1, 2] * m1.m[2] + m4.m[1, 3] * m1.m[3];
        int z = (-1) * (int)Mathf.Sin((Mathf.Deg2Rad * angle)) * m4.m[2, 0] * m1.m[0] + m4.m[2, 1] * m1.m[1] + (int)Mathf.Cos((Mathf.Deg2Rad * angle)) * m4.m[2, 2] * m1.m[2] + m4.m[2, 3] * m1.m[3];
        int w = m4.m[3, 0] * m1.m[0] + m4.m[3, 1] * m1.m[1] + m4.m[3, 2] * m1.m[2] + m4.m[3, 3] * m1.m[3];

        Matrix4x1 m = new Matrix4x1(x, y, z);

        return m;
    }

    public Matrix4x1 RotateZ(Matrix4x4 m4, Matrix4x1 m1, float angle)
    {
        int x = (int)Mathf.Cos((Mathf.Deg2Rad * angle)) * m4.m[0, 0] * m1.m[0] + (-1) * (int)Mathf.Sin((Mathf.Deg2Rad * angle)) * m4.m[0, 1] * m1.m[1] + m4.m[0, 2] * m1.m[2] + m4.m[0, 3] * m1.m[3];
        int y = (int)Mathf.Sin((Mathf.Deg2Rad * angle)) * m4.m[1, 0] * m1.m[0] + (int)Mathf.Cos((Mathf.Deg2Rad * angle)) * m4.m[1, 1] * m1.m[1] + m4.m[1, 2] * m1.m[2] + m4.m[1, 3] * m1.m[3];
        int z = m4.m[2, 0] * m1.m[0] + m4.m[2, 1] * m1.m[1] + m4.m[2, 2] * m1.m[2] + m4.m[2, 3] * m1.m[3];
        int w = m4.m[3, 0] * m1.m[0] + m4.m[3, 1] * m1.m[1] + m4.m[3, 2] * m1.m[2] + m4.m[3, 3] * m1.m[3];

        Matrix4x1 m = new Matrix4x1(x, y, z);

        return m;
    }

    public Matrix4x1 RotateX(Matrix4x4 m4, Matrix4x1 m1, float angle)
    {
        int x = m4.m[0, 0] * m1.m[0] + m4.m[0, 1] * m1.m[1] + m4.m[0, 2] * m1.m[2] + m4.m[0, 3] * m1.m[3];
        int y = m4.m[1, 0] * m1.m[0] + (int)Mathf.Cos((Mathf.Deg2Rad * angle)) * m4.m[1, 1] * m1.m[1] + (-1) * (int)Mathf.Sin((Mathf.Deg2Rad * angle)) * m4.m[1, 2] * m1.m[2] + m4.m[1, 3] * m1.m[3];
        int z = m4.m[2, 0] * m1.m[0] + (int)Mathf.Sin((Mathf.Deg2Rad * angle)) * m4.m[2, 1] * m1.m[1] + (int)Mathf.Cos((Mathf.Deg2Rad * angle)) * m4.m[2, 2] * m1.m[2] + m4.m[2, 3] * m1.m[3];
        int w = m4.m[3, 0] * m1.m[0] + m4.m[3, 1] * m1.m[1] + m4.m[3, 2] * m1.m[2] + m4.m[3, 3] * m1.m[3];

        Matrix4x1 m = new Matrix4x1(x, y, z);

        return m;
    }

    public void Print4x1(Matrix4x1 m1)
    {
        for (int i = 0; i < 4; i++)
        {
            Debug.Log(m1.m[i]);
        }
    }
}

public class Matrices : MonoBehaviour
{
    Matrix3x3 M;
    MatrixCalculator Calc = new MatrixCalculator();

    public bool Transform;
    public bool Scale;
    public bool RotateX;
    public bool RotateY;
    public bool RotateZ;
    public Vec2 Size;
    public Vec2 Num;

    public Rect R;
    public Rect R2;

    public Transform A;
    public Transform B;
    public Transform C;
    public Transform D;

    public Vec4 M4x4V;
    public Vec4 M4x4S;
    Matrix4x4 M4x4;
    Matrix4x1 M4x1A;
    Matrix4x1 M4x1B;
    Matrix4x1 M4x1C;
    Matrix4x1 M4x1D;

    Matrix4x1 M4x1A2;
    Matrix4x1 M4x1B2;
    Matrix4x1 M4x1C2;
    Matrix4x1 M4x1D2;

    public float Angle;

    public bool ConcatScale;
    public bool ConcatRotate;

    private void Draw()
    {
        R.A.x = (int)A.position.x;
        R.A.y = (int)A.position.y;
        R.A.z = (int)A.position.z;
        R.B.x = (int)B.position.x;
        R.B.y = (int)B.position.y;
        R.B.z = (int)B.position.z;
        R.C.x = (int)C.position.x;
        R.C.y = (int)C.position.y;
        R.C.z = (int)C.position.z;
        R.D.x = (int)D.position.x;
        R.D.y = (int)D.position.y;
        R.D.z = (int)D.position.z;


        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(R.A.x, R.A.y), new Vector3(R.B.x, R.B.y));
        Gizmos.DrawLine(new Vector3(R.B.x, R.B.y), new Vector3(R.C.x, R.C.y));
        Gizmos.DrawLine(new Vector3(R.C.x, R.C.y), new Vector3(R.D.x, R.D.y));
        Gizmos.DrawLine(new Vector3(R.D.x, R.D.y), new Vector3(R.A.x, R.A.y));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(R2.A.x, R2.A.y), new Vector3(R2.B.x, R2.B.y));
        Gizmos.DrawLine(new Vector3(R2.B.x, R2.B.y), new Vector3(R2.C.x, R2.C.y));
        Gizmos.DrawLine(new Vector3(R2.C.x, R2.C.y), new Vector3(R2.D.x, R2.D.y));
        Gizmos.DrawLine(new Vector3(R2.D.x, R2.D.y), new Vector3(R2.A.x, R2.A.y));
    }

    private void OnDrawGizmos()
    {
        Draw();

        M4x4 = new Matrix4x4(M4x4V.x, M4x4V.y, M4x4V.z);

        M4x1A = new Matrix4x1(R.A.x, R.A.y, R.A.z);
        M4x1B = new Matrix4x1(R.B.x, R.B.y, R.B.z);
        M4x1C = new Matrix4x1(R.C.x, R.C.y, R.C.z);
        M4x1D = new Matrix4x1(R.D.x, R.D.y, R.D.z);

        if(ConcatRotate)
        {
            Matrix4x4 X = new Matrix4x4(1);
            X.m[0, 0] = 1;
            X.m[1, 1] = (int)Mathf.Cos((Mathf.Deg2Rad * Angle));
            X.m[2, 2] = (int)Mathf.Cos((Mathf.Deg2Rad * Angle));
            X.m[3, 3] = 1;
            X.m[2, 1] = -1 * (int)Mathf.Sin((Mathf.Deg2Rad * Angle));
            X.m[1, 2] = (int)Mathf.Sin((Mathf.Deg2Rad * Angle));

            Matrix4x4 Y = new Matrix4x4(1);
            Y.m[0, 0] = (int)Mathf.Cos((Mathf.Deg2Rad * Angle));
            Y.m[1, 1] = 1;
            Y.m[2, 2] = (int)Mathf.Cos((Mathf.Deg2Rad * Angle));
            Y.m[3, 3] = 1;
            Y.m[2, 0] = -1 * (int)Mathf.Sin((Mathf.Deg2Rad * Angle));
            Y.m[0, 2] = (int)Mathf.Sin((Mathf.Deg2Rad * Angle));

            Matrix4x4 Z = new Matrix4x4(1);
            X.m[0, 0] = (int)Mathf.Cos((Mathf.Deg2Rad * Angle));
            Z.m[1, 1] = (int)Mathf.Cos((Mathf.Deg2Rad * Angle));
            Z.m[2, 2] = 1;
            Z.m[3, 3] = 1;
            Z.m[0, 1] = -1 * (int)Mathf.Sin((Mathf.Deg2Rad * Angle));
            Z.m[1, 0] = (int)Mathf.Sin((Mathf.Deg2Rad * Angle));

            Matrix4x4 T = Calc.Multiply(Y, X);

            Matrix4x4 R = Calc.Multiply(T, Z);

            M4x1A2 = Calc.Multiply(R, M4x1A);
            M4x1B2 = Calc.Multiply(R, M4x1B);
            M4x1C2 = Calc.Multiply(R, M4x1C);
            M4x1D2 = Calc.Multiply(R, M4x1D);

            R2.A.x = M4x1A2.m[0];
            R2.A.y = M4x1A2.m[1];
            R2.A.z = M4x1A2.m[2];
            R2.B.x = M4x1B2.m[0];
            R2.B.y = M4x1B2.m[1];
            R2.B.z = M4x1B2.m[2];
            R2.C.x = M4x1C2.m[0];
            R2.C.y = M4x1C2.m[1];
            R2.C.z = M4x1C2.m[2];
            R2.D.x = M4x1D2.m[0];
            R2.D.y = M4x1D2.m[1];
            R2.D.z = M4x1D2.m[2];

            ConcatRotate = false;
        }

        if(ConcatScale)
        {
            Matrix4x4 a1 = new Matrix4x4(R2.A.x, R2.A.y, R2.A.z);
            Matrix4x4 a2 = new Matrix4x4(0, 0, 0, M4x4S.x, M4x4S.y, M4x4S.z);
            Matrix4x4 b1 = new Matrix4x4(R2.B.x, R2.B.y, R2.B.z);
            Matrix4x4 b2 = new Matrix4x4(0, 0, 0, M4x4S.x, M4x4S.y, M4x4S.z);
            Matrix4x4 c1 = new Matrix4x4(R2.C.x, R2.C.y, R2.C.z);
            Matrix4x4 c2 = new Matrix4x4(0, 0, 0, M4x4S.x, M4x4S.y, M4x4S.z);
            Matrix4x4 d1 = new Matrix4x4(R2.D.x, R2.D.y, R2.D.z);
            Matrix4x4 d2 = new Matrix4x4(0, 0, 0, M4x4S.x, M4x4S.y, M4x4S.z);

            Matrix4x4 ra = Calc.Multiply(a1, a2);
            Matrix4x4 rb = Calc.Multiply(b1, b2);
            Matrix4x4 rc = Calc.Multiply(c1, c2);
            Matrix4x4 rd = Calc.Multiply(d1, d2);

            Matrix4x4 a3 = new Matrix4x4(-R2.A.x, -R2.A.y, -R2.A.z);
            Matrix4x4 b3 = new Matrix4x4(-R2.B.x, -R2.B.y, -R2.B.z);
            Matrix4x4 c3 = new Matrix4x4(-R2.C.x, -R2.C.y, -R2.C.z);
            Matrix4x4 d3 = new Matrix4x4(-R2.D.x, -R2.D.y, -R2.D.z);

            ra = Calc.Multiply(ra, a3);
            rb = Calc.Multiply(rb, b3);
            rc = Calc.Multiply(rc, c3);
            rd = Calc.Multiply(rd, d3);

            M4x1A2 = Calc.Multiply(ra, M4x1A);
            M4x1B2 = Calc.Multiply(rb, M4x1B);
            M4x1C2 = Calc.Multiply(rc, M4x1C);
            M4x1D2 = Calc.Multiply(rd, M4x1D);

            R2.A.x = M4x1A2.m[0];
            R2.A.y = M4x1A2.m[1];
            R2.A.z = M4x1A2.m[2];
            R2.B.x = M4x1B2.m[0];
            R2.B.y = M4x1B2.m[1];
            R2.B.z = M4x1B2.m[2];
            R2.C.x = M4x1C2.m[0];
            R2.C.y = M4x1C2.m[1];
            R2.C.z = M4x1C2.m[2];
            R2.D.x = M4x1D2.m[0];
            R2.D.y = M4x1D2.m[1];
            R2.D.z = M4x1D2.m[2];

            //M4x1A2 = Calc.Scale(new Matrix4x4(-R2.A.x, -R2.A.y, -R2.A.z), M4x1A, 2, 2, 2);
            //M4x1B2 = Calc.Scale(new Matrix4x4(-R2.B.x, -R2.B.y, -R2.B.z), M4x1B, 2, 2, 2);
            //M4x1C2 = Calc.Scale(new Matrix4x4(-R2.C.x, -R2.C.y, -R2.C.z), M4x1C, 2, 2, 2);
            //M4x1D2 = Calc.Scale(new Matrix4x4(-R2.D.x, -R2.D.y, -R2.D.z), M4x1D, 2, 2, 2);

            //R2.A.x = M4x1A2.m[0];
            //R2.A.y = M4x1A2.m[1];
            //R2.A.z = M4x1A2.m[2];
            //R2.B.x = M4x1B2.m[0];
            //R2.B.y = M4x1B2.m[1];
            //R2.B.z = M4x1B2.m[2];
            //R2.C.x = M4x1C2.m[0];
            //R2.C.y = M4x1C2.m[1];
            //R2.C.z = M4x1C2.m[2];
            //R2.D.x = M4x1D2.m[0];
            //R2.D.y = M4x1D2.m[1];
            //R2.D.z = M4x1D2.m[2];

            //M4x1A2 = Calc.Multiply(new Matrix4x4(R2.A.x, R2.A.y, R2.A.z), M4x1A);
            //M4x1B2 = Calc.Multiply(new Matrix4x4(R2.B.x, R2.B.y, R2.B.z), M4x1A);
            //M4x1C2 = Calc.Multiply(new Matrix4x4(R2.C.x, R2.C.y, R2.C.z), M4x1A);
            //M4x1D2 = Calc.Multiply(new Matrix4x4(R2.D.x, R2.D.y, R2.D.z), M4x1A);

            //R2.A.x = M4x1A2.m[0];
            //R2.A.y = M4x1A2.m[1];
            //R2.A.z = M4x1A2.m[2];
            //R2.B.x = M4x1B2.m[0];
            //R2.B.y = M4x1B2.m[1];
            //R2.B.z = M4x1B2.m[2];
            //R2.C.x = M4x1C2.m[0];
            //R2.C.y = M4x1C2.m[1];
            //R2.C.z = M4x1C2.m[2];
            //R2.D.x = M4x1D2.m[0];
            //R2.D.y = M4x1D2.m[1];
            //R2.D.z = M4x1D2.m[2];

            ConcatScale = false;
        }

        if (RotateX)
        {
            M4x1A2 = Calc.RotateX(M4x4, M4x1A, Angle);
            M4x1B2 = Calc.RotateX(M4x4, M4x1B, Angle);
            M4x1C2 = Calc.RotateX(M4x4, M4x1C, Angle);
            M4x1D2 = Calc.RotateX(M4x4, M4x1D, Angle);

            R2.A.x = M4x1A2.m[0];
            R2.A.y = M4x1A2.m[1];
            R2.A.z = M4x1A2.m[2];
            R2.B.x = M4x1B2.m[0];
            R2.B.y = M4x1B2.m[1];
            R2.B.z = M4x1B2.m[2];
            R2.C.x = M4x1C2.m[0];
            R2.C.y = M4x1C2.m[1];
            R2.C.z = M4x1C2.m[2];
            R2.D.x = M4x1D2.m[0];
            R2.D.y = M4x1D2.m[1];
            R2.D.z = M4x1D2.m[2];

            RotateX = false;
        }

        if (RotateY)
        {
            M4x1A2 = Calc.RotateY(M4x4, M4x1A, Angle);
            M4x1B2 = Calc.RotateY(M4x4, M4x1B, Angle);
            M4x1C2 = Calc.RotateY(M4x4, M4x1C, Angle);
            M4x1D2 = Calc.RotateY(M4x4, M4x1D, Angle);

            R2.A.x = M4x1A2.m[0];
            R2.A.y = M4x1A2.m[1];
            R2.A.z = M4x1A2.m[2];
            R2.B.x = M4x1B2.m[0];
            R2.B.y = M4x1B2.m[1];
            R2.B.z = M4x1B2.m[2];
            R2.C.x = M4x1C2.m[0];
            R2.C.y = M4x1C2.m[1];
            R2.C.z = M4x1C2.m[2];
            R2.D.x = M4x1D2.m[0];
            R2.D.y = M4x1D2.m[1];
            R2.D.z = M4x1D2.m[2];

            RotateY = false;
        }

        if (RotateZ)
        {
            M4x1A2 = Calc.RotateZ(M4x4, M4x1A, Angle);
            M4x1B2 = Calc.RotateZ(M4x4, M4x1B, Angle);
            M4x1C2 = Calc.RotateZ(M4x4, M4x1C, Angle);
            M4x1D2 = Calc.RotateZ(M4x4, M4x1D, Angle);

            R2.A.x = M4x1A2.m[0];
            R2.A.y = M4x1A2.m[1];
            R2.A.z = M4x1A2.m[2];
            R2.B.x = M4x1B2.m[0];
            R2.B.y = M4x1B2.m[1];
            R2.B.z = M4x1B2.m[2];
            R2.C.x = M4x1C2.m[0];
            R2.C.y = M4x1C2.m[1];
            R2.C.z = M4x1C2.m[2];
            R2.D.x = M4x1D2.m[0];
            R2.D.y = M4x1D2.m[1];
            R2.D.z = M4x1D2.m[2];

            RotateZ = false;
        }

        if (Transform)
        {
            M4x1A2 = Calc.Multiply(M4x4, M4x1A);
            M4x1B2 = Calc.Multiply(M4x4, M4x1B);
            M4x1C2 = Calc.Multiply(M4x4, M4x1C);
            M4x1D2 = Calc.Multiply(M4x4, M4x1D);

            R2.A.x = M4x1A2.m[0];
            R2.A.y = M4x1A2.m[1];
            R2.A.z = M4x1A2.m[2];
            R2.B.x = M4x1B2.m[0];
            R2.B.y = M4x1B2.m[1];
            R2.B.z = M4x1B2.m[2];
            R2.C.x = M4x1C2.m[0];
            R2.C.y = M4x1C2.m[1];
            R2.C.z = M4x1C2.m[2];
            R2.D.x = M4x1D2.m[0];
            R2.D.y = M4x1D2.m[1];
            R2.D.z = M4x1D2.m[2];

            //M = new Matrix3x3(Size);
            //M.m[0, 0] = -1;
            //M.m[0, 1] = 4;
            //M.m[0, 2] = -3;
            //M.m[1, 0] = 5;
            //M.m[1, 1] = 0;
            //M.m[1, 2] = -2;

            //M.PrintAll(Size);
            //M.Print(Num);
            Transform = false;
        }

        if(Scale)
        {
            M4x1A2 = Calc.Scale(M4x4, M4x1A, M4x4S.x, M4x4S.y, M4x4S.z);
            M4x1B2 = Calc.Scale(M4x4, M4x1B, M4x4S.x, M4x4S.y, M4x4S.z);
            M4x1C2 = Calc.Scale(M4x4, M4x1C, M4x4S.x, M4x4S.y, M4x4S.z);
            M4x1D2 = Calc.Scale(M4x4, M4x1D, M4x4S.x, M4x4S.y, M4x4S.z);

            R2.A.x = M4x1A2.m[0];
            R2.A.y = M4x1A2.m[1];
            R2.A.z = M4x1A2.m[2];
            R2.B.x = M4x1B2.m[0];
            R2.B.y = M4x1B2.m[1];
            R2.B.z = M4x1B2.m[2];
            R2.C.x = M4x1C2.m[0];
            R2.C.y = M4x1C2.m[1];
            R2.C.z = M4x1C2.m[2];
            R2.D.x = M4x1D2.m[0];
            R2.D.y = M4x1D2.m[1];
            R2.D.z = M4x1D2.m[2];

            Scale = false;
        }
    }
}
