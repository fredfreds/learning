using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.AbstractFactory
{
    public interface IUnit
    {
        string Name();
    }

    public class Enemy : IUnit
    {
        public string Name()
        {
            return "Enemy";
        }
    }

    public class Car : IUnit
    {
        public string Name()
        {
            return "Car";
        }
    }

    public class Hero : IUnit
    {
        public string Name()
        {
            return "Hero";
        }
    }

    public interface IUnitFactory
    {
        IUnit Create();
    }

    public class EnemyFactory : IUnitFactory
    {
        public IUnit Create()
        {
            return new Enemy();
        }
    }

    public class HeroFactory : IUnitFactory
    {
        public IUnit Create()
        {
            return new Hero();
        }
    }

    public class CarFactory : IUnitFactory
    {
        public IUnit Create()
        {
            return new Car();
        }
    }

    public class World
    {
        private List<Tuple<string, IUnitFactory>> factories =
            new List<Tuple<string, IUnitFactory>>();

        public World()
        {
            foreach (var t in typeof(World).Assembly.GetTypes())
            {
                if(typeof(IUnitFactory).IsAssignableFrom(t) &&
                    !t.IsInterface)
                {
                    factories.Add(Tuple.Create(
                        t.Name.Replace("Factory", string.Empty),
                        (IUnitFactory)Activator.CreateInstance(t)
                        )); 
                }
            }
        }

        public string GetUnitTypes()
        {
            string s = "";
            for (int i = 0; i < factories.Count; i++)
            {
                Tuple<string, IUnitFactory> tuple = factories[i];
                s += $"{i} : {tuple.Item1}\n";
            }
            return s;
        }

        public IUnit Create(int i)
        {
            return factories[i].Item2.Create();
        }
    }

    public class AbstractFactory : MonoBehaviour
    {
        public Button BuildButton;
        public Button BuildButton2;

        public Text InfoText;
        public Text InfoText2;

        public int Index;

        World w = new World();
        IUnit u;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
            BuildButton2.onClick.AddListener(GetTypes);
        }

        private void GetTypes()
        {
            InfoText.text = w.GetUnitTypes();
        }

        private void Build()
        {
            u = w.Create(Index);
            InfoText2.text = u.Name();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
            BuildButton2.onClick.RemoveListener(GetTypes);
        }
    }
}