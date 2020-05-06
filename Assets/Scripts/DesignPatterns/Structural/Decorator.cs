using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Decorator
{
    public interface IFlyUnit
    {
        string Fly();
        int Speed { get; set; }
    }

    public interface IWalkUnit
    {
        string Walk();
    }

    public class FlyUnit : IFlyUnit
    {
        public string Fly()
        {
            return $"Fly with speed {Speed}";
        }
        
        public int Speed { get ; set; }
    }

    public class WalkUnit : IWalkUnit
    {
        public string Walk()
        {
            return $"Walk with speed {Speed}";
        }

        public int Speed { get; set; }
    }

    public class SimpleUnit : IFlyUnit, IWalkUnit
    {
        private FlyUnit flyUnit = new FlyUnit();
        private WalkUnit walkUnit = new WalkUnit();
        private int speed;

        public string Fly()
        {
            return flyUnit.Fly();
        }

        public string Walk()
        {
            return walkUnit.Walk();
        }

        public int Speed { get { return speed; } 
            set { speed = value;
                flyUnit.Speed = value;
                walkUnit.Speed = value;
            } }
    }

    public interface IProp
    {
        string GetPosition();
    }

    public class SimpleProp : IProp
    {
        private float posX, posY;

        public SimpleProp(float x, float y)
        {
            this.posX = x;
            this.posY = y;
        }

        public string GetPosition()
        {
            return $"Prop position at {posX}, {posY}";
        }
    }

    public class SimpleColorProp : IProp
    {
        private IProp prop;
        private string color;

        public SimpleColorProp(IProp p, string c)
        {
            this.prop = p;
            this.color = c;
        }

        public string GetPosition()
        {
            return $"{color} {prop.GetPosition()}";
        }
    }

    public class ComplexColorProp : IProp
    {
        private IProp prop;
        private bool hasShield;

        public ComplexColorProp(IProp p, bool b)
        {
            this.prop = p;
            this.hasShield = b;
        }

        public string GetPosition()
        {
            return $"{prop.GetPosition()} with {nameof(hasShield)} : {hasShield}";
        }
    }

    public class Decorator : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;
        public Text Info5Text;
        public Text Info6Text;

        SimpleUnit u = new SimpleUnit();
        IProp s;
        IProp s2;
        IProp s3;
        IProp s4;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            u.Speed = 10;
            InfoText.text = u.Fly();
            Info2Text.text = u.Walk();
            s = new SimpleProp(10, 10);
            Info3Text.text = s.GetPosition();
            s2 = new SimpleColorProp(s, "Green");
            Info4Text.text = s2.GetPosition();
            s3 = new ComplexColorProp(s2, true);
            Info5Text.text = s3.GetPosition();
            s4 = new ComplexColorProp(s, true);
            Info6Text.text = s4.GetPosition();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}