using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Patterns.CreationalTest
{
    public interface IClone
    {
        IClone Clone();
    }

    public abstract class SceneObject 
    {
        public string ObjectColor { get; set; } = "black";
        public SceneObject()
        {
        }

        public SceneObject(string color)
        {
            this.ObjectColor = color;
        }

        public string GetColor()
        {
            return ObjectColor;
        }

        public virtual string Draw()
        {
            return $"{ObjectColor}";
        }
    }

    public class Coords
    {
        private int x, y;
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public Coords()
        {

        }

        public Coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Point : SceneObject, IClone
    {
        private Coords coords = new Coords();
        public int X { get => coords.X; set => coords.X = value; }
        public int Y { get => coords.Y; set => coords.Y = value; }

        public Point(int x, int y)
            : base()
        {
            coords = new Coords(x, y);
        }

        public Point(int x, int y, string color)
            : base(color)
        {
            coords = new Coords(x, y);
        }

        public Point(Point p)
        {
            this.X = p.X;
            this.Y = p.Y;
            this.ObjectColor = p.ObjectColor;
        }

        public IClone Clone()
        {
            return new Point(this);
        }

        public override string Draw()
        {
            return $"{X} + {Y} + {ObjectColor}";
        }
    }

    public class Scene
    {
        private List<SceneObject> objects;
        private Scene()
        {
            objects = new List<SceneObject>();
        }

        public string Draw()
        {
            string t = "";

            for (int i = 0; i < objects.Count; i++)
            {
                t += objects[i].Draw() + " ";
            }

            return t;
        }

        public void Add(SceneObject o)
        {
            objects.Add(o);
        }

        public void Clear()
        {
            objects.Clear();
        }

        private static Scene instance = new Scene();
        public static Scene GetInstance()
        {
            return instance;
        }
    }

    public abstract class AbstractFactory
    {
        public abstract Point CreatePoint();
    }

    public class ColorFactory : AbstractFactory
    {
        public override Point CreatePoint()
        {
            Point p = new Point(8, 8);
            Scene.GetInstance().Add(p);
            return p;
        }
    }

    public class TestPatterns : MonoBehaviour
    {
        public Button BuildButton;
        public Button ClearButton;

        public Text InfoText;
        public Text InfoCloneText;

        Scene s = Scene.GetInstance();

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
            ClearButton.onClick.AddListener(Clear);
        }

        private void Clear()
        {
            s.Clear();
            InfoText.text = s.Draw();
        }

        private void Build()
        {
            Point p0 = new Point(3, 3);
            Point p1 = (Point)p0.Clone();
            Point p2 = new Point(1, 2, "white");

            
            s.Add(p0);
            s.Add(p1);
            s.Add(p2);

            InfoText.text = s.Draw();

            AbstractFactory f = new ColorFactory();
            f.CreatePoint().ObjectColor = "red";

            InfoCloneText.text = s.Draw();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
            ClearButton.onClick.RemoveListener(Clear);
        }
    }
}