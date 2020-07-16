using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Collections
{

    public class UnityCountry : MonoBehaviour
    {
        public Text T1;
        public Text T2;

        public Button B;
        public int ID;

        public event EventHandler<UnitChangeArgs> UnitEvent;

        void Start()
        {
            B = GetComponent<Button>();
            B.onClick.AddListener(Press);
        }

        void OnDisable()
        {
            B.onClick.RemoveListener(Press);
        }

        public void Press()
        {
            UnitChange(ID);
        }

        public void UnitChange(int id)
        {
            UnitEvent?.Invoke(this,
                new UnitChangeArgs { ID = id });
        }
    }

    public class UnitChangeArgs
    {
        public int ID;
    }
}