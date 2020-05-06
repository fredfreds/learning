using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Command
{
    public class Unit
    {
        private int health;

        public string Increase(int a)
        {
            health += a;
            return $"{health}";
        }

        public bool Decrease(int a)
        {
            if (health - a >= 0)
            {
                health -= a;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"{health}";
        }
    }

    public interface ICommand
    {
        void Call();
        void Undo();
    }

    public class UnitCommand : ICommand
    {
        private Unit unit;
        public enum Action
        {
            Inc,
            Dec
        }

        private Action action;
        private int amount;
        private bool succeeded;

        public UnitCommand(Unit u, Action a, int m)
        {
            this.unit = u;
            this.action = a;
            this.amount = m;
        }

        public void Call()
        {
            switch (action)
            {
                case Action.Inc:
                    unit.Increase(amount);
                    succeeded = true;
                    break;
                case Action.Dec:
                    succeeded = unit.Decrease(amount);
                    break;
                default:
                    break;
            }
        }

        public void Undo()
        {
            if (!succeeded) return;

            switch (action)
            {
                case Action.Inc:
                    unit.Decrease(amount);
                    break;
                case Action.Dec:
                    unit.Increase(amount);
                    break;
                default:
                    break;
            }
        }
    }

    public class Command : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;
        public Text Info5Text;

        Unit u = new Unit();

        public int Inc;
        public int Dec;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            InfoText.text = u.ToString();

            List<UnitCommand> commands = new List<UnitCommand>
            {
                new UnitCommand(u, UnitCommand.Action.Inc, Inc),
                new UnitCommand(u, UnitCommand.Action.Dec, Dec)
            };

            commands[0].Call();
            Info2Text.text = u.ToString();

            commands[1].Call();
            Info3Text.text = u.ToString();

            commands[1].Undo();
            Info4Text.text = u.ToString();

            commands[0].Undo();
            Info5Text.text = u.ToString();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}