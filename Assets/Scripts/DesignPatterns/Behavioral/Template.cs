using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Template
{
    public abstract class Unit
    {
        public string Do()
        {
            if (Turn == 1)
                return Hit();
            else
                return Take();
        }

        protected abstract string Hit();

        protected abstract string Take();
        protected abstract int Turn { get; }
    }

    public class SimpleUnit : Unit
    {
        private int simpleTurn;
        public SimpleUnit(int t)
        {
            simpleTurn = t;
        }

        protected override int Turn => simpleTurn;

        protected override string Hit()
        {
            return $"Hit Simple";
        }

        protected override string Take()
        {
            return $"Take Simple";
        }
    }

    public class ComplexUnit : Unit
    {
        private int complexTurn;
        public ComplexUnit(int t)
        {
            complexTurn = t;
        }

        protected override int Turn => complexTurn;

        protected override string Hit()
        {
            return $"Hit Complex";
        }

        protected override string Take()
        {
            return $"Take Complex";
        }
    }

    public class Template : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;

        Unit u1;
        Unit u2;
        public int t1;
        public int t2;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            u1 = new SimpleUnit(t1);
            u2 = new ComplexUnit(t2);

            InfoText.text = u1.Do();
            Info2Text.text = u2.Do();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}