using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoF.Observer
{
    //-------------------------------------------VARIANT 1-------------------------------

    public interface IObserver
    {
        string Update(string s);
    }

    public class DataSource : Subject
    {
        private string value;
        public string Value { 
            get => value; 
            set
            {
                this.value = value + "\n" + Notify(value);
            }
        }

    }

    public class Subject
    {
        private List<IObserver> observers = new List<IObserver>();

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public string Notify(string s)
        {
            string t = "";
            foreach (var item in observers)
            {
                t += item.Update(s) + "\n";
            }

            return t;
        }
    }

    public class SpreadSheet : IObserver
    {
        public string Update(string s)
        {
            return $"SpreadSheet updated with {s}.";
        }
    }

    public class Chart : IObserver
    {
        public string Update(string s)
        {
            return $"Chart updated with {s}.";
        }
    }

    //-------------------------------------------VARIANT 2-------------------------------

    public interface IObserver2
    {
        string Update();
    }

    public class DataSource2 : Subject2
    {
        private string value;
        public string Value
        {
            get => value;
            set
            {
                this.value = value + "\n" + Notify();
            }
        }

    }

    public class Subject2
    {
        private List<IObserver2> observers = new List<IObserver2>();

        public void AddObserver(IObserver2 observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver2 observer)
        {
            observers.Remove(observer);
        }

        public string Notify()
        {
            string t = "";
            foreach (var item in observers)
            {
                t += item.Update() + "\n";
            }

            return t;
        }
    }

    public class SpreadSheet2 : IObserver2
    {
        private DataSource2 ds;

        public SpreadSheet2(DataSource2 ds)
        {
            this.ds = ds;
        }

        public string Update()
        {
            return $"SpreadSheet updated with {ds.Value}.";
        }
    }

    public class Chart2 : IObserver2
    {
        private DataSource2 ds;

        public Chart2(DataSource2 ds)
        {
            this.ds = ds;
        }

        public string Update()
        {
            return $"Chart updated with {ds.Value}.";
        }
    }

    //-------------------------------------------VARIANT 3-------------------------------

    public class Stock
    {
        private string symbol;
        private float price;
        private List<IStockObserver> stockObservers = new List<IStockObserver>();

        public void Add(IStockObserver observer)
        {
            stockObservers.Add(observer);
        }

        public void Remove(IStockObserver observer)
        {
            stockObservers.Remove(observer);
        }

        public string Notify()
        {
            string t = "";
            foreach (var item in stockObservers)
            {
                t += item.Update() + "\n";
            }
            return t;
        }

        public Stock(string symbol, float price)
        {
            this.Symbol = symbol;
            this.Price = price;
        }

        public string Symbol { get => symbol; set => symbol = value; }
        public float Price { get => price; set => price = value; }

        public override string ToString()
        {
            return $"{symbol} : {price}";
        }
    }

    public interface IStockObserver
    {
        string Update();
    }

    public class StatusBar : IStockObserver
    {
        private List<Stock> stocks = new List<Stock>();

        public void Add(Stock stock)
        {
            stocks.Add(stock);
            stock.Add(this);
        }

        private string Show()
        {
            string t = "";
            foreach (var item in stocks)
            {
                t += item.ToString() + "\n";
            } 
            return t;
        }

        public string Update()
        {
            return $"StatusBar updated.\n" + Show();
        }
    }

    public class StockListView : IStockObserver
    {
        private List<Stock> stocks = new List<Stock>();

        public void Add(Stock stock)
        {
            stocks.Add(stock);
            stock.Add(this);
        }

        private string Show()
        {
            string t = "";
            foreach (var item in stocks)
            {
                t += item.ToString() + "\n";
            }
            return t;
        }

        public string Update()
        {
            return $"StockListView updated.\n" + Show();
        }
    }

    public class Observer : MonoBehaviour
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

        DataSource ds = new DataSource();
        SpreadSheet ss1 = new SpreadSheet();
        SpreadSheet ss2 = new SpreadSheet();
        Chart c = new Chart();

        DataSource2 ds2 = new DataSource2();
        SpreadSheet2 ss12;
        SpreadSheet2 ss22;
        Chart2 c2;

        StatusBar sb = new StatusBar();
        StockListView sl = new StockListView();
        Stock s1 = new Stock("s1", 10);
        Stock s2 = new Stock("s2", 20);
        Stock s3 = new Stock("s3", 30);

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            ds.AddObserver(ss1);
            ds.AddObserver(ss2);
            ds.AddObserver(c);
            ds.Value = "Hello";
            InfoText.text = ds.Value;

            ds.RemoveObserver(ss2);
            ds.Value = "Hello 2";
            Info2Text.text = ds.Value;

            ds2.Value = "DS";
            ss12 = new SpreadSheet2(ds2);
            ss22 = new SpreadSheet2(ds2);
            c2 = new Chart2(ds2);
            ds2.AddObserver(ss12);
            ds2.AddObserver(ss22);
            ds2.AddObserver(c2);
            Info3Text.text = ds2.Value;

            ds2.Value = "DS 2";
            ds2.RemoveObserver(ss22);
            Info4Text.text = ds2.Value;

            sb.Add(s1);
            sb.Add(s2);
            Info5Text.text = sb.Update();

            sl.Add(s1);
            sl.Add(s2);
            sl.Add(s3);
            Info6Text.text = sl.Update();

            s1.Price = 13;
            s2.Price = 18;
            Info7Text.text = sl.Update();

            Info8Text.text = s1.Notify();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}