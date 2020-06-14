using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DataStructuresAndAlgorithms.LinkedLists
{
    public class Node
    {
        private int value;
        private Node next;

        public Node(int value)
        {
            this.Value = value;
        }

        public Node Next { get => next; set => next = value; }
        public int Value { get => value; set => this.value = value; }
    }

    public class LinkedList
    {
        private Node first;
        private Node last;
        private int size;

        public void AddLast(int item)
        {
            Node node = new Node(item);

            if(IsEmpty())
            {
                first = node;
                last = first;
            }
            else
            {
                last.Next = node;
                last = node;
            }

            size++;
        }

        public void AddFirst(int item)
        {
            Node node = new Node(item);

            if (IsEmpty())
            {
                first = node;
                last = first;
            }
            else
            {
                node.Next = first;
                first = node;
            }

            size++;
        }

        public int IndexOf(int item)
        {
            int index = 0;
            Node current = first;
            while (current != null)
            {
                if (current.Value == item) 
                { 
                    return index; 
                }

                current = current.Next;
                index++;
            }

            return -1;
        }

        public bool Contains(int item)
        {
            return IndexOf(item) != -1;
        }

        public void RemoveFirst()
        {
            if(IsEmpty())
            {
                throw new System.ArgumentNullException();
            }

            if (first == last)
            {
                first = null;
                last = null;
            }
            else
            {
                Node second = first.Next;
                first.Next = null;
                first = second;
            }

            size--;
        }

        public void RemoveLast()
        {
            if(IsEmpty())
            {
                throw new System.ArgumentNullException();
            }

            if (first == last)
            {
                first = null;
                last = null;
            }
            else
            {
                Node previous = GetPrevious(last);
                last = previous;
                last.Next = null;
            }

            size--;
        }

        public int Size()
        {
            if(IsEmpty())
            {
                return 0;
            }

            return size;
        }

        public int[] ToArray()
        {
            int[] array = new int[size];
            Node current = first;
            int index = 0;

            while (current != null)
            {
                array[index++] = current.Value;
                current = current.Next;
            }

            return array;
        }

        public void Reverse()
        {
            if(IsEmpty())
            {
                return;
            }

            Node previous = first;
            Node current = first.Next;

            while (current != null)
            {
                Node next = current.Next;
                current.Next = previous;
                previous = current;
                current = next;
            }

            last = first;
            last.Next = null;

            first = previous;
        }

        public int GetKthFromEnd(int k)
        {
            if(IsEmpty())
            {
                throw new System.ArgumentNullException();
            }

            Node a = first;
            Node b = first;

            for (int i = 0; i < k - 1; i++)
            {
                b = b.Next;
                if(b == null)
                {
                    throw new System.ArgumentNullException();
                }
            }

            while (b != last)
            {
                a = a.Next;
                b = b.Next;
            }

            return a.Value;
        }

        public string GetMiddle()
        {
            if(IsEmpty())
            {
                throw new System.ArgumentNullException();
            }

            Node a = first;
            Node b = first;

            while (b != last && b.Next != last)
            {
                b = b.Next.Next;
                a = a.Next;
            }

            if(b == last)
            {
                return a.Value.ToString();
            }
            else
            {
                return a.Value.ToString() + "/" + a.Next.Value.ToString();
            }
        }

        public bool HasLoop()
        {
            Node a = first;
            Node b = first;

            while (b != null && b.Next != null)
            {
                b = b.Next.Next;
                a = a.Next;

                if (a == b)
                {
                    return true;
                }
            }

            return false;
        }

        private Node GetPrevious(Node node)
        {
            Node current = first;

            while (current != null)
            {
                if (current.Next == node)
                {
                    return current;
                }
                current = current.Next;
            }

            return null;
        }

        private bool IsEmpty()
        {
            return first == null;
        }
    }

    public class LinkedLists : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;

        LinkedList list = new LinkedList();

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            list.AddFirst(10);
            list.AddFirst(20);
            list.AddLast(4);
            list.AddLast(11);
            InfoText.text = Log(list) + "/" + list.Contains(11);

            list.RemoveFirst();
            list.RemoveLast();
            Info2Text.text = Log(list) + "/" + list.IndexOf(4);

            list.Reverse();
            Info3Text.text = Log(list) + "/" + list.Size().ToString();

            list.AddFirst(25);
            list.AddFirst(30);
            Info4Text.text = list.GetKthFromEnd(3).ToString() + "/" + list.GetMiddle() + "/" + list.HasLoop().ToString();
        }

        private string Log(LinkedList l)
        {
            int[] t = l.ToArray();
            string temp = "";
            for (int i = 0; i < t.Length; i++)
            {
                temp += t[i].ToString() + " ";
            }
            return temp;
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}