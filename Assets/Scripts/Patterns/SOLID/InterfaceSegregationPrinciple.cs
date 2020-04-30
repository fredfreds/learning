using System;
using UnityEngine;

namespace SOLID
{
    public class House
    {
        
    }

    // public interface IBuilder
    // {
    //     void Build(House h);
    //     void Move(House h);
    //     void Destroy(House h);
    // }

    public interface IBuilder
    {
        string Build(House h);
    }
    
    public interface IDestroyer
    {
        string Destroy(House h);
    }
    
    public interface IMover
    {
        string Move(House h);
    }
    
    public  interface IMultiBuilder : IBuilder, IDestroyer, IMover {}
    
    public class MultiBuilder : IMultiBuilder
    {
        public string Build(House h)
        {
            return "Build";
        }

        public string Move(House h)
        {
            return "Move";
        }

        public string Destroy(House h)
        {
            return "Destroy";
        }
    }

    public class OldBuilder : IBuilder
    {
        public string Build(House h)
        {
            return "Build";
        }
    }

    public class InterfaceSegregationPrinciple : MonoBehaviour
    {
        MultiBuilder mb = new MultiBuilder();
        OldBuilder ob = new OldBuilder();
        House h = new House();
        public UnityEngine.UI.Text Text;
        public UnityEngine.UI.Button MultiButton;
        public UnityEngine.UI.Button OldButton;

        private void OnEnable()
        {
            MultiButton.onClick.AddListener(GetInfoMB);
            OldButton.onClick.AddListener(GetInfoOB);
        }

        private void OnDisable()
        {
            MultiButton.onClick.RemoveListener(GetInfoMB);
            OldButton.onClick.RemoveListener(GetInfoOB);
        }

        private void GetInfoMB()
        {
            Text.text = $"{mb.Build(h)} {mb.Destroy(h)} {mb.Move(h)}";
        }
        
        private void GetInfoOB()
        {
            Text.text = $"{ob.Build(h)}";
        }
    }
}