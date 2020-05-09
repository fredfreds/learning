using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Strategy
{
    public enum StrategyType
    {
        First,
        Second
    }

    public interface IStrategy
    {
        string Init();
        string Do();
        string End();
    }

    public class StrategyOne : IStrategy
    {
        public string Do()
        {
            return "Strategy One DO";
        }

        public string End()
        {
            return "Strategy One END";
        }

        public string Init()
        {
            return "Strategy One INIT";
        }
    }

    public class StrategyTwo : IStrategy
    {
        public string Do()
        {
            return "Strategy Two DO";
        }

        public string End()
        {
            return "Strategy Two END";
        }

        public string Init()
        {
            return "Strategy Two INIT";
        }
    }

    public class DynamicStrategyManager
    {
        private IStrategy strategy;
        public void SetStrategy(StrategyType t)
        {
            switch (t)
            {
                case StrategyType.First:
                    strategy = new StrategyOne();
                    break;
                case StrategyType.Second:
                    strategy = new StrategyTwo();
                    break;
                default:
                    break;
            }
        }

        public string Start()
        {
            return $"{strategy.Init()} + {strategy.Do()} + {strategy.End()}";
        }
    }

    public class StaticStrategyManager<ST> where ST : IStrategy, new()
    {
        private IStrategy strategy = new ST();

        public string Start()
        {
            return $"{strategy.Init()} + {strategy.Do()} + {strategy.End()}";
        }
    }

    public class Strategy : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;

        DynamicStrategyManager dm = new DynamicStrategyManager();
        public StrategyType st = StrategyType.First;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            dm.SetStrategy(st);
            InfoText.text = dm.Start();

            var s1 = new StaticStrategyManager<StrategyOne>();
            var s2 = new StaticStrategyManager<StrategyTwo>();

            Info2Text.text = s1.Start();
            Info3Text.text = s2.Start();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}