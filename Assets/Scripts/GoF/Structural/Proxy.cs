using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoF.Proxy
{
    //---------------------------------------------VARIANT 1-----------------

    public interface IBook
    {
        string Show();
        string FileName();
    }

    public class EbookProxy : IBook
    {
        private string fileName;
        private IBook book;

        public EbookProxy(string fileName)
        {
            this.fileName = fileName;
        }

        public string FileName()
        {
            string t = "";
            if (book == null)
                t = new Ebook(fileName).FileName();
            return t;
        }

        public string Show()
        {
            if (book == null)
                book = new Ebook(fileName);
            return book.Show();
        }
    }

    public class LoggingEbookProxy : IBook
    {
        private string fileName;
        private IBook book;

        public LoggingEbookProxy(string fileName)
        {
            this.fileName = fileName;
        }

        public string FileName()
        {
            string t = "";
            if (book == null)
                t = new Ebook(fileName).FileName();
            return t;
        }

        public string Show()
        {
            if (book == null)
                book = new Ebook(fileName);
            return "Logging " + book.Show();
        }
    }

    public class Ebook : IBook
    {
        private string fileName;

        public string FileName() 
        { 
            return fileName; 
        }

        public Ebook(string fileName)
        {
            this.fileName = fileName;
            Load();
        }

        private void Load()
        {
            Debug.Log("Load = " + fileName);
        }

        public string Show()
        {
            return "Book = " + fileName;
        }
    }

    public class Library
    {
        private Dictionary<string, IBook> ebooks = new Dictionary<string, IBook>();

        public void Add(IBook book)
        {
            ebooks.Add(book.FileName(), book);
        }

        public string Open(string book)
        {
            return ebooks[book].Show();
        }
    }

    public class LibraryTest1
    {
        private string[] books = new string[] { "a", "b", "c" };
        private Library library = new Library();

        public LibraryTest1()
        {
            Test();
        }

        private void Test()
        {
            foreach (var item in books)
            {
                library.Add(new EbookProxy(item));
            }
        }

        public string Open()
        {
            return library.Open("a");
        }
    }

    public class LibraryTest2
    {
        private string[] books = new string[] { "a", "b", "c" };
        private Library library = new Library();

        public LibraryTest2()
        {
            Test();
        }

        private void Test()
        {
            foreach (var item in books)
            {
                library.Add(new LoggingEbookProxy(item));
            }
        }

        public string Open()
        {
            return library.Open("b");
        }
    }

    public class LibraryTest3
    {
        private string[] books = new string[] { "a", "b", "c" };
        private Library library = new Library();

        public LibraryTest3()
        {
            Test();
        }

        private void Test()
        {
            foreach (var item in books)
            {
                library.Add(new Ebook(item));
            }
        }

        public string Open()
        {
            return library.Open("c");
        }
    }

    //---------------------------------------------VARIANT 2-----------------

    public class Product
    {
        private int id;
        private string name;

        public Product(int id)
        {
            this.id = id;
        }

        public int GetId()
        {
            return id;
        }

        public string GetName()
        {
            return name;
        }

        public virtual void SetName(string n)
        {
            this.name = n;
        }
    }

    public class DbContext
    {
        private Dictionary<int, Product> objects = new Dictionary<int, Product>();

        public Product GetProduct(int id)
        {
            Product product = new Product(id);
            product.SetName("Product 1");
            return product;
        }

        public string SaveChanges()
        {
            string t = "";
            foreach (var item in objects)
            {
                t += item.Key + "/" + item.Value.GetName() + "\n";
            }
            objects.Clear();

            return t;

        }

        public void MarkAsChanged(Product product)
        {
            objects.Add(product.GetId(), product);
        }
    }

    public class ProxyProduct
    {
        private Product product;
        private DbContext db;

        public ProxyProduct(Product product, DbContext db)
        {
            this.product = product;
            this.db = db;
        }

        public void SetName(string n)
        {
            product.SetName(n);
            db.MarkAsChanged(product);
        }
    }

    public class Proxy : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;
        public Text Info5Text;
        public Text Info6Text;
        public Text Info7Text;

        LibraryTest1 lt1 = new LibraryTest1();
        LibraryTest2 lt2 = new LibraryTest2();
        LibraryTest3 lt3 = new LibraryTest3();

        DbContext db = new DbContext();
        Product p;
        Product p2;
        ProxyProduct pp;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            InfoText.text = lt1.Open();
            Info2Text.text = lt2.Open();
            Info3Text.text = lt3.Open();

            p = db.GetProduct(1);
            Info4Text.text = p.GetName();
            p.SetName("Updated 1");
            Info5Text.text = p.GetName();
            Info6Text.text = db.SaveChanges();
            pp = new ProxyProduct(p, db);
            p2 = db.GetProduct(2);
            pp.SetName("Updated2 1");
            pp = new ProxyProduct(p2, db);
            pp.SetName("Updated2 2");
            Info7Text.text = db.SaveChanges();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}