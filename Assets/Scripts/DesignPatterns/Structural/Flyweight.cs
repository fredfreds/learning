using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Flyweight
{
    public class Unit
    {
        private string[] stats;
        private Dictionary<int, UnitToken> statTokens = new Dictionary<int, UnitToken>();

        public Unit(string n)
        {
            this.stats = n.Split(' ');
        }

        public class UnitToken
        {
            public bool Increase;
        }

        public UnitToken this[int index]
        {
            get
            {
                UnitToken ut = new UnitToken();
                statTokens.Add(index, ut);
                return statTokens[index];
            }
        }

        public override string ToString()
        {
            List<string> ts = new List<string>();

            for (int i = 0; i < stats.Length; i++)
            {
                string s = stats[i];
                if(statTokens.ContainsKey(i) && statTokens[i].Increase)
                {
                    s = s.ToUpperInvariant();
                }

                ts.Add(s);
            }

            return string.Join(" ", ts);
        }
    }

    public class Flyweight : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            Unit u = new Unit("mega super cool master");
            u[0].Increase = true;
            u[2].Increase = true;
            InfoText.text = u.ToString();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}