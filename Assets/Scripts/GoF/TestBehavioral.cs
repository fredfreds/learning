using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoF.Behavioral
{
    // Memento - Allows restoring an object to a previous state.
    #region Memento
    public class MementoUnit
    {
        public string content;

        public MementoState CreateState()
        {
            return new MementoState(content);
        }

        public void Restore(MementoState state)
        {
            content = state.content;
        }
    }

    public class MementoState
    {
        public string content;

        public MementoState(string content)
        {
            this.content = content;
        }
    }

    public class MementoHistory
    {
        private Stack<MementoState> states = new Stack<MementoState>();

        public void Push(MementoState state)
        {
            states.Push(state);
        }

        public MementoState Pop()
        {
            return states.Pop();
        }
    }

    public class TestMemento
    {
        public string Test1()
        {
            string t = "";
            MementoUnit u = new MementoUnit();
            u.content = "Hello";
            t += u.content + "\n";
            MementoState m = u.CreateState();
            u.content = "Hello 2";
            t += u.content + "\n";
            u.Restore(m);
            t += u.content + "\n";
            return t;
        }

        public string Test2()
        {
            string t = "";
            MementoHistory h = new MementoHistory();
            MementoUnit u = new MementoUnit();
            u.content = "Hello";
            t += u.content + "\n";
            MementoState m1 = u.CreateState();
            h.Push(m1);
            u.content = "Hello 2";
            t += u.content + "\n";
            MementoState m2 = u.CreateState();
            h.Push(m2);
            t += u.content + "\n";
            u.content = "Hello 3";
            t += u.content + "\n";
            u.Restore(h.Pop());
            t += u.content + "\n";
            return t;
        }
    }
    #endregion
    // State - Allows an object to behave differently depending on the state it is in.
    #region State
    public interface IState
    {
        string Handle();
    }

    public class ConcreteStateA : IState
    {
        public string Handle()
        {
            return "State 1";
        }
    }

    public class ConcreteStateB : IState
    {
        public string Handle()
        {
            return "State 2";
        }
    }

    public enum States
    {
        State1,
        State2
    }

    public class StateContext
    {
        private IState State;
        public string Context;
        public void Request(States states)
        {
            switch (states)
            {
                case States.State1:
                    State = new ConcreteStateA();
                    Context = State.Handle();
                    break;
                case States.State2:
                    State = new ConcreteStateB();
                    Context = State.Handle();
                    break;
                default:
                    break;
            }
        }
    }
    #endregion
    // Iterator - Allows iterating over an object without having to expose 
    //the object’s internal structure(which may change in the future).
    #region Iterator
    public interface IIterator
    {
        void Next();
        string Current();
        bool IsDone();
    }

    public interface IAggregate
    {
        void CreateIterator();
    }

    public class ConcreteAggregate : IAggregate
    {
        public List<string> History = new List<string>() { "One", "Two", "Three" };
        public ConcreteIterator c;

        public void CreateIterator()
        {
            c = new ConcreteIterator(this);
        }

        public class ConcreteIterator : IIterator
        {
            public ConcreteAggregate Aggregate;
            private int index;

            public ConcreteIterator(ConcreteAggregate aggregate)
            {
                this.Aggregate = aggregate;
            }

            public string Current()
            {
                return Aggregate.History[index];
            }

            public bool IsDone()
            {
                return index < Aggregate.History.Count;
            }

            public void Next()
            {
                index++;
            }
        }
    }

    public class TestIterator
    {
        public string Test1()
        {
            ConcreteAggregate ca = new ConcreteAggregate();
            ca.CreateIterator();
            string t = "";
            while (ca.c.IsDone())
            {
                t += ca.c.Current();
                ca.c.Next();
            }
            return t;
        }
    }
    #endregion
    // Strategy - Allows passing different algorithms (behaviours) to an object.
    #region Strategy
    public interface IStrategy
    {
        string Do();
    }

    public class StrategyA : IStrategy
    {
        public string Do()
        {
            return "A";
        }
    }

    public class StrategyB : IStrategy
    {
        public string Do()
        {
            return "B";
        }
    }

    public class StrategyContext
    {
        public string SetStrategyA()
        {
            return new StrategyA().Do();
        }

        public string SetStrategyB()
        {
            return new StrategyB().Do();
        }
    }

    public class StrategyTest
    {
        public string Test1()
        {
            string t = "";
            t += new StrategyContext().SetStrategyA();
            t += new StrategyContext().SetStrategyB();
            return t;
        }
    }
    #endregion
    // Template - Allows defining a template (skeleton) for an operation. 
    //Specific steps will then be implemented in subclasses.
    #region Template
    public abstract class Template
    {
        public string TemplateMethod()
        {
            string t = "";
            t += Operation1();
            t += Operation2();
            return t;
        }

        protected abstract string Operation1();
        protected abstract string Operation2();
    }

    public class ConcreteTemplate : Template
    {
        protected override string Operation1()
        {
            return "1";
        }

        protected override string Operation2()
        {
            return "2";
        }
    }

    public class TemplateTest
    {
        public string Test1()
        {
            return new ConcreteTemplate().TemplateMethod();
        }
    }
    #endregion
    // Command - Allows decouple a sender from a receiver. The sender will talk to 
    //the receive through a command.Commands can be undone and persisted.
    #region Command
    public interface ICommand
    {
        string Execute();
    }

    public class CommandA : ICommand
    {
        public Receiver Receiver = new Receiver();
        public string Execute()
        {
            return Receiver.Operation() + "A";
        }
    }

    public class CommandB : ICommand
    {
        public Receiver Receiver = new Receiver();
        public string Execute()
        {
            return Receiver.Operation() + "B";
        }
    }

    public class Invoker
    {
        public ICommand Command;
        public string Operation()
        {
            return Command.Execute();
        }
    }

    public class Receiver
    {
        public string Operation()
        {
            return "Operation";
        }
    }

    public class CommandTest
    {
        public string Test1()
        {
            string t = "";
            Invoker i = new Invoker();
            i.Command = new CommandA();
            t += i.Operation();
            i.Command = new CommandB();
            t += i.Operation();
            return t;
        }
    }
    #endregion
    // Observer - Allows an object notify other objects when its state changes.
    #region Observer
    public interface IObserver
    {
        string Update(string t);
    }

    public class ConcreteObserverA : IObserver
    {
        public string Update(string t)
        {
            return "Concrete A Update " + t;
        }
    }

    public class ConcreteObserverB : IObserver
    {
        public string Update(string t)
        {
            return "Concrete B Update " + t;
        }
    }

    public abstract class SubjectObserver
    {
        public List<IObserver> observers = new List<IObserver>();
        public void Add(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Remove(IObserver observer)
        {
            observers.Remove(observer);
        }

        public string Notify()
        {
            string t = "";
            foreach (var item in observers)
            {
                t += item.Update(SetT()) + "\n";
            }
            return t;
        }

        protected abstract string SetT();
    }

    public class ConcreteSubject : SubjectObserver
    {
        protected override string SetT()
        {
            return "Subject T";
        }
    }

    public class TestObserver
    {
        public string Test() 
        {
            string t = "";
            ConcreteSubject cs = new ConcreteSubject();
            cs.Add(new ConcreteObserverA());
            ConcreteObserverB cob = new ConcreteObserverB();
            cs.Add(cob);
            t += cs.Notify();
            cs.Remove(cob);
            t += cs.Notify();
            return t;
        }
    }
    #endregion
    // Mediator - Allows an object to encapsulate the communication between other objects. 
    #region Mediator
    public interface IColleague
    {
        string Join();
    }

    public class ConcreteColleague : IColleague
    {
        public Chat c = new Chat();
        public string Join()
        {
            //c.Colleague = this;
            return "Fred " + c.Say();
        }
    }

    public abstract class Mediator
    {
        public IColleague Colleague;// = new ConcreteColleague();

        public string Say()
        {
            return Do();
        }

        public string ReSay()
        {
            return Colleague.Join();
        }

        protected abstract string Do();
    }

    public class Chat : Mediator
    {
        protected override string Do()
        {
            return "Join chat";
        }
    }

    public class MediatorTest
    {
        public string Test()
        {
            return new ConcreteColleague().Join();
        }
    }
    #endregion
    // Chain Of Responsibility - Allows building a chain of objects to process a request.
    #region Chain Of Responsibility
    public abstract class Handler
    {
        public Handler Next;

        public string Handle()
        {
            string t = "";
            if (Next != null)
                t += Next.DoHandle();

            return t += DoHandle();
        }
        protected abstract string DoHandle();
    }

    public class ConcreteHandlerA : Handler
    {
        protected override string DoHandle()
        {
            return "Handle A";
        }
    }

    public class ConcreteHandlerB : Handler
    {
        protected override string DoHandle()
        {
            return "Handle B";
        }
    }

    public class Sender
    {
        public Handler Handler;

        public string Handle()
        {
            return Handler.Handle();
        }
    }

    public class CORTest
    {
        public string Test()
        {
            string t = "";
            Sender s = new Sender();
            s.Handler = new ConcreteHandlerA();
            s.Handler.Next = new ConcreteHandlerB();
            t += s.Handle();
            return t;
        }
    }
    #endregion
    // Visitor - Allows adding new operations to an object structure without modifying it.
    #region Visitor 
    public interface IVisitor
    {
        string Visit(ConcreteElementA a);
    }

    public class Visitor1 : IVisitor
    {
        public string Visit(ConcreteElementA a)
        {
            return "Visitor 1 " + a.ToString();
        }
    }

    public class Visitor2 : IVisitor
    {
        public string Visit(ConcreteElementA a)
        {
            return "Visitor 2 " + a.ToString();
        }
    }

    public interface IElement
    {
        string Accept(IVisitor visitor);
    }

    public class ConcreteElementA : IElement
    {
        public string Accept(IVisitor visitor)
        {
            return "Element A " + visitor.Visit(this);
        }
    }

    public class VisitorTest
    {
        public string Test()
        {
            string t = "";
            ConcreteElementA a = new ConcreteElementA();
            t += a.Accept(new Visitor1()) + "\n";
            t += a.Accept(new Visitor2());
            return t;
        }
    }
    #endregion
    public class TestBehavioral : MonoBehaviour
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

        public States SetState = States.State1;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            //Memento
            InfoText.text = new TestMemento().Test1();
            Info2Text.text = new TestMemento().Test2();

            //State
            StateContext sc = new StateContext();
            sc.Request(SetState);
            Info3Text.text = sc.Context;

            //Iterator
            TestIterator ti = new TestIterator();
            Info4Text.text = ti.Test1();

            //Strategy
            StrategyTest st = new StrategyTest();
            Info5Text.text = st.Test1();

            //Template
            TemplateTest tt = new TemplateTest();
            Info6Text.text = tt.Test1();

            //Command
            Info7Text.text = new CommandTest().Test1();

            //Observer
            Info8Text.text = new TestObserver().Test();

            //Mediator
            Info9Text.text = new MediatorTest().Test();

            //Chain Of Responsibility
            Info10Text.text = new CORTest().Test();

            //Visitor
            Info11Text.text = new VisitorTest().Test();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}