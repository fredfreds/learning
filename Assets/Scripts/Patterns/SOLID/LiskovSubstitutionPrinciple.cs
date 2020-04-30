using System;
using UnityEngine;

namespace SOLID
{
    public class Hero
    {
        //public int Height { get; set; }
        public virtual int Height { get; set; }
        //public int Health { get; set; }
        public virtual int Health { get; set; }

        public Hero()
        {
            
        }

        public Hero(int height, int health)
        {
            Height = height;
            Health = health;
        }

        public override string ToString()
        {
            return $"{nameof(Height)}: {Height}, {nameof(Health)}: {Health}";
        }
    }

    public class HeroFriend : Hero
    {
        //        public new int Health
        public override int Health
        {
            set => base.Health = base.Height = value;
        }
        
        //        public new int Height
        public override int Height
        {
            set => base.Height = base.Health = value;
        }
    }

    
    public class LiskovSubstitutionPrinciple : MonoBehaviour
    {
        public UnityEngine.UI.Text Text;
        public UnityEngine.UI.Button CalcButton;
        int Calc(Hero h) => h.Health * h.Height;
        public int Val = 2;
        public HeroFriend hf = new HeroFriend();

        private void OnEnable()
        {
            CalcButton.onClick.AddListener(Calculate);
        }

        private void Calculate()
        {
            hf.Health = Val; 
            Text.text = $"{hf} has {Calc(hf).ToString()}";
        }

        private void OnDisable()
        {
            CalcButton.onClick.RemoveListener(Calculate);
        }
    }
}