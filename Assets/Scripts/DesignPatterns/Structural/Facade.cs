using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Facade
{
    public class SomeComplexCode
    {
        public string GetCode()
        {
            return "Complex Operations";
        }
    }

    public class MoreComplexCode
    {
        public string GetCode()
        {
            return "More Complex Code";
        }
    }

    public class FacadePattern
    {
        public string GetCode()
        {
            SomeComplexCode sc = new SomeComplexCode();
            MoreComplexCode mc = new MoreComplexCode();
            return sc.GetCode() + "/" + mc.GetCode();
        }
    }

    public class Facade : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;

        FacadePattern fp = new FacadePattern();

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            InfoText.text = fp.GetCode();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}