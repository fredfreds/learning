using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.ChainOfResponsibility
{
    public class Unit
    {
        public string Name;
        public int Health;

        public Unit(string n, int h)
        {
            this.Name = n;
            this.Health = h;
        }

        public override string ToString()
        {
            return $"{Name}, {Health}";
        }
    }

    public class UnitModifier
    {
        protected Unit unit;
        protected UnitModifier next;

        public UnitModifier(Unit u)
        {
            this.unit = u;
        }

        public void Add(UnitModifier m)
        {
            if(next != null)
            {
                next.Add(m);
            }
            else
            {
                next = m;
            }
        }

        public virtual string Handle() => next?.Handle();
    }

    public class NoModifier : UnitModifier
    {
        public NoModifier(Unit u)
            : base(u)
        { }

        public override string Handle()
        {
            return null;
        }
    }

    public class IncreaseHealthModifier : UnitModifier
    {
        public IncreaseHealthModifier(Unit u) 
            : base(u) 
        { }

        public override string Handle()
        {
            unit.Health *= 2;
            base.Handle();
            return $"{unit.Health}";
        }
    }

    public class IncreaseDoubleHealthModifier : UnitModifier
    {
        public IncreaseDoubleHealthModifier(Unit u)
            : base(u)
        { }

        public override string Handle()
        {
            unit.Health *= 3;
            base.Handle();
            return $"{unit.Health}";
        }
    }

    public class ChainOfResponsibility : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;

        Unit u;
        UnitModifier m;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            u = new Unit("Fred", 4);
            InfoText.text = u.ToString();

            m = new UnitModifier(u);
            m.Add(new IncreaseHealthModifier(u));
            m.Add(new IncreaseDoubleHealthModifier(u));
            m.Handle();
            Info2Text.text = u.ToString();

            //m.Add(new IncreaseDoubleHealthModifier(u));
            //Info3Text.text = u.ToString();

            //m.Add(new NoModifier(u));
            //Info4Text.text = u.ToString();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}