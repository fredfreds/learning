using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Patterns.Singleton
{
    // Lazy
    public class Singleton1
    {
        private Singleton1()
        {
        }

        private static Singleton1 instance = null;

        public static Singleton1 Instance()
        {
            if(instance == null)
            {
                return new Singleton1();
            }

            return instance;
        }
    }

    // Thread safe
    public class Singleton2
    {
        private Singleton2()
        {
        }

        private static Singleton2 instance = new Singleton2();

        public static Singleton2 Instance()
        {
            return instance;
        }
    }

    // synchronized
    public class Singleton3
    {
        private static object Lock = new object();

        private Singleton3()
        {
        }

        private static volatile Singleton3 instance = null;

        public static Singleton3 Instance()
        {
            lock (Lock)
            {
                if (instance == null)
                {
                    return new Singleton3();
                }

                return instance;
            }
        }
    }

    public class Singleton : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoS1Text;
        public Text InfoS11Text;
        public Text InfoS2Text;
        public Text InfoS21Text;
        public Text InfoS3Text;
        public Text InfoS31Text;
        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            Singleton1 s1 = Singleton1.Instance();
            Singleton1 s11 = s1;
            InfoS1Text.text = s1.GetHashCode().ToString();
            InfoS11Text.text = s11.GetHashCode().ToString();
            InfoS2Text.text = Singleton2.Instance().GetHashCode().ToString();
            InfoS21Text.text = Singleton2.Instance().GetHashCode().ToString();
            Singleton3 s3 = Singleton3.Instance();
            Singleton3 s31 = s3;
            InfoS3Text.text = s3.GetHashCode().ToString();
            InfoS31Text.text = s31.GetHashCode().ToString();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}