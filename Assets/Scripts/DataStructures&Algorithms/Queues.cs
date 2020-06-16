using DataStructuresAndAlgorithms.Stacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DataStructuresAndAlgorithms.Queues
{
    public class QueueTest
    {
        public string Test(Queue<int> ints)
        {
            string t = "";
            Stack<int> temp = new Stack<int>();

            while (ints.Count > 0)
            {
                temp.Push(ints.Dequeue());
            }
            while (temp.Count > 0)
            {
                ints.Enqueue(temp.Pop());
            }

            int[] ar = ints.ToArray();

            for (int i = 0; i < ar.Length; i++)
            {
                t += ar[i] + ",";
            }

            return t;
        }
    }

    public class QueueCustom
    {
        private int[] items = new int[5];
        private int rear;
        private int front;
        private int count;
        public QueueCustom(int cap)
        {
            items = new int[cap];
        }

        public void Enqueue(int item)
        {
            if (count == items.Length)
                throw new System.IndexOutOfRangeException();

            items[rear] = item;
            rear = (rear + 1) % items.Length;
            count++;
        }

        public int Dequeue()
        {
            int item = items[front];
            items[front] = 0;
            front = (front + 1) % items.Length;
            count--;
            return item;
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

    public class QueueStack
    {
        private StackCustom sc1 = new StackCustom();
        private StackCustom sc2 = new StackCustom();

        public void Enqueue(int item)
        {
            sc1.Push(item);
        }

        public int Dequeue()
        {
            if (IsEmpty())
                throw new System.IndexOutOfRangeException();

            MoveStack1ToStack2();

            return sc2.Pop();
        }

        public int Peek()
        {
            if (IsEmpty())
                throw new System.IndexOutOfRangeException();

            MoveStack1ToStack2();

            return sc2.Peek();
        }

        public bool IsEmpty()
        {
            return sc1.IsEmpty() && sc2.IsEmpty();
        }

        private void MoveStack1ToStack2()
        {
            if (sc2.IsEmpty())
            {
                while (!sc1.IsEmpty())
                    sc2.Push(sc1.Pop());
            }
        }
    }

    public class QueuePriority
    {
        private int[] items = new int[5];
        private int count;
        private int front;

        public void Add(int item)
        {
            if (IsFull())
                throw new System.ArgumentOutOfRangeException();

            int i = ShiftItems(item);
            
            items[i] = item;
            count++;
        }

        public int Remove()
        {
            if(IsEmpty())
            {
                throw new System.ArgumentOutOfRangeException();
            }

            int i = items[front];
            items[front] = 0;
            front = (front + 1) % items.Length;
            count--;
            return i;
        }

        public bool IsEmpty()
        {
            return count == 0;
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

        public int Size()
        {
            return count;
        }

        private bool IsFull()
        {
            return count == items.Length;
        }

        private int ShiftItems(int item)
        {
            int i = 0;
            for (i = count - 1; i >= 0; i--)
            {
                if (items[i] > item)
                {
                    items[i + 1] = items[i];
                }
                else
                {
                    break;
                }
            }

            return i + 1;
        }
    }

    public class QueueReverser
    {
        public string Test()
        {
            string t = "";
            Queue<int> q1 = new Queue<int>();
            q1.Enqueue(10);
            q1.Enqueue(20);
            q1.Enqueue(30);
            q1.Enqueue(40);
            q1.Enqueue(50);

            Stack<int> s1 = new Stack<int>();

            for (int i = 0; i < 3; i++)
            {
                s1.Push(q1.Dequeue());
            }

            while(s1.Count > 0)
            {
                q1.Enqueue(s1.Pop());
            }

            for (int i = 0; i < 2; i++)
            {
                q1.Enqueue(q1.Dequeue());
            }

            foreach (var item in q1)
            {
                t += item.ToString() + ",";
            }

            return t;
        }
    }

    public class QueueLinkedList
    {
        private List<int> items = new List<int>();

        public void Enqueue(int i)
        {
            items.Add(i);
        }

        public int Dequeue()
        {
            if(IsEmpty())
                throw new System.Exception();

            int i = items[0];
            items.RemoveAt(0);
            return i;
        }

        public int Peek()
        {
            if (IsEmpty())
                throw new System.Exception();

            int i = items[0];
            return i;
        }

        public int Size()
        {
            return items.Count;
        }

        public bool IsEmpty()
        {
            return items.Count <= 0;
        }

        public override string ToString()
        {
            string t = "";
            for (int i = 0; i < items.Count; i++)
            {
                t += items[i].ToString() + ",";
            }
            return t;
        }
    }

    public class QueueList
    {
        private class Node
        {
            private int value;
            private Node next;

            public Node Next { get => next; set => next = value; }
            public int Value { get => value; set => this.value = value; }

            public Node(int v)
            {
                this.Value = v;
            }
        }

        private Node head;
        private Node tail;
        private int count;

        public void Enqueue(int item)
        {
            Node node = new Node(item);

            if(IsEmpty())
            {
                head = node;
                tail = node;
            }
            else
            {
                tail.Next = node;
                tail = node;
            }

            count++;
        }

        public int Dequeue()
        {
            if (IsEmpty())
                throw new System.Exception();

            int i = 0;
            if(head == tail)
            {
                i = head.Value;
                head = null;
                tail = null;
            }
            else
            {
                i = head.Value;
                Node s = head.Next;
                head.Next = null;
                head = s;
            }

            count--;

            return i;
        }

        public int Peek()
        {
            if (IsEmpty())
                throw new System.Exception();

            return head.Value;
        }

        public int Size()
        {
            return count;
        }

        public bool IsEmpty()
        {
            return head == null;
        }

        public override string ToString()
        {
            string t = "";
            Node cur = head;
            while(cur != null)
            {
                t += cur.Value.ToString() + ",";
                cur = cur.Next;
            }

            return t;
        }
    }

    public class StackQueue
    {
        private Queue<int> q1 = new Queue<int>();
        private Queue<int> q2 = new Queue<int>();
        private int count;
        private int top;

        public void Push(int i)
        {
            q1.Enqueue(i);
            top = i;
            count++;
        }

        public int Pop()
        {
            if (IsEmpty())
                throw new System.Exception();

            while(q1.Count > 1)
            {
                top = q1.Dequeue();
                q2.Enqueue(top);
            }

            Swap();

            return q2.Dequeue();
        }

        public int Peek()
        {
            if (IsEmpty())
                throw new System.Exception();

            return top;
        }

        public int Size()
        {
            return count;
        }

        public bool IsEmpty()
        {
            return q1.Count <= 0;
        }

        private void Swap()
        {
            Queue<int> temp = q1;
            q1 = q2;
            q2 = temp;
        }

        public override string ToString()
        {
            int[] q = q1.ToArray();
            string t = "";
            for (int i = 0; i < q.Length; i++)
            {
                t += q[i].ToString() + ",";
            }

            return t;
        }
    }

    public class Queues : MonoBehaviour
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
        public Text Info9Text;
        public Text Info10Text;
        public Text Info11Text;

        QueueTest qt = new QueueTest();
        Queue<int> q = new Queue<int>();
        QueueCustom qc = new QueueCustom(3);
        QueueStack qs = new QueueStack();
        QueuePriority qp = new QueuePriority();
        QueueReverser qv = new QueueReverser();
        QueueLinkedList ql = new QueueLinkedList();
        QueueList qll = new QueueList();
        StackQueue sq = new StackQueue();

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            q.Enqueue(10);
            q.Enqueue(15);
            q.Enqueue(20);
            q.Enqueue(30);
            InfoText.text = qt.Test(q);

            qc.Enqueue(1);
            qc.Enqueue(4);
            qc.Enqueue(6);
            Info2Text.text = qc.ToString() + "/" + qc.Dequeue().ToString() + "/" + qc.ToString();

            qc.Enqueue(12);
            Info3Text.text = qc.ToString() + "/" + qc.Dequeue();

            qs.Enqueue(4);
            qs.Enqueue(14);
            qs.Enqueue(41);
            Info4Text.text = qs.Dequeue().ToString();
            Info5Text.text = qs.Dequeue().ToString();

            qp.Add(4);
            qp.Add(12);
            qp.Add(2);
            qp.Add(3);
            Info6Text.text = qp.ToString();

            string t = "";
            while (!qp.IsEmpty())
                t += qp.Remove() + "/";
            Info7Text.text = t;

            Info8Text.text = qv.Test();

            ql.Enqueue(1);
            ql.Enqueue(12);
            ql.Enqueue(15);
            ql.Enqueue(21);
            Info9Text.text = ql.ToString() + "/" + ql.Size() + "/" + ql.Peek() + "/" + ql.Dequeue() + "\n" + ql.ToString();

            qll.Enqueue(10);
            qll.Enqueue(12);
            qll.Enqueue(20);
            qll.Enqueue(34);
            qll.Enqueue(45);
            Info10Text.text = qll.ToString() + "/" + qll.Size() + "/" + qll.Peek() + "/" + qll.Dequeue() + "\n" + qll.ToString();

            sq.Push(1);
            sq.Push(3);
            sq.Push(6);
            sq.Push(12);
            sq.Push(5);
            Info11Text.text = sq.ToString() + "/" + sq.Size() + "/" + sq.Peek() + "/" + sq.Pop() + "\n" + sq.ToString();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}