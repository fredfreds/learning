using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Prototype
{
    public static class ExtensionMethods
    {
        public static T Copy<T>(this T self)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, self);
            ms.Seek(0, SeekOrigin.Begin);
            object copy = bf.Deserialize(ms);
            ms.Close();
            return (T)copy;
        }

        public static T CopyXML<T>(this T self)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                xs.Serialize(ms, self);
                ms.Position = 0;
                return (T)xs.Deserialize(ms);
            }
        }
    }

    public interface IClone<T>
    {
        T Clone();
    }

    [Serializable]
    public class Unit : IClone<Unit>
    {
        public string Name;
        public Stats Stats;

        public Unit() { }

        public Unit(string n, Stats s)
        {
            Name = n;
            Stats = s;    
        }

        public Unit(Unit u)
        {
            Name = u.Name;
            Stats = new Stats(u.Stats);
            //Stats = u.Stats;
        }

        public Unit Clone()
        {
            //return new Unit(Name, Stats);
            return new Unit(Name, Stats.Clone());
        }
    }

    [Serializable]
    public class Stats : IClone<Stats>
    {
        public int Health;

        public Stats() { }

        public Stats(int h)
        {
            Health = h;
        }

        public Stats(Stats s)
        {
            Health = s.Health;
        }

        public Stats Clone()
        {
            return new Stats(Health);
        }
    }

    public class Prototype : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;
        public Text Info5Text;
        public Text Info6Text;
        public Text Info7Text;

        Unit u1;
        Unit u2;
        Unit u3;
        Unit u4;
        Unit u5;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            u1 = new Unit("Fred", new Stats(10));
            InfoText.text = $"{u1.Name} : {u1.Stats.Health}";
            u2 = new Unit(u1);
            Info2Text.text = $"Copy u1 = {u2.Name} : {u2.Stats.Health}";
            u2.Name = "Fill";
            u2.Stats.Health = 3;
            Info3Text.text = $"{u2.Name} : {u2.Stats.Health}";
            u1 = new Unit(u2);
            Info4Text.text = $"Copy u2 = {u1.Name} : {u1.Stats.Health}";
            u3 = u1.Clone();
            Info5Text.text = $"Copy u1 = {u3.Name} : {u3.Stats.Health}";
            u4 = u2.Copy();
            Info6Text.text = $"Copy u2 = {u4.Name} : {u4.Stats.Health}";
            u1.Name = "Fred";
            u1.Stats.Health = 4;
            u5 = u1.CopyXML();
            Info7Text.text = $"Copy u1 = {u5.Name} : {u5.Stats.Health}";
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}