using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.NullObject
{
    public interface ILog
    {
        string Information(string m);
        string Warning(string m);
    }

    public class GameLog : ILog
    {
        public string Information(string m)
        {
            return m;
        }

        public string Warning(string m)
        {
            return $"Warning: {m}";
        }
    }

    public class NullLog : ILog
    {
        public string Information(string m)
        {
            return default;
        }

        public string Warning(string m)
        {
            return default;
        }
    }

    public class Unit
    {
        private int health;
        private ILog log;

        public Unit(ILog l)
        {
            this.log = l;
        }

        public string AddHealth(int h)
        {
            health += h;
            return log.Information($"Added {health}");
        }
    }

    public class NullObject : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;

        ILog log = new GameLog();
        ILog log2 = new NullLog();
        Unit u;
        Unit u2;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            u = new Unit(log);
            InfoText.text = u.AddHealth(10);

            u2 = new Unit(log2);
            Info2Text.text = u2.AddHealth(10);
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}