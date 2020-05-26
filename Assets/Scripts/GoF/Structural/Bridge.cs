using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoF.Bridge
{
    public interface IDevice
    {
        string TurnOn();
        string TurnOff();
        string SetChannel(int num);
    }

    public class SonyTV : IDevice
    {
        public string SetChannel(int num)
        {
            return "Sony Channel = " + num.ToString();
        }

        public string TurnOff()
        {
            return "Turn Off Sony";
        }

        public string TurnOn()
        {
            return "Turn On Sony";
        }
    }

    public class SamsungTV : IDevice
    {
        public string SetChannel(int num)
        {
            return "Samsung Channel = " + num.ToString();
        }

        public string TurnOff()
        {
            return "Turn Off Samsung";
        }

        public string TurnOn()
        {
            return "Turn On Samsung";
        }
    }

    public class RemoteControl
    {
        protected IDevice device;

        public RemoteControl(IDevice device)
        {
            this.device = device;
        }

        public string TurnOn()
        {
            return device.TurnOn();
        }

        public string TurnOff()
        {
            return device.TurnOff();
        }
    }

    public class AdvancedRemoteControl : RemoteControl
    {
        public AdvancedRemoteControl(IDevice device) : base(device)
        {
        }

        public string SetChannel(int num)
        {
            return device.SetChannel(num);
        }
    }

    public class Bridge : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;

        RemoteControl control;
        AdvancedRemoteControl control2;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            control = new AdvancedRemoteControl(new SonyTV());
            InfoText.text = control.TurnOn();
            control2 = new AdvancedRemoteControl(new SamsungTV());
            Info2Text.text = control2.SetChannel(5);
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}