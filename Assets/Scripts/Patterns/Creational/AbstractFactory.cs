using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Patterns.AbstractFactory
{
    public interface IEnemy
    {
        string Init();
    }

    public interface IShield
    {
        IShield Shield();
        string Cover();
    }

    public interface IEnemyFactory
    {
        IEnemy CreateEnemy();
        IShield CreateShield();
    }

    public class SimpleEnemy : IEnemy
    {
        public string Init()
        {
            return "Simple Enemy initialized!";
        }
    }

    public class ComplexEnemy : IEnemy
    {
        public string Init()
        {
            return "Complex Enemy initialized!";
        }
    }

    public class SimpleShield : IShield
    {
        public IShield Shield()
        {
            return this;
        }

        public string Cover()
        {
            return "Simple Cover";
        }
    }

    public class ComplexShield : IShield
    {
        public IShield Shield()
        {
            return this;
        }

        public string Cover()
        {
            return "Complex Cover";
        }
    }

    public class SimpleEnemyFactory : IEnemyFactory
    {
        public IEnemy CreateEnemy()
        {
            return new SimpleEnemy();
        }

        public IShield CreateShield()
        {
            return new SimpleShield();
        }
    }

    public class ComplexEnemyFactory : IEnemyFactory
    {
        public IEnemy CreateEnemy()
        {
            return new ComplexEnemy();
        }

        public IShield CreateShield()
        {
            return new ComplexShield();
        }
    }

    public enum FactoryType
    {
        Simple,
        Complex
    }

    public class AbstractFactory : MonoBehaviour
    {
        public Button BuildButton;
        public Text EnemyInfoText;
        public Text WeaponInfoText;

        public FactoryType Type = FactoryType.Simple;

        IEnemyFactory factory;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            switch (Type)
            {
                case FactoryType.Simple:
                    factory = new SimpleEnemyFactory();
                    EnemyInfoText.text = factory.CreateEnemy().Init();
                    WeaponInfoText.text = factory.CreateShield().Shield().Cover();
                    break;
                case FactoryType.Complex:
                    factory = new ComplexEnemyFactory();
                    EnemyInfoText.text = factory.CreateEnemy().Init();
                    WeaponInfoText.text = factory.CreateShield().Shield().Cover();
                    break;
                default:
                    break;
            }
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}