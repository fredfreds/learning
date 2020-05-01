using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Patterns.Prototype
{
    public interface IPrototype
    {
        IPrototype Clone();
    }

    public class Enemy : IPrototype
    {
        public int x, y;

        public Enemy(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Enemy(Enemy e)
        {
            this.x = e.x;
            this.y = e.y;
        }

        public virtual IPrototype Clone()
        {
            return new Enemy(this);
        }

        public override string ToString()
        {
            return $"{x} + {y}";
        }
    }

    public class SimpleEnemy : Enemy, IPrototype
    {
        public int z;

        public SimpleEnemy(int x, int y, int z) 
            : base(x, y)
        {
            this.z = z;
        }

        public SimpleEnemy(SimpleEnemy s)
            : base(s.x, s.y)
        {
            this.z = s.z;
        }

        public override IPrototype Clone()
        {
            return new SimpleEnemy(this);
        }

        public override string ToString()
        {
            return $"{x} + {y} + {z}";
        }
    }

    public class Prototype : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text CloneInfoText;
        public Text CloneInfoText2;
        public Text CloneInfoText3;
        public Text CloneInfoText4;
        public Text CloneInfoText5;

        IPrototype e;
        SimpleEnemy n;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            e = new SimpleEnemy(3, 1, 2);
            InfoText.text = e.ToString();

            IPrototype clone = e.Clone();
            CloneInfoText.text = clone.ToString();

            Enemy clone2 = (Enemy)e.Clone();
            CloneInfoText2.text = clone2.ToString();

            IPrototype clone3 = (Enemy)e.Clone();
            CloneInfoText3.text = clone3.ToString();

            n = new SimpleEnemy(3, 1, 2);
            CloneInfoText4.text = n.ToString();

            SimpleEnemy clone4 = (SimpleEnemy)n.Clone();
            CloneInfoText5.text = clone4.ToString();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}