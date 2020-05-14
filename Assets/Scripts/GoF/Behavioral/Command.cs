using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoF.Command
{
    //--------------------------------------------VARIANT 1-------------------------------

    public interface ICommand
    {
        string Execute();
    }

    public class UIButton
    {
        private string label;
        private ICommand command;
        public string Label { get => label; set => label = value; }

        public UIButton(ICommand command)
        {
            this.command = command;
        }

        public string Click()
        {
            return command.Execute();
        }
    }

    public class CustomerService
    {
        public string AddCustomer()
        {
            return "Add customer";
        }
    }

    public class UserService
    {
        public string AddUser()
        {
            return "Add User";
        }
    }

    public class AddCustomerService : ICommand
    {
        private CustomerService service;
        public AddCustomerService(CustomerService service)
        {
            this.service = service;
        }

        public string Execute()
        {
            return service.AddCustomer();
        }
    }

    public class AddUserService : ICommand
    {
        private UserService service;
        public AddUserService(UserService service)
        {
            this.service = service;
        }

        public string Execute()
        {
            return service.AddUser();
        }
    }

    public class CompositeCommand : ICommand
    {
        private List<ICommand> commands = new List<ICommand>();

        public void Add(ICommand command)
        {
            commands.Add(command);
        }

        public string Execute()
        {
            string t = "";

            foreach (var item in commands)
            {
                t += item.Execute() + "\n";
            }

            return t;
        }
    }

    //--------------------------------------------VARIANT 2-------------------------------
    public class Document
    {
        private string content;
        public string Content { get => content; set => content = value; }

        public string MakeBold()
        {
            content = $"<b> {content} </b>";
            return content;
        }
    }

    public interface IUndo : ICommand
    {
        string Unexecute();
    }

    public class BoldCommand : IUndo
    {
        private string prevContent;
        private Document document;
        private History history;

        public BoldCommand(Document document, History history)
        {
            this.document = document;
            this.history = history;
        }

        public string Execute()
        {
            prevContent = document.Content;
            string t = document.MakeBold();
            history.Add(this);
            return t;
        }

        public string Unexecute()
        {
            document.Content = prevContent;
            return document.Content;
        }
    }

    public class UndoCommand : ICommand
    {
        private History history;

        public UndoCommand(History history)
        {
            this.history = history;
        }

        public string Execute()
        {
            if (history.Size() > 0)
                return history.Remove().Unexecute();
            else
                return "None";
        }
    }

    public class History
    {
        private Stack<IUndo> commands = new Stack<IUndo>();

        public void Add(IUndo command)
        {
            commands.Push(command);
        }

        public IUndo Remove()
        {
            return commands.Pop();
        }

        public int Size()
        {
            return commands.Count;
        }
    }

    //--------------------------------------------VARIANT 3-------------------------------

    public class VideoEditor
    {
        private float contract;
        private string text;
        public float Contract { get => contract; set => contract = value; }
        public string Text { get => text; set => text = value; }

        public void RemoveText()
        {
            text = "";
        }

        public override string ToString()
        {
            return $"Video Editor: {contract}, {text}";
        }
    }

    public interface IEditorCommand
    {
        void Execute();
    }

    public interface IEditorUndoCommand : IEditorCommand
    {
        void Unexecute();
    }

    public abstract class AbstractUndoCommand : IEditorUndoCommand
    {
        protected VideoEditor editor;
        protected EditorHistory history;

        protected AbstractUndoCommand(VideoEditor editor, EditorHistory history)
        {
            this.editor = editor;
            this.history = history;
        }

        public void Execute()
        {
            DoExecute();

            history.Add(this);
        }

        public virtual void Unexecute()
        {
        }

        protected abstract void DoExecute();
    }

    public class SetContrastCommand : AbstractUndoCommand
    {
        private float contrast;
        private float prevContrast;

        public SetContrastCommand(float contrast, VideoEditor editor, EditorHistory history) : base(editor, history)
        {
            prevContrast = editor.Contract;
            this.contrast = contrast;
        }

        protected override void DoExecute()
        {
            editor.Contract = contrast;
        }

        public override void Unexecute()
        {
            editor.Contract = prevContrast;
        }
    }

    public class SetTextCommand : AbstractUndoCommand
    {
        private string text;

        public SetTextCommand(string text, VideoEditor editor, EditorHistory history) : base(editor, history)
        {
            this.text = text;
        }

        protected override void DoExecute()
        {
            editor.Text = text;
        }

        public override void Unexecute()
        {
            editor.RemoveText();
        }
    }

    public class UndoEditorCommand : IEditorCommand
    {
        private EditorHistory history;

        public UndoEditorCommand(EditorHistory history)
        {
            this.history = history;
        }

        public void Execute()
        {
            if(history.HistorySize() > 0)
            {
                history.Remove().Unexecute();
            }
        }
    }

    public class EditorHistory
    {
        private Stack<IEditorUndoCommand> commands = new Stack<IEditorUndoCommand>();

        public void Add(IEditorUndoCommand command)
        {
            commands.Push(command);
        }

        public IEditorUndoCommand Remove()
        {
            return commands.Pop();
        }

        public int HistorySize()
        {
            return commands.Count;
        }
    }

    public class Command : MonoBehaviour
    {
        public Button BuildButton;
        public Button BuildButton1;
        public Button BuildButton2;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;
        public Text Info5Text;
        public Text Info6Text;

        CustomerService service = new CustomerService();
        UserService uService = new UserService();
        ICommand s;
        ICommand u;
        ICommand uc;
        IUndo bc;

        History h = new History();
        Document d = new Document();
        public string C;
        public int Co;
        public int HS;
        public int Contrast;
        public string Text;

        UIButton b;
        CompositeCommand c = new CompositeCommand();

        VideoEditor editor = new VideoEditor();
        EditorHistory history = new EditorHistory();
        SetContrastCommand u1;
        SetTextCommand u2;
        UndoEditorCommand u3;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
            BuildButton1.onClick.AddListener(Build1);
            BuildButton2.onClick.AddListener(Build2);
        }

        private void Build()
        {
            s = new AddCustomerService(service);
            b = new UIButton(s);
            InfoText.text = b.Click();

            c.Add(s);
            u = new AddUserService(uService);
            c.Add(u);
            Info2Text.text = c.Execute();

            d.Content = C;
            bc = new BoldCommand(d, h);
            Info3Text.text = bc.Execute();
            //Info4Text.text = bc.Unexecute();
            Co = h.Size();

            u1 = new SetContrastCommand(Contrast, editor, history);
            u1.Execute();
            Info5Text.text = editor.ToString();
            HS = history.HistorySize();
        }

        private void Build1()
        {
            uc = new UndoCommand(h);
            Info4Text.text = uc.Execute();
            Co = h.Size();

            u3 = new UndoEditorCommand(history);
            u3.Execute();
            Info5Text.text = editor.ToString();
            Info6Text.text = editor.ToString();
            HS = history.HistorySize();
        }

        private void Build2()
        {
            u2 = new SetTextCommand(Text, editor, history);
            u2.Execute();
            Info6Text.text = editor.ToString();
            HS = history.HistorySize();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
            BuildButton1.onClick.RemoveListener(Build1);
            BuildButton2.onClick.RemoveListener(Build2);
        }
    }
}