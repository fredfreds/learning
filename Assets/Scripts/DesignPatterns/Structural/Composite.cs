using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Composite
{
    public class Unit
    {
        public virtual string Name { get; set; } = "Units";

        private Lazy<List<Unit>> children = new Lazy<List<Unit>>();

        public List<Unit> Children => children.Value;

        private void Print(StringBuilder sb, int d)
        {
            sb.AppendLine(Name);
            foreach (var item in Children)
            {
                item.Print(sb, d + 1);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Print(sb, 0);
            return sb.ToString();
        }
    }

    public class SimpleUnit : Unit
    {
        public override string Name => "Simple";
    }

    public class ComplexUnit : Unit
    {
        public override string Name => "Complex";
    }

    public class Composite : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info1Text;
        public Text Info2Text;

        Unit u;
        Unit g;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            u = new Unit();
            u.Name = "Unit";
            u.Children.Add(new SimpleUnit());
            u.Children.Add(new ComplexUnit());

            InfoText.text = u.ToString();

            g = new Unit();
            g.Children.Add(new SimpleUnit());
            g.Children.Add(new ComplexUnit());
            u.Children.Add(g);

            Info1Text.text = g.ToString();

            Info2Text.text = u.ToString();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}