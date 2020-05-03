using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Patterns.Composite
{
    public class Enemy
    {
        private int x, y;

        public Enemy(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Move(int dx, int dy)
        {
            x += dx;
            y += dy;
        }

        public int X { get { return x; } }
        public int Y { get { return y; } }

        public string Show()
        {
            return $"{X} + {Y}";
        }
    }

    public class EnemyFactory
    {
        public Enemy CreateEnemyOne()
        {
            return new Enemy(Random.Range(0, 100), Random.Range(0, 100));
        }

        public Enemy CreateEnemyTwo()
        {
            return new Enemy(Random.Range(100, 200), Random.Range(100, 200));
        }

        public Enemy CreateEnemyThree()
        {
            return new Enemy(Random.Range(100, 200), Random.Range(100, 200));
        }
    }

    public class Flyweight : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {

        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}