using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Collections
{

    public enum ChangeType { Append, Insert, Remove }

    public class ItineraryChange
    {
        public ItineraryChange(ChangeType changeType, Country value, int index)
        {
            ChangeType = changeType;
            Value = value;
            Index = index;
        }

        public ChangeType ChangeType { get; }
        public Country Value { get; }
        public int Index { get; }

    }

    public class CountryCode
    {
        public CountryCode(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override string ToString() => Value;

        public override bool Equals(object obj)
        {
            if (obj is CountryCode other)
                return StringComparer.OrdinalIgnoreCase.Equals(this.Value, other.Value);
            return false;
        }

        public static bool operator ==(CountryCode lhs, CountryCode rhs)
        {
            if (lhs != null)
                return lhs.Equals(rhs);
            else
                return rhs == null;
        }

        public static bool operator !=(CountryCode lhs, CountryCode rhs)
        {
            return !(lhs == rhs);
        }

        public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(this.Value);
    }
    public class Country
    {
        public Country(string name, string code, string region, int population)
        {
            Name = name;
            Code = new CountryCode(code);
            Region = region;
            Population = population;
        }

        public string Name { get; }
        public CountryCode Code { get; }
        public string Region { get; }
        public int Population { get; }

        public override string ToString() => $"{Name} ({Code})";
    }

    public class AppData
    {
        public List<Country> AllCountries { get; private set; }
        //public Dictionary<string, Country> AllCountriesByKey { get; private set; }
        //public SortedDictionary<string, Country> AllCountriesByKey { get; private set; }
        public Dictionary<CountryCode, Country> AllCountriesByKey { get; private set; }

        public LinkedList<Country> ItineraryBuilder { get; } = new LinkedList<Country>();

        public Stack<ItineraryChange> ChangeLog { get; } = new Stack<ItineraryChange>();

        public void Initialize(string csvPath)
        {
            CsvReader reader = new CsvReader(csvPath);
            AllCountries = reader.ReadAllCountries().OrderBy(x => x.Name).ToList();
            //AllCountriesByKey = AllCountries.ToDictionary(x => x.Code, StringComparer.OrdinalIgnoreCase);
            var dict = AllCountries.ToDictionary(x => x.Code);
            //AllCountriesByKey = new SortedDictionary<string, Country>(dict);
            AllCountriesByKey = dict;
        }
    }

    public static class LinkedListExtension
    {
        public static LinkedListNode<T> GetNth<T>(this LinkedList<T> lst, int n)
        {
            LinkedListNode<T> curr = lst.First;
            for (int i = 0; i < n; i++)
            {
                curr = curr.Next;
            }
            return curr;
        }
    }

    public class CsvReader
    {
        private string csvPath;

        public CsvReader(string csvPath)
        {
            this.csvPath = csvPath;
        }

        public List<Country> ReadAllCountries()
        {
            List<Country> countries = new List<Country>();

            using (StreamReader sr = new StreamReader(csvPath))
            {
                sr.ReadLine();

                string csvLine;
                while ((csvLine = sr.ReadLine()) != null)
                {
                    countries.Add(ReadCountryFromCsvLine(csvLine));
                }
            }

            return countries;
        }

        public Country ReadCountryFromCsvLine(string csvLine)
        {
            string[] parts = csvLine.Split(',');
            string name;
            string code;
            string region;
            string popText;
            switch (parts.Length)
            {
                case 4:
                    name = parts[0];
                    code = parts[1];
                    region = parts[2];
                    popText = parts[3];
                    break;
                case 5:
                    name = parts[0] + ", " + parts[1];
                    name = name.Replace("\"", null).Trim();
                    code = parts[2];
                    region = parts[3];
                    popText = parts[4];
                    break;
                default:
                    throw new Exception($"Can't parse country from csvLine: {csvLine}");
            }

            int.TryParse(popText, out int population);
            return new Country(name, code, region, population);
        }
    }

    public class CountryComparer : IComparer<Country>
    {
        public static CountryComparer Instance { get; } = new CountryComparer();
        private CountryComparer() { }

        public int Compare(Country x, Country y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

    public class Collections : MonoBehaviour
    {
        public string CsvPath;
        public InputField IF;
        public Text T1;
        public Text T2;

        public Button Add;
        public Button Remove;
        public Button Insert;
        public Button Undo;
        public int ID;
        public int id2 = 0;
        int id3 = 0;
        public GameObject UnityCountry;
        public Transform Parent;
        public Transform Parent2;
        public List<GameObject> Items = new List<GameObject>();

        private AppData appData = new AppData();

        void OnEnable()
        {
            IF.onValueChanged.AddListener(GetCountry);
            Add.onClick.AddListener(AddItem);
            Remove.onClick.AddListener(RemoveItem);
            Insert.onClick.AddListener(InsertItem);
            Undo.onClick.AddListener(UndoItem);
        }

        void OnDisable()
        {
            IF.onValueChanged.RemoveListener(GetCountry);
            Add.onClick.RemoveListener(AddItem);
            Remove.onClick.RemoveListener(RemoveItem);
            Insert.onClick.RemoveListener(InsertItem);
            Undo.onClick.RemoveListener(UndoItem);
        }

        void UndoItem()
        {
            if (appData.ChangeLog.Count == 0)
                return;

            ItineraryChange lc = appData.ChangeLog.Pop();
            switch (lc.ChangeType)
            {
                case ChangeType.Append:
                    appData.ItineraryBuilder.RemoveLast();
                    Destroy(Items[Items.Count-1]);
                    Items.Remove(Items[Items.Count - 1]);
                    for (int i = 1; i < Items.Count; i++)
                    {
                        Items[i].GetComponent<UnityCountry>().ID--;
                    }
                    id3--;
                    break;
                case ChangeType.Insert:
                    break;
                case ChangeType.Remove:
                    break;
                default:
                    break;
            }

            foreach (var item in appData.ChangeLog)
            {
                Debug.Log(item.Value);
            }
        }

        void InsertItem()
        {
            GameObject obj = Instantiate(UnityCountry, Parent2);
            Items.Add(obj);
            Country country = appData.AllCountries[ID];
            var nodeToRemove = appData.ItineraryBuilder.GetNth(id2);
            int si = Items[id2].gameObject.transform.GetSiblingIndex();
            obj.transform.SetSiblingIndex(si--);
            appData.ItineraryBuilder.AddBefore(nodeToRemove, country);
            UnityCountry uc = obj.GetComponent<UnityCountry>();
            uc.T1.text = country.Name;
            uc.T2.text = country.Code.Value;
            uc.ID = id3;
            id3++;
            uc.UnitEvent += Call2;

            foreach (var item in appData.ItineraryBuilder)
            {
                Debug.Log(item.Name);
            }
        }

        SortedSet<Country> c = new SortedSet<Country>(CountryComparer.Instance);

        void AddItem()
        {
            GameObject obj = Instantiate(UnityCountry, Parent2);
            Items.Add(obj);
            Country country = appData.AllCountries[ID];
            UnityCountry uc = obj.GetComponent<UnityCountry>();
            uc.T1.text = country.Name;
            uc.T2.text = country.Code.Value;
            appData.ItineraryBuilder.AddLast(country);
            uc.ID = id3;
            id3++;
            uc.UnitEvent += Call2;
            var change = new ItineraryChange(ChangeType.Append, country, appData.ItineraryBuilder.Count);
            appData.ChangeLog.Push(change);
            c.Add(country);

            foreach (var item in c)
            {
                Debug.Log(item.Name);
            }
        }

        void RemoveItem()
        {
            var nodeToRemove = appData.ItineraryBuilder.GetNth(id2);
            appData.ItineraryBuilder.Remove(nodeToRemove);
            Destroy(Items[id2]);
            Items.Remove(Items[id2]);
            for (int i = 1; i < Items.Count; i++)
            {
                Items[i].GetComponent<UnityCountry>().ID--;
            }
            id3--;

            var change = new ItineraryChange(ChangeType.Remove, nodeToRemove.Value, id2);
            appData.ChangeLog.Push(change);
        }

        void GetCountry(string code)
        {
            if (code.Length != 3)
            {
                return;
            }

            // O(n)
            //Country country = appData.AllCountries.Find(x => x.Code == code); 
            // O(1)
            appData.AllCountriesByKey.TryGetValue(new CountryCode(code), out Country result);
            Country country = result;

            if (country != null)
            {
                T1.text = country.Name;
                T2.text = country.Code.Value;
            }
        }

        void Start()
        {
            int id = 0;
            appData.Initialize(Application.dataPath + CsvPath);
            foreach(var item in appData.AllCountriesByKey)
            {
                GameObject obj = Instantiate(UnityCountry, Parent);
                UnityCountry uc = obj.GetComponent<UnityCountry>();
                uc.T1.text = item.Value.Name;
                uc.T2.text = item.Value.Code.Value;
                uc.ID = id;
                uc.UnitEvent += Call;
                id++;
            }
        }
        private void Call(object s, UnitChangeArgs u)
        {
            ID = u.ID;
        }

        private void Call2(object s, UnitChangeArgs u)
        {
            id2 = u.ID;
        }
    }
}