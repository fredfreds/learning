using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Observer
{
    //--------------------------------------------VARIANT 1------------------------------------

    public class UnitChangeArgs
    {
        public string Info;
    }

    public class Unit
    {
        public void UnitChange()
        {
            UnitEvent?.Invoke(this,
                new UnitChangeArgs { Info = "Some Info" });
        }

        public event EventHandler<UnitChangeArgs> UnitEvent;
    }

    //--------------------------------------------VARIANT 2------------------------------------

    public class Prop
    {
        public BindingList<float> States = new BindingList<float>();
        public void AddStates(float s)
        {
            States.Add(s);
        }
    }

    public class Observer : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;
        public Text Info5Text;
        public Text Info6Text;
        public Text Info7Text;

        Unit u = new Unit();

        Prop p = new Prop();

        private string info = "info";
        private float infoF = 0;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            u.UnitEvent += Call;
            InfoText.text = info;

            u.UnitChange();
            Info2Text.text = info;

            u.UnitEvent -= Call;
            Info3Text.text = info;

            u.UnitChange();
            Info4Text.text = info;

            p.States.ListChanged += Call2;
            Info5Text.text = infoF.ToString() + "/" + p.States.Count;

            p.AddStates(10);
            Info6Text.text = infoF.ToString();

            p.States.ListChanged -= Call2;
            Info7Text.text = infoF.ToString() + "/" + p.States.Count; ;
        }

        private void Call2(object o, ListChangedEventArgs e)
        {
            if(e.ListChangedType == ListChangedType.ItemAdded)
            {
                float f = ((BindingList<float>)o)[e.NewIndex];
                infoF = f;
            }
        }

        private void Call(object s, UnitChangeArgs u)
        {
            info = u.Info;
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}