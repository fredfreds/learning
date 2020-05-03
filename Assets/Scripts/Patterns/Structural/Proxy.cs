using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Patterns.Proxy
{
    public interface IAtmosphereService
    {
        int Temperature();
        int Wind();
        void Location(string city);
    }

    public enum LevelType
    {
        LevelOne,
        LevelTwo
    }

    public class WorldOne : IAtmosphereService
    {
        private string city;

        public void Location(string city)
        {
            this.city = city;
        }

        public int Temperature()
        {
            switch (city)
            {
                case "LevelOne":
                    return 10;
                case "LevelTwo":
                    return 15;
                default:
                    return 0;
            }
        }

        public int Wind()
        {
            switch (city)
            {
                case "LevelOne":
                    return 5;
                case "LevelTwo":
                    return 7;
                default:
                    return 0;
            }
        }
    }

    public class WorldTwo
    {
        public int Temperature(int la, int lo)
        {
            if (la == 30 && lo == 30)
            {
                return 20;
            }
            else
            {
                if (la == 20 && lo == 20)
                {
                    return 10;
                }
                else
                {
                    return 5;
                }
            }
        }

        public int Wind(int la, int lo)
        {
            if (la == 30 && lo == 30)
            {
                return 25;
            }
            else
            {
                if (la == 20 && lo == 20)
                {
                    return 15;
                }
                else
                {
                    return 7;
                }
            }
        }
    }

    public class WorldThree : IAtmosphereService
    {
        private string city;

        public void Location(string city)
        {
            this.city = city;
        }

        public int Temperature()
        {
            switch (city)
            {
                case "LevelOne":
                    return 20;
                case "LevelTwo":
                    return 25;
                default:
                    return 0;
            }
        }

        public int Wind()
        {
            switch (city)
            {
                case "LevelOne":
                    return 15;
                case "LevelTwo":
                    return 17;
                default:
                    return 0;
            }
        }
    }

    public class WorldTwoAdapter : IAtmosphereService
    {
        private int la;
        private int lo;

        private WorldTwo worldTwo;

        public WorldTwoAdapter(WorldTwo w)
        {
            worldTwo = w;
        }

        public void Location(string city)
        {
            switch (city)
            {
                case "LevelOne":
                    la = 20;
                    lo = 20;
                    break;
                case "LevelTwo":
                    la = 30;
                    lo = 30;
                    break;
            }
        }

        public int Temperature()
        {
            return worldTwo.Temperature(la, lo) / 2;
        }

        public int Wind()
        {
            return worldTwo.Wind(la, lo) / 2;
        }
    }

    public class WorldThreeProxy : IAtmosphereService
    {
        private WorldThree worldThree;

        public WorldThreeProxy()
        {
            worldThree = new WorldThree();
        }

        public void Location(string city)
        {
            worldThree.Location(city);
        }

        public int Temperature()
        {
            return worldThree.Temperature();
        }

        public int Wind()
        {
            return worldThree.Wind();
        }
    }

    public class Proxy : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;

        public LevelType l = LevelType.LevelOne;

        IAtmosphereService w = new WorldOne();
        IAtmosphereService w2 = new WorldTwoAdapter(new WorldTwo());
        IAtmosphereService w3 = new WorldThreeProxy();

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            w.Location(l.ToString());
            InfoText.text = $"{w.Temperature()} + {w.Wind()} = {w.Temperature() + w.Wind()}";
            w2.Location(l.ToString());
            Info2Text.text = $"{w2.Temperature()} + {w2.Wind()} = {w2.Temperature() + w2.Wind()}";
            w3.Location(l.ToString());
            Info3Text.text = $"{w3.Temperature()} + {w3.Wind()} = {w3.Temperature() + w3.Wind()}";
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}