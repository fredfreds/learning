using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Bridge
{
    public interface IBusterPlatform
    {
        string Bust(float a);
    }

    public class AndroidBusterPlatform : IBusterPlatform
    {
        public string Bust(float a)
        {
            return $"Bust for {a} on ANDROID";
        }
    }

    public class iOSBusterPlatform : IBusterPlatform
    {
        public string Bust(float a)
        {
            return $"Bust for {a} on iOS";
        }
    }

    public abstract class BusterItem
    {
        protected IBusterPlatform buster;
        protected float amount;

        protected BusterItem(IBusterPlatform b, float a)
        {
            this.buster = b;
            this.amount = a;
        }

        public abstract string Give();
        public abstract string Increase(float f);
    }

    public class SimpleBuster : BusterItem
    {
        public SimpleBuster(IBusterPlatform p, float a)
            : base(p, a)
        {
        }

        public override string Give()
        {
            return buster.Bust(amount);
        }

        public override string Increase(float f)
        {
            amount *= f;
            return $"Increase for {f} to {amount} power";
        }
    }

    public class ComplexBuster : BusterItem
    {
        public ComplexBuster(IBusterPlatform p, float a)
            : base(p, a)
        {
        }

        public override string Give()
        {
            return buster.Bust(amount);
        }

        public override string Increase(float f)
        {
            amount *= f;
            return $"Increase for {f} to {amount} power";
        }
    }

    public class Bridge : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info1Text;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;
        public Text Info5Text;

        IBusterPlatform p;
        BusterItem b;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            p = new AndroidBusterPlatform();
            b = new SimpleBuster(p, 10);

            InfoText.text = b.Give();
            Info1Text.text = b.Increase(4);
            Info2Text.text = b.Give();

            p = new iOSBusterPlatform();
            b = new ComplexBuster(p, 40);

            Info3Text.text = b.Give();
            Info4Text.text = b.Increase(4);
            Info5Text.text = b.Give();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}