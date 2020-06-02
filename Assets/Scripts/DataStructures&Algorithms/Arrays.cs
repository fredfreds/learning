using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DataStructuresAndAlgorithms.Arrays
{
    //Lookup by Index - O(1)
    //Lookup by Value - O(n)
    //Insert - O(n)
    //Delete - O(n)

    public class Array
    {
        private int[] items;
        private int count;
        public Array(int len)
        {
            items = new int[len];
        }

        public void Insert(int item)
        {
            if(items.Length == count)
            {
                int[] newArray = new int[count * 2];

                for (int i = 0; i < count; i++)
                {
                    newArray[i] = items[i];
                }

                items = newArray;
            }

            items[count++] = item;
        }

        public void RemoveAt(int index)
        {
            if(index < 0 || index >= count)
            {

            }

            for (int i = index; i < count; i++)
            {
                items[i] = items[i + 1];
            }

            count--;
        }

        public int IndexOf(int item)
        {
            for (int i = 0; i < count; i++)
            {
                if(item == items[i])
                {
                    return i;
                }
            }

            return -1;
        }

        public string Max()
        {
            int t = 0;

            for (int i = 0; i < items.Length; i++)
            {
                if(items[i] > t)
                {
                    t = items[i];
                }
            }

            return t.ToString();
        }

        public Array Intersect(Array arr)
        {
            Array t = new Array(count);

            for (int i = 0; i < items.Length; i++)
            {
                if(arr.IndexOf(items[i]) >= 0)
                {
                    t.Insert(items[i]);
                }
            }

            return t;
        }

        public void Reverse()
        {
            int[] t = new int[count];

            for (int i = 0; i < count; i++)
            {
                t[i] = items[count - i - 1];
            }

            items = t;
        }

        public void InsertAt(int item, int index)
        {
            if (items.Length == count)
            {
                int[] newArray = new int[count * 2];

                for (int i = 0; i < count; i++)
                {
                    newArray[i] = items[i];
                }

                items = newArray;
            }

            for (int i = count - 1; i >= index; i--)
            {
                items[i + 1] = items[i];
            }

            items[index] = item;
            count++;
        }

        public string Print()
        {
            string t = "";
            for (int i = 0; i < count; i++)
            {
                t += items[i].ToString() + "\n";
            }
            return t;
        }

        public string GetLenth()
        {
            return count.ToString();
        }
    }

    public class Arrays : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;
        public Text Info5Text;
        public Text Info6Text;
        public Text Info7Text;
        public Text Info8Text;

        Array array;
        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            array = new Array(2);
            array.Insert(3);
            InfoText.text = array.Print() + "/" + array.GetLenth();

            array.Insert(4);
            array.Insert(5);
            Info2Text.text = array.Print() + "/" + array.GetLenth();

            array.RemoveAt(3);
            Info3Text.text = array.Print() + "/" + array.GetLenth();

            
            Info4Text.text = array.IndexOf(4).ToString();

            array.Insert(12);
            array.Insert(12);
            Info5Text.text = array.Max();

            Array t1 = new Array(6);
            t1.Insert(12);
            t1.Insert(12);
            t1.Insert(5);
            t1.Insert(2);
            t1.Insert(4);
            t1.Insert(12);

            Info6Text.text = array.Intersect(t1).Print() + "\n" + t1.GetLenth() + "/" + array.GetLenth();

            Info7Text.text = array.Print();
            array.Reverse();
            array.InsertAt(16, 2);
            Info8Text.text = array.Print();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}