using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Memento
{
    public class Unit
    {
        private int health;
        private List<UnitMemento> mementos = new List<UnitMemento>();
        private int cur;

        public Unit(int h)
        {
            this.health = h;
            mementos.Add(new UnitMemento(health));
        }

        public UnitMemento Set(int a)
        {
            health += a;
            UnitMemento m = new UnitMemento(health);
            mementos.Add(m);
            ++cur;
            return m;
        }

        public UnitMemento Restore(UnitMemento m)
        {
            if(m != null)
            {
                health = m.Health;
                mementos.Add(m);
                return m;
            }
            return null;
        }

        public UnitMemento Undo()
        {
            if(cur > 0)
            {
                UnitMemento m = mementos[--cur];
                health = m.Health;
                return m;
            }
            return null;
        }

        public UnitMemento Redo()
        {
            if(cur + 1 < mementos.Count)
            {
                UnitMemento m = mementos[++cur];
                health = m.Health;
                return m;
            }
            return null;
        }

        public override string ToString()
        {
            return $"{health}";
        }
    }

    public class UnitMemento
    {
        public int Health { get; }
        
        public UnitMemento(int h)
        {
            this.Health = h;
        }
    }

    public class Memento : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;
        public Text Info5Text;
        public Text Info6Text;
        public Text Info7Text;

        public Unit u;
        public UnitMemento m1;
        public UnitMemento m2;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            u = new Unit(10);
            m1 = u.Set(10);
            m2 = u.Set(20);

            InfoText.text = u.ToString();

            u.Undo();

            Info2Text.text = u.ToString();

            u.Undo();

            Info3Text.text = u.ToString();

            u.Redo();

            Info4Text.text = u.ToString();

            u.Restore(m1);

            Info5Text.text = u.ToString();

            u.Restore(m2);

            Info6Text.text = u.ToString();

            u.Undo();

            Info7Text.text = u.ToString();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}