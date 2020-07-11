using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatternsNew.Tanks
{
    public interface IMovable
    {
        Vector3 GetPosition();
        void SetPosition(Vector3 newPosition);
        Vector3 GetVelocity();
    }

    public interface ICommand
    {
        void Execute();
    }

    public class CommandExeption : System.Exception
    {

    }

    public class Tank : IMovable
    {
        public Vector3 Position;
        public Vector3 Velocity;
        public Vector3 GetPosition()
        {
            return Position;
        }

        public Vector3 GetVelocity()
        {
            return Velocity;
        }

        public void SetPosition(Vector3 newPosition)
        {
            Position = newPosition;
        }
    }

    public class FuelTank : Tank, IRotable
    {
        public float Fuel { get; set; }
        public bool CanMove { get; set; }
        public Vector3 Rotation { get; set; }

        public void SetRotation(Vector3 rotation)
        {
            Rotation = rotation;
        }
    }

    public interface IRotable
    {
        void SetRotation(Vector3 rotation);
        Vector3 Rotation { get; set; }
    }

    public class RotateCommand : ICommand
    {
        IRotable rotable;

        public RotateCommand(IRotable rotable)
        {
            this.rotable = rotable;
        }

        public void Execute()
        {
            try
            {
                rotable.SetRotation(rotable.Rotation);
            }
            catch
            {

                throw new CommandExeption();
            }
        }
    }

    public class MoveCommand : ICommand
    {
        FuelTank movable;
        public MoveCommand(FuelTank movable)
        {
            this.movable = movable;
        }

        public void Execute()
        {
            try
            {
                if(movable.CanMove)
                    movable.SetPosition(movable.GetPosition() + movable.GetVelocity());
            }
            catch 
            {
                throw new CommandExeption();
            }
        }
    }

    public class CheckFuelCommand : ICommand
    {
        FuelTank movable;
        public CheckFuelCommand(FuelTank fuelTank)
        {
            this.movable = fuelTank;
        }

        public void Execute()
        {
            try
            {
                if (movable.Fuel > 0)
                    movable.CanMove = true;
                else
                    movable.CanMove = false;
                    //movable.SetPosition(movable.GetPosition() + movable.GetVelocity());
            }
            catch
            {
                throw new CommandExeption();
            }
        }
    }

    public class Commands : ICommand
    {
        private Queue<ICommand> commands = new Queue<ICommand>();
        public Commands(FuelTank tank)
        {
            commands.Enqueue(new CheckFuelCommand(tank));
            commands.Enqueue(new MoveCommand(tank));
            commands.Enqueue(new RotateCommand(tank));
        }

        public void Execute()
        {
            foreach (var item in commands)
            {
                item.Execute();
            }
        }
    }

    public class Tanks : MonoBehaviour
    {
        FuelTank tank = new FuelTank();
        Commands commands;
        public GameObject Tank;
        public Button button;
        public Vector3 Velocity;
        public Vector3 Rotation;
        public float Fuel;

        private void OnEnable()
        {
            button.onClick.AddListener(Move);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(Move);
        }

        private void Start()
        {
            tank.Position = new Vector3(0, 0, 0);
            Tank.transform.position = tank.Position;
        }

        private void Move()
        {
            tank.Velocity = Velocity;
            tank.Rotation = Rotation;
            tank.Fuel = Fuel;
            commands = new Commands(tank);
            commands.Execute();
            Tank.transform.position = tank.Position;
            Tank.transform.eulerAngles = tank.Rotation;
        }
    }
}