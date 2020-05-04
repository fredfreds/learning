using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Factory
{
    public class Unit
    {
        private int heath, armor;
        private Unit(int h, int a)
        {
            this.heath = h;
            this.armor = a;
        }

        public override string ToString()
        {
            return $"{heath}/{armor}";
        }

        public static class UnitFactory
        {
            public static Unit CreateUnit(int h, int a)
            {
                return new Unit(h, a);
            }
        }
    }

    public class Factory : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            InfoText.text = Unit.UnitFactory.CreateUnit(10, 5).ToString();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}