using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Interpreter
{
    public class UnitProcessor
    {
        public Dictionary<char, int> Units = new Dictionary<char, int>();

        public enum Operation
        {
            Nothing,
            Add
        }

        public void Clear()
        {
            Units.Clear();
        }

        public int Result(string s)
        {
            int cur = 0;
            Operation op = Operation.Nothing;
            string[] parts = Regex.Split(s, @"(?<=[+])");

            foreach (var item in parts)
            {
                string[] ns = item.Split(new[] { "+" }, System.StringSplitOptions.RemoveEmptyEntries);
                string f = ns[0];
                int v, z;

                if(int.TryParse(f, out z))
                {
                    v = z;
                }
                else if(f.Length == 1 && Units.ContainsKey(f[0]))
                {
                    v = Units[f[0]];
                }
                else
                {
                    return 0;
                }

                switch (op)
                {
                    case Operation.Nothing:
                        cur = v;
                        break;
                    case Operation.Add:
                        cur += v;
                        break;
                    default:
                        break;
                }

                if(item.EndsWith("+"))
                {
                    op = Operation.Add;
                }
            }
            return cur;
        }
    }

    public class Interpreter : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;

        public string S;

        UnitProcessor u = new UnitProcessor();

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            u.Units.Add('a', 10);
            InfoText.text = u.Result(S).ToString();
            u.Clear();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}