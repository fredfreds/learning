using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.State
{
    //-----------------------------------------------VARIANT 1---------------------------------
    public enum States
    {
        State1,
        State2,
        State3
    }

    public enum GameStates
    {
        GameState1,
        GameState2,
        GameState3,
        GameState4,
        GameState5
    }

    public class StatePattern
    {
        public Dictionary<States, List<(GameStates, States)>> states
            = new Dictionary<States, List<(GameStates, States)>>
            {
                [States.State1] = new List<(GameStates, States)>
                {
                    (GameStates.GameState2, States.State2),
                    (GameStates.GameState3, States.State3)
                },
                [States.State2] = new List<(GameStates, States)>
                {
                    (GameStates.GameState1, States.State1),
                    (GameStates.GameState3, States.State3)
                },
                [States.State3] = new List<(GameStates, States)>
                {
                    (GameStates.GameState1, States.State1),
                    (GameStates.GameState2, States.State2)
                }
            };
    }

    public enum Control
    {
        S1,
        S2
    }

    //-----------------------------------------------VARIANT 2---------------------------------

    public enum UnitState
    {
        State1,
        State2,
        State3
    }

    public class State : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;

        public Control statesControl = Control.S1;
        public States states = States.State1;
        public GameStates gameStates = GameStates.GameState1;

        public StatePattern sp = new StatePattern();

        public UnitState us = UnitState.State1;
        public int k = 0;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            var (g, s) = sp.states[states][(int)statesControl];
            InfoText.text = states.ToString() + "/" + gameStates.ToString(); ;
            states = s;
            gameStates = g;
            Info2Text.text = states.ToString() + "/" + gameStates.ToString();

            Info3Text.text = "";
            for (int i = 0; i < sp.states[states].Count; i++)
            {
                Info3Text.text += $"{sp.states[states][i].ToString()}\n";
            }

            switch (us)
            {
                case UnitState.State1:
                    if (k == 10)
                    {
                        Info4Text.text = "State1 TO State3";
                        us = UnitState.State3;
                    }
                    else
                    {
                        Info4Text.text = "State1 TO State2";
                        us = UnitState.State2;
                    }
                    break;
                case UnitState.State2:
                    Info4Text.text = "State2 TO State1";
                    us = UnitState.State1;
                    break;
                case UnitState.State3:
                    Info4Text.text = "State3 TO State1";
                    us = UnitState.State1;
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