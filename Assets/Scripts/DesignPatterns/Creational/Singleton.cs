using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Singleton
{
    public interface IDatabase
    {
        string GetName();
    }
    public class Database : IDatabase
    {
        private string Name;

        private static int count;

        private Database()
        {
            count++;
            Name = "Fred";
        }

        public string GetName()
        {
            return Name;
        }

        public static int Count()
        {
            return count;
        }

        private static Lazy<Database> instance = new Lazy<Database>(() => new Database());
        public static Database Instance => instance.Value;
    }

    public class Singleton : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            IDatabase db = Database.Instance;
            InfoText.text = db.GetName();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}