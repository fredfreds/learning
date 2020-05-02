using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Patterns.Bridge
{
    public interface IData
    {
        bool Contain(string n);
        string Show();
    }

    public class DataOne : IData
    {
        public bool Contain(string n)
        {
            if (n == "Unit")
                return true;
            else
                return false;
        }

        public string Show()
        {
            return "Data for Unit";
        }
    }

    public class DataTwo : IData
    {
        public bool Contain(string n)
        {
            if (n == "Enemy")
                return true;
            else
                return false;
        }

        public string Show()
        {
            return "Data for Enemy";
        }
    }

    public class Unit
    {
        private IData dt;

        public Unit(IData dt)
        {
            this.dt = dt;
        }

        public string Check(string n)
        {
            if (dt.Contain(n))
            {
                return $"Data contains {n}";
            }
            else
            {
                return "Not found";
            }
        }

        public string GetData()
        {
            return dt.Show();
        }

        public virtual string GetUnitType()
        {
            return "Unit";
        }
    }

    public class Enemy : Unit
    {
        public Enemy(IData dt)
            : base(dt)
        {
        }

        public override string GetUnitType()
        {
            return "Enemy";
        }
    }

    public class Bridge : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;

        IData d1 = new DataOne();
        IData d2 = new DataTwo();
        Unit u1;
        Unit u2;
        Unit u3;
        Unit u4;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            u1 = new Unit(d1);
            u2 = new Unit(d2);
            u3 = new Enemy(d1);
            u4 = new Enemy(d2);

            InfoText.text = u1.Check("Unit") + " " + u1.GetData() + " " + u1.GetUnitType();
            Info2Text.text = u2.Check("Unit") + " " + u2.GetData() + " " + u2.GetUnitType();
            Info3Text.text = u3.Check("Enemy") + " " + u3.GetData() + " " + u3.GetUnitType();
            Info4Text.text = u4.Check("Enemy") + " " + u4.GetData() + " " + u4.GetUnitType();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}