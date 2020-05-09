using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Visitor
{
    public abstract class BusterVisitor
    {
        public abstract string Visit(SimpleBuster s);
        public abstract string Visit(ComplexBuster c);
    }

    public abstract class Buster
    {
        public abstract string Accept(BusterVisitor bv);
    }

    public class SimpleBuster : Buster
    {
        public readonly string Ability;

        public SimpleBuster(string p)
        {
            this.Ability = p;
        }

        public override string Accept(BusterVisitor bv)
        {
             return bv.Visit(this);
        }
    }

    public class ComplexBuster : Buster
    {
        public readonly Buster First, Second;

        public ComplexBuster(Buster f, Buster s)
        {
            this.First = f;
            this.Second = s;
        }

        public override string Accept(BusterVisitor bv)
        {
            return bv.Visit(this);
        }
    }

    public class BusterManager : BusterVisitor
    {
        public override string Visit(SimpleBuster s)
        {
            return $"Simple: {s.Ability}";
        }

        public override string Visit(ComplexBuster c)
        {
            return $"Complex: {c.First.Accept(this)} and {c.Second.Accept(this)}";
        }
    }

    public class Visitor : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;

        SimpleBuster s1;
        SimpleBuster s2;
        ComplexBuster c;
        BusterManager bm = new BusterManager();

        public string Ability1;
        public string Ability2;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            s1 = new SimpleBuster(Ability1);
            s2 = new SimpleBuster(Ability2);
            c = new ComplexBuster(s1, s2);

            InfoText.text = bm.Visit(s1);
            Info2Text.text = bm.Visit(s2);
            Info3Text.text = bm.Visit(c);
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}