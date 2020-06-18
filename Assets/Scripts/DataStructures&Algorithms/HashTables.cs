using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DataStructuresAndAlgorithms.HashTables
{
    // PROBING 
    // Linear: (hash1 + i) % table_size
    // Quadratic: (hash1 + i^2) % table_size
    // Double Hash: (hash1 + i * hash2) % table_size

    public class HashMap
    {
        private class Entry
        {
            private int key;
            private string value;

            public Entry(int key, string value)
            {
                this.Key = key;
                this.Value = value;
            }

            public int Key { get => key; set => key = value; }
            public string Value { get => value; set => this.value = value; }
        }

        private Entry[] entries = new Entry[5];
        private int count;

        public void Put(int key, string value)
        {
            Entry entry = GetEntry(key);
            if(entry != null)
            {
                entry.Value = value;
                return;
            }

            if(IsFull())
            {
                throw new System.Exception();
            }

            entries[GetIndex(key)] = new Entry(key, value);
            Debug.Log("Done");
            count++;
        }

        public string Get(int key)
        {
            Entry entry = GetEntry(key);
            return entry != null ? entry.Value : null;
        }

        public void Remove(int key)
        {
            int index = GetIndex(key);
            if(index == -1 || entries[index] == null)
            {
                return;
            }

            entries[index] = null;
            count--;
        }

        public int Size()
        {
            return count;
        }

        public override string ToString()
        {
            string t = "";
            for (int i = 0; i < entries.Length; i++)
            {
                t += entries[i].Key + "," + entries[i].Value + "/";
            }
            return t;
        }

        private Entry GetEntry(int key)
        {
            int index = GetIndex(key);
            return index >= 0 ? entries[index] : null;
        }

        private bool IsFull()
        {
            return count == entries.Length;
        }

        private int GetIndex(int key)
        {
            int steps = 0;
            Debug.Log("Enter");
            while(steps < entries.Length)
            {
                int index = Index(key, steps++);
                Debug.Log(index + "/" + key + "/" + steps);
                Entry entry = entries[index];
                if(entry == null || entry.Key == key)
                {
                    return index;
                }
            }

            return -1;
        }

        private int Index(int key, int index)
        {
            return (Hash(key) + index) % entries.Length;
        }

        private int Hash(int key)
        {
            return key % entries.Length;
        }
    }

    public class HashTable
    {
        private class Entry
        {
            private int key;
            private string value;

            public Entry(int key, string value)
            {
                this.Key = key;
                this.Value = value;
            }

            public int Key { get => key; set => key = value; }
            public string Value { get => value; set => this.value = value; }
        }

        private LinkedList<Entry>[] entries = new LinkedList<Entry>[1];
        private int count;

        public void Put(int key, string value)
        {
            int index = Hash(key);
            if (entries[index] == null)
                entries[index] = new LinkedList<Entry>();

            LinkedList<Entry> e = entries[index];
            foreach (var item in e)
            {
                if(item.Key == key)
                {
                    item.Value = value;
                    return;
                }
            }

            count++;
            e.AddLast(new Entry(key, value));
        }

        public string Get(int key)
        {
            int index = Hash(key);
            LinkedList<Entry> e = entries[index];
            if (e != null)
            {
                foreach (var item in e)
                {
                    if (item.Key == key)
                    {
                        return item.Value;
                    }
                }
            }

            return null;
        }

        public int Size()
        {
            return count;
        }

        private int Hash(int key)
        {
            return key % entries.Length;
        }
    }

    public class CharFinder
    {
        public char FindFirst(string s)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();
            char[] c = s.ToCharArray();

            for (int i = 0; i < c.Length; i++)
            {
                if(map.ContainsKey(c[i]))
                {
                    int val = map[c[i]];
                    map[c[i]] = val + 1;
                }
                else
                {
                    map.Add(c[i], 1);
                }
            }

            for (int i = 0; i < c.Length; i++)
            {
                if (map[c[i]] == 1)
                    return c[i];
            }

            return char.MinValue;
        }

        public char FindRepeat(string s)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();
            char[] c = s.ToCharArray();

            for (int i = 0; i < c.Length; i++)
            {
                if (map.ContainsKey(c[i]))
                {
                    int val = map[c[i]];
                    map[c[i]] = val + 1;
                }
                else
                {
                    map.Add(c[i], 1);
                }
            }

            for (int i = 0; i < c.Length; i++)
            {
                if (map[c[i]] > 1)
                    return c[i];
            }

            return char.MinValue;
        }

        public char MostFrequent(string s)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();
            char[] c = s.ToCharArray();

            for (int i = 0; i < c.Length; i++)
            {
                if (map.ContainsKey(c[i]))
                {
                    int val = map[c[i]];
                    map[c[i]] = val + 1;
                }
                else
                {
                    map.Add(c[i], 1);
                }
            }

            int f = -1;
            char r = s[0];

            for (int i = 0; i < c.Length; i++)
            {
                if (map[c[i]] > f)
                {
                    f = map[c[i]];
                    r = c[i];
                }
            }

            return r;
        }

        public int[] Sum(int[] nums, int t)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                int c = t - nums[i];
                if(map.ContainsKey(c))
                {
                    return new int[] { map[c], i };
                }
                map.Add(nums[i], i);
            }

            return null;
        }

        public int Differenct(int[] arr, int diff)
        {
            int count = 0;
            HashSet<int> keys = new HashSet<int>();
            for (int i = 0; i < arr.Length; i++)
            {
                keys.Add(arr[i]);
            }

            for (int i = 0; i < arr.Length; i++)
            {
                if(keys.Contains(arr[i] + diff))
                    count++;
                if (keys.Contains(arr[i] - diff))
                    count++;
                keys.Remove(arr[i]);
            }
            return count;
        }
    }
    public class HashTables : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;

        public string Test;
        public CharFinder cf = new CharFinder();
        HashTable ht = new HashTable();
        HashMap hm = new HashMap();

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            InfoText.text = cf.FindFirst(Test).ToString() + cf.Differenct(new int[] { 1, 5, 9, 7, 4, 12, 3 }, 2);
            int[] arr = cf.Sum(new int[] { 1, 5, 9, 7, 4, 12, 3 }, 9);
            Info2Text.text = cf.FindRepeat(Test).ToString() + "/" + cf.MostFrequent(Test).ToString() + "/" + arr[0] + "," + arr[1];

            ht.Put(6, "A");
            ht.Put(8, "B");
            ht.Put(11, "C");
            ht.Put(6, "A+");
            Info3Text.text = ht.Get(6) + "/" + ht.Size();

            hm.Put(1, "4");
            hm.Put(4, "6");
            hm.Put(11, "45");
            hm.Put(21, "54");
            hm.Put(31, "64");
            Info4Text.text = hm.ToString();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}