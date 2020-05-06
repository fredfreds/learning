using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Proxy
{
    //--------------------------------------------VARIANT 1-------------------------------------

    public interface IUnit
    {
        string Do();
    }

    public class Unit : IUnit
    {
        public string Do()
        {
            return "Doing some action";
        }
    }

    public class Shield
    {
        public int Resist { get; set; }

        public Shield(int r)
        {
            this.Resist = r;
        }
    }

    public class UnitProxy : IUnit
    {
        private Shield shield;
        private Unit unit = new Unit();

        public UnitProxy(Shield s)
        {
            this.shield = s;
        }

        public string Do()
        {
            if(shield.Resist > 10)
            {
                return unit.Do();
            }
            else
            {
                return "Cant resist";
            }
        }
    }

    //--------------------------------------------VARIANT 2-------------------------------------

    public class Char
    {
        public int Armor { get; set; }
        public int Weight { get; set; }

        public string Resist()
        {
            return "Resist";
        }

        public string Pick()
        {
            return "Pick";
        }

        public string PickAndResist()
        {
            return "Pick and Resist";
        }
    }

    public class SimpleChar
    {
        public readonly Char c;

        public SimpleChar(Char c)
        {
            this.c = c;
        }

        public int Armor
        {
            get { return c.Armor; }
            set { c.Armor = value; }
        }

        public int Weight
        {
            get { return c.Weight; }
            set { c.Weight = value; }
        }

        public string Resist()
        {
            if (Armor > 10)
                return "Resist";
            return "Cant Resist";
        }

        public string Pick()
        {
            if (Weight < 10)
                return "Pick";
            return "Cant Pick";
        }

        public string PickAndResist()
        {
            return Pick() + "/" + Resist();
        }
    }

    public class Proxy : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;

        public int Resist;
        public int Weight;

        IUnit u;
        Char c;
        SimpleChar sc;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            u = new UnitProxy(new Shield(Resist));
            InfoText.text = u.Do();
            c = new Char { Armor = Resist, Weight = Weight };
            Info2Text.text = c.PickAndResist() + "/" + c.Pick() + "/" + c.Resist();
            sc = new SimpleChar(c);
            Info3Text.text = sc.PickAndResist() + "/" + sc.Pick() + "/" + sc.Resist();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}