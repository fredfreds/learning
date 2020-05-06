using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Adapter
{
    public class Unit
    {
        public int Health;
    }

    public interface IUnit
    {
        int Health { get; }
    }

    public static class ExtensionMethods
    {
        public static int GetHealth(this IUnit u)
        {
            return u.Health * 10 / 100;
        }
    }

    public class UnitToIUnitAdapter : IUnit
    {
        private readonly Unit unit;

        public UnitToIUnitAdapter(Unit u)
        {
            this.unit = u;
        }

        public int Health => unit.Health;

        public int GetNewHealth()
        {
            return unit.Health * 10 / 100;
        }
    }

    public class Adapter : MonoBehaviour
    {
        public Button BuildButton;

        public Text InfoText;

        Unit u;
        UnitToIUnitAdapter a;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            u = new Unit();
            u.Health = 100;

            a = new UnitToIUnitAdapter(u);
            InfoText.text = u.Health.ToString() + "/" + a.GetHealth().ToString()
                + "/" + a.GetNewHealth().ToString(); 
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}