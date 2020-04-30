using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Patterns.Builder
{
    public interface IBuilder
    {
        void Reset();
        void Create();
        void Move();
        void AddShield();
        void Gather();
    }

    public class Manager
    {
        private IBuilder builder;

        public Manager(IBuilder builder)
        {
            this.builder = builder;
        }

        public void Init(bool withWeapon)
        {
            builder.Reset();
            builder.Create();
            builder.Move();

            if(withWeapon) builder.AddShield();

            builder.Gather();
        }
    }

    public class HouseBuilder : IBuilder
    {
        private House house;

        public void AddShield()
        {
            house.InitService(true);
        }

        public void Create()
        {
            house.InitPlace(true);
        }

        public void Gather()
        {
            house.InitFinish(true);
        }

        public void Move()
        {
            house.InitBuilding(true);
        }

        public void Reset()
        {
            house = new House();
        }

        public House Result()
        {
            return house;
        }
    }

    public class ShipBuilder : IBuilder
    {
        private Ship ship;

        public void AddShield()
        {
            ship.InitService(true);
        }

        public void Create()
        {
            ship.InitPlace(true);
        }

        public void Gather()
        {
            ship.InitFinish(true);
        }

        public void Move()
        {
            ship.InitBuilding(true);
        }

        public void Reset()
        {
            ship = new Ship();
        }

        public Ship Result()
        {
            return ship;
        }
    }

    public class PriceBuilder : IBuilder
    {
        private int total;

        public void AddShield()
        {
            total += 5;
        }

        public void Create()
        {
            total += 10;
        }

        public void Gather()
        {
            total += 15;
        }

        public void Move()
        {
            total += 20;
        }

        public void Reset()
        {
            total = 0;
        }

        public int Result()
        {
            return total;
        }
    }

    public class House
    {
        private bool place;
        private bool building;
        private bool service;
        private bool finish;

        public bool IsPlace()
        {
            return place;
        }

        public void InitPlace(bool place)
        {
            this.place = place;
        }

        public bool IsBuilding()
        {
            return building;
        }

        public void InitBuilding(bool building)
        {
            this.building = building;
        }

        public bool IsService()
        {
            return service;
        }

        public void InitService(bool service)
        {
            this.service = service;
        }
        public bool IsFinish()
        {
            return finish;
        }

        public void InitFinish(bool finish)
        {
            this.finish = finish;
        }

        public string Convert(bool b)
        {
            return b ? "Yes" : "No";
        }

        public override string ToString()
        {
            return $"Place: {Convert(IsPlace())} + " +
                $"Building: {Convert(IsBuilding())} + " +
                $"Service: {Convert(IsService())} + " +
                $"Finish: {Convert(IsFinish())}";
        }
    }

    public class Ship
    {
        private bool place;
        private bool building;
        private bool service;
        private bool finish;

        public bool IsPlace()
        {
            return place;
        }

        public void InitPlace(bool place)
        {
            this.place = place;
        }

        public bool IsBuilding()
        {
            return building;
        }

        public void InitBuilding(bool building)
        {
            this.building = building;
        }

        public bool IsService()
        {
            return service;
        }

        public void InitService(bool service)
        {
            this.service = service;
        }
        public bool IsFinish()
        {
            return finish;
        }

        public void InitFinish(bool finish)
        {
            this.finish = finish;
        }

        public string Convert(bool b)
        {
            return b ? "Yes" : "No";
        }

        public override string ToString()
        {
            return $"Ship Place: {Convert(IsPlace())} + " +
                $"Ship Building: {Convert(IsBuilding())} + " +
                $"Ship Service: {Convert(IsService())} + " +
                $"Ship Finish: {Convert(IsFinish())}";
        }
    }

    public class Builder : MonoBehaviour
    {
        public Button BuildButton;
        public Text PriceInfoText;
        public Text HouseInfoText;
        public Text ShipInfoText;

        HouseBuilder houseBuilder = new HouseBuilder();
        ShipBuilder shipBuilder = new ShipBuilder();
        PriceBuilder priceBuilder = new PriceBuilder();

        Manager houseManager;
        Manager shipManager;
        Manager priceManager;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
            houseManager = new Manager(houseBuilder);
            shipManager = new Manager(shipBuilder);
            priceManager = new Manager(priceBuilder);
        }

        private void Build()
        {
            priceManager.Init(true);
            PriceInfoText.text = priceBuilder.Result().ToString();
            houseManager.Init(true);
            HouseInfoText.text = houseBuilder.Result().ToString();
            shipManager.Init(true);
            ShipInfoText.text = shipBuilder.Result().ToString();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}