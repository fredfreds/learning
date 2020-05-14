using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoF.Template
{
    //--------------------------------------------VARIANT 1-------------------------------

    public abstract class Task
    {
        private AuditTrail auditTrail;

        public Task() 
        {
            auditTrail = new AuditTrail();
        }

        public Task(AuditTrail auditTrail)
        {
            this.auditTrail = auditTrail;
        }

        public string Execute()
        {
            return doExecute() + "/" + auditTrail.Record();
        }

        protected abstract string doExecute();
    }

    public class TransferMoney : Task
    {
        protected override string doExecute()
        {
            return "Transfer Money";
        }
    }

    public class AuditTrail
    {
        public string Record()
        {
            return "Record";
        }
    }

    //--------------------------------------------VARIANT 2-------------------------------

    public abstract class Operation
    {
        private Window window;

        public Operation()
        {
            window = new Window();
        }

        public string Execute()
        {

            return BeforeOperation() + "/" + window.Close() + "/" + AfterOperation();
        }

        protected abstract string BeforeOperation();
        protected abstract string AfterOperation();
    }

    public class CustomOperation : Operation
    {
        protected override string AfterOperation()
        {
            return "After";
        }

        protected override string BeforeOperation()
        {
            return "Before";
        }
    }

    public class Window
    {
        public string Close()
        {
            return "Close";
        }
    }

    //--------------------------------------------VARIANT 3-------------------------------

    public class GUI
    {
        public string Close()
        {
            return OnClosing() + "/" + "Close" + "/" + OnClosed();
        }

        protected virtual string OnClosing() { return ""; }
        protected virtual string OnClosed() { return ""; }
    }

    public class ChatGUI : GUI
    {
        protected override string OnClosing()
        {
            return "Chat Closing";
        }

        protected override string OnClosed()
        {
            return "Chat Closed";
        }
    }

    public class Template : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;

        Task t = new TransferMoney();
        Operation o = new CustomOperation();
        GUI g = new ChatGUI();

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            InfoText.text = t.Execute();
            Info2Text.text = o.Execute();
            Info3Text.text = g.Close();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}