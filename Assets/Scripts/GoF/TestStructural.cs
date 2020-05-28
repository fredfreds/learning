using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoF.TestStructural
{
    #region Composite
    // Composite - Represents object hierarchies where individual objects and compositions of
    // objects are treated the same way.

    public abstract class Composite
    {
        public abstract string Operation();
    }

    public class CompositeItem : Composite
    {
        public override string Operation()
        {
            return "Item";
        }
    }

    public class CompositeGroup : Composite
    {
        private List<Composite> composites = new List<Composite>();

        public void Add(Composite item)
        {
            composites.Add(item);
        }

        public override string Operation()
        {
            string t = "";
            foreach (var item in composites)
            {
                t += item.Operation();
            }
            return t;
        }
    }

    public class CompositeTest
    {
        Composite item1 = new CompositeItem();
        Composite item2 = new CompositeItem();
        Composite item3 = new CompositeItem();

        CompositeGroup group = new CompositeGroup();

        public string Test()
        {
            string t = "";
            group.Add(item1);
            group.Add(item2);
            group.Add(item3);
            t += group.Operation();
            return t;
        }
    }

    #endregion

    #region Adapter
    // Adapter - Allows converting the interface of a class into another interface that clients
    // expect.

    public interface ITarget
    {
        string Operation();
    }

    public class Client
    {
        private ITarget target;

        public Client(ITarget target)
        {
            this.target = target;
        }

        public string Test()
        {
            return target.Operation();
        }
    }

    public class Adaptee
    {
        public string Operation()
        {
            return "Adaptee ";
        }
    }

    public class Adapter : ITarget
    {
        private Adaptee adaptee;

        public Adapter(Adaptee adaptee)
        {
            this.adaptee = adaptee;
        }

        public string Operation()
        {
            return adaptee.Operation();
        }
    }

    public class AdapterTest
    {
        Client client;
        Adaptee adaptee = new Adaptee();
        Adapter adapter;

        public string Test() 
        {
            adapter = new Adapter(adaptee);
            client = new Client(adapter);
            return client.Test();
        }
    }

    #endregion

    #region Decorator

    // Decorator - Adds additional behavior to an object dynamically

    public interface IDecorator
    {
        string Operation();
    }

    public class ConcreteComponent : IDecorator
    {
        public string Operation()
        {
            return "Concrete";
        }
    }

    public class Decorator : IDecorator
    {
        private IDecorator decorator;

        public Decorator(IDecorator decorator)
        {
            this.decorator = decorator;
        }

        public string Operation()
        {
            return AddBehaviour() + decorator.Operation();
        }

        private string AddBehaviour()
        {
            return "Decorator ";
        }
    }

    public class DecoratorTest
    {
        IDecorator d1;
        IDecorator d2;

        public string Test()
        {
            d1 = new ConcreteComponent();
            d2 = new Decorator(d1);
            return d2.Operation();
        }
    }

    #endregion

    #region Facade

    // Facade - Provides a simplified, higher-level interface to a subsystem. Clients can talk
    // to the facade rather than individual classes in the subsystem.

    public class FacadeClient
    {
        public string Test()
        {
            Facade f = new Facade();
            return f.Operation();
        }
    }

    public class Facade
    {
        public string Operation()
        {
            Operation1 operation1 = new Operation1();
            Operation2 operation2 = new Operation2();
            Operation3 operation3 = new Operation3();
            return operation1.O1() + operation2.O2() + operation3.O3();
        }
    }

    public class Operation1
    {
        public string O1()
        {
            return "0peration 1 ";
        }
    }

    public class Operation2
    {
        public string O2()
        {
            return "0peration 2 ";
        }
    }

    public class Operation3
    {
        public string O3()
        {
            return "0peration 3 ";
        }
    }

    #endregion

    #region Flyweight

    // Flyweight

    public class FLyClient
    {
        private FlyweightFactory factory;

        public FLyClient(FlyweightFactory factory)
        {
            this.factory = factory;
        }

        public string Test()
        {
            string t = "";
            Flyweight f1 = new Flyweight("Fred", factory.Run("Fred"));
            Flyweight f2 = new Flyweight("Fred", factory.Run("Fred"));
            Flyweight f3 = new Flyweight("Fill", factory.Run("Fill"));
            Flyweight f4 = new Flyweight("Fill", factory.Run("Fred"));
            Flyweight f5 = new Flyweight("Fill", factory.Run("Lol"));

            List<Flyweight> flyweights = new List<Flyweight>();
            flyweights.Add(f1);
            flyweights.Add(f2);
            flyweights.Add(f3);
            flyweights.Add(f4);
            flyweights.Add(f5);

            foreach (var item in flyweights)
            {
                t += item.Name + "/" + item.Image + "\n";
            }

            return t;
        }
    }

    public class Flyweight
    {
        public string Name;
        public FlyweightImage Image;

        public Flyweight(string name, FlyweightImage image)
        {
            Name = name;
            Image = image;
        }
    }

    public class FlyweightImage
    {
        public string Name;
    }

    public class FlyweightFactory
    {
        private Dictionary<string, FlyweightImage> pairs = new Dictionary<string, FlyweightImage>();
        private int countC;
        private int countN;

        public FlyweightImage Run(string n)
        {
            if (!pairs.ContainsKey(n))
            {
                FlyweightImage im = new FlyweightImage();
                pairs.Add(n, im);
                Debug.Log("Contain " + countC);
                return im;
            }
            else
            {
                Debug.Log("Do not contain" + countN);
                return pairs[n];
            }
        }
    }

    public class FlyweightTest
    {
        public FLyClient fc;
        public FlyweightFactory ff = new FlyweightFactory();

        public string Test()
        {
            fc = new FLyClient(ff);
            return fc.Test();
        }
    }

    #endregion

    #region Bridge

    // Bridge - Allows representing hierarchies that grow in two different dimensions
    // independently.

    public class Bridge
    {
        RemoteControl rc;
        AdvancedRemoteControl arc;
        SonyTV stv = new SonyTV();
        SamsungTV sam = new SamsungTV();

        public string Test()
        {
            rc = new RemoteControl(stv);
            arc = new AdvancedRemoteControl(sam);
            return rc.TurnOn() + "\n" + arc.TurnOff();
        }
    }

    public interface IDevice
    {
        string TurnOn();
    }

    public class SonyTV : IDevice
    {
        public string TurnOn()
        {
            return "Sony TV Turn ON";
        }
    }

    public class SamsungTV : IDevice
    {
        public string TurnOn()
        {
            return "Samsung TV Turn ON";
        }
    }

    public class RemoteControl
    {
        private IDevice device;

        public RemoteControl(IDevice device)
        {
            this.device = device;
        }

        public string TurnOn()
        {
            return device.TurnOn();
        }
    }

    public class AdvancedRemoteControl : RemoteControl
    {
        private IDevice device;
        public AdvancedRemoteControl(IDevice device) : base(device)
        {
            this.device = device;
        }

        public string TurnOff()
        {
            return device.TurnOn() + " and Turn OFF";
        }
    }

    #endregion

    #region Proxy

    // Proxy - Allows providing a substitute for another object. The proxy object delegates
    // all the work to the target object and contains some additional behavior.

    public class ProxyClient
    {
        public string Test()
        {
            RealObject ro = new RealObject();
            Proxy proxy = new Proxy(ro);
            return ro.Request() + "\n" + proxy.Request();
        }
    }

    public interface IProxy
    {
        string Request();
    }

    public class Proxy : IProxy
    {
        private RealObject ro;

        public Proxy(RealObject ro)
        {
            this.ro = ro;
        }

        public string Request()
        {
            return ro.Request() + " Proxy";
        }
    }

    public class RealObject : IProxy
    {
        public string Request()
        {
            return "Real";
        }
    }

    #endregion

    public class TestStructural : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;
        public Text Info5Text;
        public Text Info6Text;
        public Text Info7Text;

        CompositeTest ct = new CompositeTest();
        AdapterTest at = new AdapterTest();
        DecoratorTest dt = new DecoratorTest();
        FacadeClient fc = new FacadeClient();
        FlyweightTest ft = new FlyweightTest();
        Bridge b = new Bridge();
        ProxyClient pc = new ProxyClient();

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            InfoText.text = ct.Test();
            Info2Text.text = at.Test();
            Info3Text.text = dt.Test();
            Info4Text.text = fc.Test();
            Info5Text.text = ft.Test();
            Info6Text.text = b.Test();
            Info7Text.text = pc.Test();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}