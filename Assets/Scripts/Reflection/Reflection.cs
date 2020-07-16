using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Reflection
{
    public class UnitA
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Pos { get; set; }
    }

    public class UnitB
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class Reflection : MonoBehaviour
    {
        public Button RunButton;
        public Text InfoText;
        public Text InfoText2;

        void OnEnable()
        {
            RunButton.onClick.AddListener(Run);
        }

        void OnDisable()
        {
            RunButton.onClick.RemoveListener(Run);
        }

        private void Run()
        {
            UnitA unitA = new UnitA { ID = 1, Name = "Fred", Pos = "3,1,3" };
            UnitB unitB = new UnitB();

            var propA = unitA.GetType().GetProperties();

            foreach (var item in propA)
            {
                var propB = unitB.GetType().GetProperty(item.Name);

                if (propB != null)
                    propB.SetValue(unitB, item.GetValue(unitA));
            }

            InfoText.text = $"{unitA.ID} : {unitA.Name} : {unitA.Pos}";
            InfoText2.text = $"{unitB.ID} : {unitB.Name}";
        }
    }
}