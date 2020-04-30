using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace SOLID
{
    public class Database
    {
        private readonly List<string> users = new List<string>();

        private int count = 0;

        public int AddUser(string text)
        {
            users.Add($"{++count}: {text}");
            return count;
        }

        public void RemoveEntry(int index)
        {
            users.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, users);
        }
    }

    public class Persistence
    {
        public void SaveToFile(Database database, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
            {
                File.WriteAllText(filename, database.ToString());
            }
        }
    }

    public class Loader
    {
        private string database;
        public void LoadDatabase(string filename)
        {
            database = File.ReadAllText(filename);
        }

        public string ReadDatabase()
        {
            return database;
        }
    }

    public class SingleResponsibilityPrinciple : MonoBehaviour
    {
        Persistence persistence = new Persistence();
        Loader loader = new Loader();
        Database database = new Database();

        string filename = @"C:\database.txt";

        public InputField NameInputField;
        public InputField IndexInputField;

        public Text InfoText;

        public Button AddUserButton;
        public Button SaveButton;
        public Button LoadButton;
        public Button RemoveButton;

        private void OnEnable()
        {
            AddUserButton.onClick.AddListener(AddUser);
            RemoveButton.onClick.AddListener(RemoveUser);
            SaveButton.onClick.AddListener(Save);
            LoadButton.onClick.AddListener(Load);
        }

        private void AddUser()
        {
            database.AddUser(NameInputField.text);
        }

        private void RemoveUser()
        {
            database.RemoveEntry(int.Parse(IndexInputField.text));
        }

        private void Save()
        {
            persistence.SaveToFile(database, filename, true);
        }

        private void Load()
        {
            loader.LoadDatabase(filename);
            InfoText.text = loader.ReadDatabase();
        }

        private void OnDisable()
        {
            AddUserButton.onClick.RemoveListener(AddUser);
            RemoveButton.onClick.RemoveListener(RemoveUser);
            SaveButton.onClick.RemoveListener(Save);
            LoadButton.onClick.RemoveListener(Load);
        }
    }
}