using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace DataStructuresAndAlgorithms.Stacks
{
    // Push - O(1)
    // Pop - O(1)
    // Peek - O(1)

    public class StackCustom
    {
        private int[] items = new int[5];
        private int count;

        public void Push(int item)
        {
            if (count == items.Length)
                throw new System.StackOverflowException();

            items[count++] = item;
        }

        public int Size()
        {
            return count;
        }

        public int Pop()
        {
            if (count == 0)
                throw new System.IndexOutOfRangeException();

            return items[--count];
        }

        public int Peek()
        {
            if (count == 0)
                throw new System.IndexOutOfRangeException();

            return items[count - 1];
        }

        public bool IsEmpty()
        {
            return count == 0;
        }

        public override string ToString()
        {
            string t = "";
            for (int i = 0; i < count; i++)
            {
                t += items[i].ToString() + ",";
            }
            return t;
        }
    }

    public class TwoStacks
    {
        private int st1count;
        private int st2count;
        private int[] items;

        public TwoStacks(int capacity)
        {
            if(capacity <= 0)
            {
                throw new System.Exception("Capacity must be greater than 1");
            }

            items = new int[capacity];
            st1count = -1;
            st2count = capacity;
        }

        public void Push1(int item)
        {
            if (IsFull1())
                throw new System.OverflowException();

            items[++st1count] = item;
        }

        public int Pop1()
        {
            if (IsEmpty1())
                throw new System.IndexOutOfRangeException();

            return items[st1count--];
        }

        public bool IsEmpty1()
        {
            return st1count == -1;
        }

        public bool IsFull1()
        {
            return st1count + 1 == st2count;
        }

        public void Push2(int item)
        {
            if (IsFull1())
                throw new System.OverflowException();

            items[--st2count] = item;
        }

        public int Pop2()
        {
            if (IsEmpty2())
                throw new System.IndexOutOfRangeException();

            return items[st2count++];
        }

        public bool IsEmpty2()
        {
            return st2count == items.Length;
        }

        public bool IsFull2()
        {
            return st2count - 1 == st1count;
        }

        public override string ToString()
        {
            string t = "";
            for (int i = 0; i < items.Length; i++)
            {
                t += items[i].ToString() + ",";
            }
            return t;
        }
    }

    public class MinStack
    {
        private StackCustom sc1 = new StackCustom();
        private StackCustom minStack = new StackCustom();

        public void Push(int item)
        {
            sc1.Push(item);

            if (minStack.IsEmpty())
                minStack.Push(item);
            else if (item < minStack.Peek())
                minStack.Push(item);
        }

        public int Pop()
        {
            if (sc1.IsEmpty())
                throw new System.IndexOutOfRangeException();

            int t = sc1.Pop();

            if (minStack.Peek() == t)
                minStack.Pop();

            return t;
        }

        public int Size()
        {
            return minStack.Size();
        }

        public int Min()
        {
            return minStack.Peek();
        }
    }

    public class Expression
    {
        public bool IsBalanced(string input, char op, char cl)
        {
            char[] ar = input.ToCharArray();
            Stack<char> c = new Stack<char>();

            for (int i = 0; i < ar.Length; i++)
            {
                if (ar[i] == op)
                    c.Push(ar[i]);
                if (ar[i] == cl)
                    c.Pop();
            }
            return c.Count > 0;
        }
    }

    public class StackTest
    {
        public string Test(string t)
        {
            if (t == null)
                throw new System.ArgumentNullException();

            Stack<char> s = new Stack<char>();
            char[] c = t.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                s.Push(c[i]);
            }
            StringBuilder r = new StringBuilder();
            while (s.Count > 0)
            {
                r.Append(s.Pop());
            }
            return r.ToString();
        }
    }

    public class Stacks : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;
        public Text Info5Text;
        public Text Info6Text;

        StackTest st = new StackTest();
        public string Input = "Test";
        Expression ex = new Expression();
        StackCustom sc = new StackCustom();
        TwoStacks ts = new TwoStacks(4);
        MinStack ms = new MinStack();

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            InfoText.text = st.Test(Input);

            bool b = !ex.IsBalanced(Input, '(', ')');
            Info2Text.text = b.ToString();

            sc.Push(1);
            sc.Push(10);
            sc.Push(100);
            sc.Push(210);

            Info3Text.text = sc.ToString();

            sc.Pop();
            sc.Pop();

            Info4Text.text = sc.Peek() + "/" + sc.IsEmpty() + "/" + sc.Pop() + "/" + sc.ToString();

            ts.Push1(10);
            ts.Push1(20);
            ts.Push2(30);
            ts.Push2(40);

            Info5Text.text = ts.ToString();

            ms.Push(10);
            ms.Push(20);
            ms.Push(40);
            ms.Push(3);

            Info6Text.text = ms.Min().ToString() + "/" + ms.Size() + "/" + ms.Pop() + "/" + ms.Min() + "/" + ms.Size();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}