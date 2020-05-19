using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoF.ChainOfResponsibility
{
    //--------------------------------------VARIANT 1-----------------------------------
    public class HttpRequest
    {
        private string username;
        private string password;

        public HttpRequest(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
    }

    public class Authenticator : Handler
    {
        public Authenticator(Handler next) : base(next)
        {
        }

        public override bool DoHandle(HttpRequest request)
        {
            Status += "Authenticator\n";
            Debug.Log("Authenticator");
            bool isValid = (request.Username == "admin" &&
                request.Password == "1234");
            return !isValid;
        }
    }

    public class Compressor : Handler
    {
        public Compressor(Handler next) : base(next)
        {
        }

        public override bool DoHandle(HttpRequest request)
        {
            Status += "Compressor\n";
            Debug.Log("Compressor");
            return false;
        }
    }

    public class Logger : Handler
    {
        public Logger(Handler next) : base(next)
        {
        }

        public override bool DoHandle(HttpRequest request)
        {
            Status += "Logger\n";
            Debug.Log("Logger");
            return false;
        }
    }

    public class Encrypter : Handler
    {
        public Encrypter(Handler next) : base(next)
        {
        }

        public override bool DoHandle(HttpRequest request)
        {
            Debug.Log("Encrypter");
            return false;
        }
    }

    public abstract class Handler
    {
        private Handler next;
        public string Status;

        public Handler(Handler next)
        {
            this.next = next;
        }

        public void Handle(HttpRequest request)
        {
            if (DoHandle(request))
            {
                Status += next.Status;
                return;
            }

            if (next != null)
            {
                Status += next.Status;
                next.Handle(request);
            }
        }

        public abstract bool DoHandle(HttpRequest request);
    }

    public class WebServer
    {
        private Handler handler;
        public string Status;
        public WebServer(Handler handler)
        {
            this.handler = handler;
            this.Status = handler.Status;
        }

        public void Handle(HttpRequest request)
        {
            handler.Handle(request);
        }
    }

    //--------------------------------------VARIANT 2-----------------------------------

    public class XlsHandler : DataHandler
    {
        protected override bool DoHandle(string data)
        {
            Debug.Log("Xls");
            return false;
        }
    }

    public class NumbersHandler : DataHandler
    {
        protected override bool DoHandle(string data)
        {
            Debug.Log("Numbers");
            return false;
        }
    }

    public class QbwHandler : DataHandler
    {
        protected override bool DoHandle(string data)
        {
            Debug.Log("Qbw");
            return false;
        }
    }

    public abstract class DataHandler
    {
        private DataHandler next;

        public void SetNext(DataHandler next)
        {
            if(next != null)
                this.next = next;
        }

        public void Handle(string data)
        {
            if (next != null && next.DoHandle(data))
                return;

            if (next != null)
                next.Handle(data);
        }

        protected abstract bool DoHandle(string data);
    }

    public class DataFactory
    {
        public DataHandler GetDataHandler()
        {
            XlsHandler xls = new XlsHandler();
            NumbersHandler nums = new NumbersHandler();
            QbwHandler qbw = new QbwHandler();
            QbwHandler qbw2 = new QbwHandler();

            qbw2.SetNext(null);
            xls.SetNext(qbw2);
            nums.SetNext(xls);
            qbw.SetNext(nums);

            return qbw;
        }
    }

    public class ChainOfResponsibility : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;

        Encrypter e;
        Compressor c;
        Logger l;
        Authenticator a;
        WebServer w;
        HttpRequest h;

        public string Name;
        public string Pass;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            e = new Encrypter(null);
            c = new Compressor(e);
            l = new Logger(c);
            a = new Authenticator(l);
            w = new WebServer(a);
            h = new HttpRequest(Name, Pass);
            w.Handle(h);

            InfoText.text = w.Status;

            DataFactory df = new DataFactory();
            df.GetDataHandler().Handle("Test");
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}