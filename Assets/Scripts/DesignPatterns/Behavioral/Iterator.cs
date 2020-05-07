using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Iterator
{
    public class Unit : IEnumerable<int>
    {
        private int[] stats = new int[2];

        private int health = 0;
        private int stamina = 1;

        public int Health
        {
            get => stats[health];
            set => stats[health] = value;
        }

        public int Stamina
        {
            get => stats[stamina];
            set => stats[stamina] = value;
        }

        public int AverageStats => (int)stats.Average();

        public IEnumerator<int> GetEnumerator()
        {
            return stats.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int this[int index]
        {
            get => stats[index];
            set => stats[index] = value;
        }
    }

    public class Iterator : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info1Text;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;
        public Text Info5Text;

        Unit u = new Unit();

        public int h;
        public int h1;
        public int s;
        public int s1;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            u.Health = h;
            InfoText.text = u.Health.ToString();
            u[0] = h1;
            Info1Text.text = u.Health.ToString();
            u.Average();
            Info2Text.text = u.Average().ToString();
            u.Stamina = s;
            Info3Text.text = u.Stamina.ToString();
            u[1] = s1;
            Info4Text.text = u.Stamina.ToString();
            u.Average();
            Info5Text.text = u.Average().ToString();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}