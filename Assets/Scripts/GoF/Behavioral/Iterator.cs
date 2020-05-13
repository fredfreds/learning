using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoF.Iterator
{
    //---------------------------------------------------VARIANT 1------------------------

    public interface IIterator<T>
    {
        bool HasNext();
        T Current();
        void Next();
    }

    public class BrowserHistory
    {
        private List<string> history = new List<string>();

        public void Add(string url)
        {
            history.Add(url);
        }

        public string Remove()
        {
            int lastIndex = history.Count - 1;
            string lastUrl = history[lastIndex];
            history.Remove(lastUrl);
            return lastUrl;
        }

        public IIterator<string> CreateIterator()
        {
            return new StringIterator(this);
        }

        public class StringIterator : IIterator<string>
        {
            private BrowserHistory history;
            private int index;

            public StringIterator(BrowserHistory history)
            {
                this.history = history;
            }

            public string Current()
            {
                return history.history[index];
            }

            public bool HasNext()
            {
                return index < history.history.Count;
            }

            public void Next()
            {
                index++;
            }
        }
    }

    public class ChatHistory
    {
        private int[] history = new int[10];
        private int index;

        public void Add(int msg)
        {
            history[index++] = msg;
        }

        public int Remove()
        {
            return history[--index];
        }

        public IIterator<int> CreateIterator()
        {
            return new ArrayIterator(this);
        }

        public class ArrayIterator : IIterator<int>
        {
            private ChatHistory history;
            private int index;

            public ArrayIterator(ChatHistory history)
            {
                this.history = history;
            }

            public int Current()
            {
                return history.history[index];
            }

            public bool HasNext()
            {
                return index < history.history.Length;
            }

            public void Next()
            {
                index++;
            }
        }
    }

    //---------------------------------------------------VARIANT 2------------------------

    public class Product
    {
        private int id;
        private string name;

        public Product(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public override string ToString()
        {
            return $"Id: {id}, Name: {name}";
        }
    }

    public class ProductCollection
    {
        private List<Product> products = new List<Product>();

        public void Add(Product product)
        {
            products.Add(product);
        }

        public IIterator<Product> CreateIterator()
        {
            return new ProductIterator(this);
        }

        public class ProductIterator : IIterator<Product>
        {
            private ProductCollection productCollection;
            private int index;

            public ProductIterator(ProductCollection productCollection)
            {
                this.productCollection = productCollection;
            }

            public Product Current()
            {
                return productCollection.products[index];
            }

            public bool HasNext()
            {
                return index < productCollection.products.Count;
            }

            public void Next()
            {
                index++;
            }
        }
    }

    public class Iterator : MonoBehaviour
    {
        public Button BuildButton;
        public Button BuildButton1;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;

        BrowserHistory h = new BrowserHistory();
        ChatHistory c = new ChatHistory();
        ProductCollection p = new ProductCollection();

        IIterator<string> iterator;
        IIterator<int> iterator2;
        IIterator<Product> iterator3;

        public string Url;
        public int Msg;

        public string Name;
        public int Id;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
            BuildButton1.onClick.AddListener(Build1);
        }

        private void Build()
        {
            h.Add(Url);
            string t = "";
            iterator = h.CreateIterator();
            while (iterator.HasNext())
            {
                string url = iterator.Current();
                t += url;
                iterator.Next();
            }
            InfoText.text = t;

            c.Add(Msg);
            string t1 = "";
            iterator2 = c.CreateIterator();
            while (iterator2.HasNext())
            {
                int msg = iterator2.Current();
                t1 += msg;
                iterator2.Next();
            }
            Info2Text.text = t1;

            p.Add(new Product(Id, Name));
            string t2 = "";
            iterator3 = p.CreateIterator();
            while (iterator3.HasNext())
            {
                Product pr = iterator3.Current();
                t2 += pr.ToString() + "\n";
                iterator3.Next();
            }
            Info3Text.text = t2;
        }

        private void Build1()
        {
            h.Remove();
            string t = "";
            iterator = h.CreateIterator();
            while (iterator.HasNext())
            {
                string url = iterator.Current();
                t += url;
                iterator.Next();
            }
            InfoText.text = t;

            c.Remove();
            string t1 = "";
            iterator2 = c.CreateIterator();
            while (iterator2.HasNext())
            {
                int msg = iterator2.Current();
                t1 += msg;
                iterator2.Next();
            }
            Info2Text.text = t1;
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
            BuildButton1.onClick.RemoveListener(Build1);
        }
    }
}