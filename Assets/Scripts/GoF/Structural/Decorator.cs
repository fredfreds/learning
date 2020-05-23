using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoF.Decorator
{
    //-----------------------------------------VARIANT 1------------------

    public interface IStream
    {
        string Write(string data);
    }

    public class CloudStream : IStream
    {
        public virtual string Write(string data)
        {
            return "Write " + data;
        }
    }

    public class CompressedStream : IStream
    {
        private IStream stream;

        public CompressedStream(IStream stream)
        {
            this.stream = stream;
        }

        public string Write(string data)
        {
            string c = Compress(data);
            return stream.Write(c);
        }

        public string Compress(string data)
        {
            return "Compressed " + data;
        }
    }

    public class EncryptedCloudStream : IStream
    {
        private IStream stream;

        public EncryptedCloudStream(IStream stream)
        {
            this.stream = stream;
        }

        public string Write(string data)
        {
            string encrypted = Encrypt(data);
            return stream.Write(encrypted);
        }

        private string Encrypt(string data)
        {
            return "234325";
        }
    }

    //-----------------------------------------VARIANT 2------------------

    public interface IDecorator
    {
        string Render();
    }

    public class ErrorDecorator : IDecorator
    {
        private IDecorator decorator;

        public ErrorDecorator(IDecorator decorator)
        {
            this.decorator = decorator;
        }

        public string Render()
        {
            return "Error " + decorator.Render(); 
        }
    }

    public class MainDecorator : IDecorator
    {
        private IDecorator decorator;

        public MainDecorator(IDecorator decorator)
        {
            this.decorator = decorator;
        }

        public string Render()
        {
            return "Main " + decorator.Render();
        }
    }

    public class Artefact : IDecorator
    {
        public string Render()
        {
            return "Window";
        }
    }

    public class Editor
    {
        public string OpenProject()
        {
            IDecorator[] decorators =
            {
                new Artefact(),
                new Artefact(),
                new Artefact()
            };

            decorators[0] = new MainDecorator(decorators[0]);
            decorators[1] = new ErrorDecorator(new MainDecorator(decorators[1]));
            decorators[2] = new ErrorDecorator(decorators[2]);

            string t = "";

            foreach (var item in decorators)
            {
                t += item.Render() + "\n";
            }

            return t;
        }
    }

    public class Decorator : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;

        IStream s1;
        IStream s2;
        IStream s3;

        Editor e = new Editor();

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            s1 = new CloudStream();
            s2 = new CompressedStream(s1);
            s3 = new EncryptedCloudStream(s2);
            InfoText.text = s3.Write("test");

            Info2Text.text = e.OpenProject();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}