using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DataStructuresAndAlgorithms.BinaryTrees
{
    // Lookup: O(log N)
    // Insert: O(log N)
    // Delete: O(log N)

    //      7
    //    4   9
    //   1 6 8 10
    // Breadth First: 
    // - Level Order: 7 4 9 1 6 8 10
    // Depth First:
    // - Pre-order  (root, left, right): 7 4 1 6 9 8 10
    // - In-order   (left, root, right): 1 4 6 7 8 9 10
    // - Post-order (left, right, root): 1 6 4 8 10 9 7

    public class Tree
    {
        private class Node
        {
            private int value;
            private Node leftChild;
            private Node rightChild;

            public Node(int value)
            {
                this.Value = value;
            }

            public Node LeftChild { get => leftChild; set => leftChild = value; }
            public Node RightChild { get => rightChild; set => rightChild = value; }
            public int Value { get => value; set => this.value = value; }
        }

        private Node root;

        public void Insert(int value)
        {
            Node node = new Node(value);

            if (root == null)
            {
                root = node;
                return;
            }

            Node curr = root;

            while (true)
            {
                if(value < curr.Value)
                {
                    if(curr.LeftChild == null)
                    {
                        curr.LeftChild = node;
                        break;
                    }

                    curr = curr.LeftChild;
                }
                else
                {
                    if(curr.RightChild == null)
                    {
                        curr.RightChild = node;
                        break;
                    }

                    curr = curr.RightChild;
                }
            }
        }

        public bool Find(int value)
        {
            Node curr = root;
            while (curr != null)
            {
                if (value < curr.Value)
                {
                    curr = curr.LeftChild;
                }
                else if (value > curr.Value)
                {
                    curr = curr.RightChild;
                }
                else
                    return true;
            }

            return false;
        }

        public int Min()
        {
            return Min(root);
        }

        private int Min(Node root)
        {
            if (root == null)
                return -1;

            if (IsLeaf(root))
                return root.Value;

            int left = Min(root.LeftChild);
            int right = Min(root.LeftChild);

            return Mathf.Min(Mathf.Min(left, right), root.Value);
        }

        public int Height()
        {
            return Height(root);
        }

        private bool IsLeaf(Node root)
        {
            return root.LeftChild == null && root.RightChild == null;
        }

        private int Height(Node root)
        {
            if(root == null)
            {
                return -1;
            }

            if(IsLeaf(root)) 
            { 
                return 0; 
            }

            return 1 + Mathf.Max(Height(root.LeftChild), Height(root.RightChild));
        }

        public void TraversePreOrder()
        {
            TraversePreOrder(root);
        }

        public void TraverseInOrder()
        {
            TraverseInOrder(root);
        }
        public void TraversePostOrder()
        {
            TraversePostOrder(root);
        }

        private void TraversePreOrder(Node root)
        {
            if(root == null)
            {
                return;
            }

            Debug.Log(root.Value);
            TraversePreOrder(root.LeftChild);
            TraversePreOrder(root.RightChild);
        }

        private void TraverseInOrder(Node root)
        {
            if(root == null)
            {
                return;
            }

            TraverseInOrder(root.LeftChild);
            Debug.Log(root.Value);
            TraverseInOrder(root.RightChild);
        }

        private void TraversePostOrder(Node root)
        {
            if(root == null)
            {
                return;
            }

            TraversePostOrder(root.LeftChild);
            TraversePostOrder(root.RightChild);
            Debug.Log(root.Value);
        }
    }

    public class Recursion
    {
        public int FactorialLoop(int n)
        {
            int fact = 1;
            for (int i = n; i > 1; i--)
            {
                fact *= i;
            }
            return fact;
        }

        public int FactorialRecursion(int n)
        {
            if (n == 0)
                return 1;

            return n * FactorialRecursion(n - 1);
        }
    }

    public enum Order
    {
        Pre,
        In,
        Post
    }

    public class BinaryTrees : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;

        public Order TreeOrder = Order.Pre;

        Tree tree = new Tree();

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            tree.Insert(7);
            tree.Insert(4);
            tree.Insert(9);
            tree.Insert(1);
            tree.Insert(6);
            tree.Insert(8);
            tree.Insert(10);

            switch (TreeOrder)
            {
                case Order.Pre:
                    tree.TraversePreOrder();
                    break;
                case Order.In:
                    tree.TraverseInOrder();
                    break;
                case Order.Post:
                    tree.TraversePostOrder();
                    break;
            }

            InfoText.text = tree.Height().ToString(); 
            Info2Text.text = tree.Min().ToString();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}