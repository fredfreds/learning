using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IoC
{
    public interface IUnit
    {
        string Name { get; }
        int ID { get; }
    }

    public class Unit : IUnit
    {
        public Unit(string name, int id)
        {
            Name = name;
            ID = id;
        }

        public string Name { get; }
        public int ID { get; }
    }

    public interface IUnitWorld
    {
        string Add(IUnit unit);
    }

    public interface IUnitService
    {
        string RegisterNewUnit(IUnit unit);
    }

    public class UnitService : IUnitService
    {
        private readonly IUnitWorld unitWorld;

        public UnitService(IUnitWorld unitWorld) =>
            this.unitWorld = unitWorld;

        public string RegisterNewUnit(IUnit unit) =>
            unitWorld.Add(unit);
    }

    public class UnitWorld : IUnitWorld
    {
        public string Add(IUnit unit)
        {
            return $"Adding {unit.Name} : {unit.ID} to unit world.";
        }
    }

    public class NewWorld : IUnitWorld
    {
        public string Add(IUnit unit)
        {
            return $"Adding {unit.Name} : {unit.ID} to new world.";
        }
    }

    public class IoC : MonoBehaviour
    {
        IUnitService unitService;
        IUnitWorld unitWorld = new UnitWorld();
        IUnitWorld newWorld = new NewWorld();
        IUnit unit;
        public Button RunButton;
        public Text InfoText;
        public Text InfoText2;
        public string Name;
        public int ID;

        void OnEnable()
        {
            RunButton.onClick.AddListener(Run);
        }

        void OnDisable()
        {
            RunButton.onClick.RemoveListener(Run);
        }

        private void Run()
        {
            unit = new Unit(Name, ID);
            unitService = new UnitService(unitWorld);
            InfoText.text = unitService.RegisterNewUnit(unit);
            unitService = new UnitService(newWorld);
            InfoText2.text = unitService.RegisterNewUnit(unit);
        }
    }
}