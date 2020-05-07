using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Mediator
{
    public class Unit
    {
        public string Name;
        public UnitRoom Room;
        private List<string> roomLog = new List<string>();

        public Unit(string n)
        {
            Name = n;
        }

        public string Say(string m)
        {
            return Room.Broadcast(Name, m);
        }

        public string PrivatMessage(string w, string m)
        {
            return Room.Message(Name, w, m);
        }

        public string Receive(string s, string m)
        {
            string sm = $"{s} : {m}";
            roomLog.Add(sm);
            return sm;
        }
    }

    public class UnitRoom
    {
        private List<Unit> units = new List<Unit>();

        public string Join(Unit u)
        {
            string j = $"{u.Name} join";
            string js = $"{j} : {Broadcast("room", j)}";

            u.Room = this;
            units.Add(u);

            return js;
        }

        public string Broadcast(string s, string j)
        {
            string js = "";

            foreach (var item in units)
            {
                if(item.Name != s)
                {
                    js = item.Receive(s, j);
                }
            }

            return js;
        }

        public string Message(string s, string d, string m)
        {
            string ms = units.FirstOrDefault(p => p.Name == d)
                ?.Receive(s, m);
            return $"{d} : {ms}";
        }
    }

    public class Human
    {
        private HumanMediator hm;
        public string Message { get; set; }

        public Human(HumanMediator hm)
        {
            this.hm = hm;
            hm.Alert += Mediator_Alert;
        }

        public void Mediator_Alert(object o, string s)
        {
            if(o != this)
            {
                Message += s;
            }
        }

        public string Say(string s)
        {
            return hm.Broadcast(this, s);
        }
    }

    public class HumanMediator
    {
        public string Broadcast(object o, string s)
        {
            Alert?.Invoke(o, s);
            return s;
        }

        public event EventHandler<string> Alert;
    }

    public class Mediator : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;
        public Text Info5Text;
        public Text Info6Text;
        public Text Info7Text;

        UnitRoom ur = new UnitRoom();
        Unit u1 = new Unit("Fred");
        Unit u2 = new Unit("Freya");
        Unit u3;
        public string nu;
        public string n;

        HumanMediator hm = new HumanMediator();
        Human h1;
        Human h2;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            InfoText.text = ur.Join(u1);
            Info2Text.text = ur.Join(u2);
            Info3Text.text = u1.Say("u1 say");
            Info4Text.text = u2.Say("u2 say");
            u3 = new Unit(nu);
            Info5Text.text = ur.Join(u3);
            Info6Text.text = u3.Say("u3 say");
            Info7Text.text = u2.PrivatMessage(n, "u3 to u2");

            h1 = new Human(hm);
            h2 = new Human(hm);

            Debug.Log(h1.Message);
            Debug.Log(h2.Message);

            h1.Say("hello h1");

            Debug.Log(h1.Message);
            Debug.Log(h2.Message);

            h2.Say("hello h2");

            Debug.Log(h1.Message);
            Debug.Log(h2.Message);
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}