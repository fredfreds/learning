using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoF.Strategy
{
    //--------------------------------------------VARIANT 1-------------------------------

    public interface ICompressor
    {
        string Compress(string fileName);
    }

    public class JpegCompressor : ICompressor
    {
        public string Compress(string fileName)
        {
            return $"{fileName} compressed by Jpeg Compressor";
        }
    }

    public class PngCompressor : ICompressor
    {
        public string Compress(string fileName)
        {
            return $"{fileName} compressed by Png Compressor";
        }
    }

    public interface IFilter
    {
        string Apply(string fileName);
    }

    public class BlackAndWhiteFilter : IFilter
    {
        public string Apply(string fileName)
        {
            return $"{fileName} filtered by Black And White Filter";
        }
    }

    public class ColorFilter : IFilter
    {
        public string Apply(string fileName)
        {
            return $"{fileName} filtered by Color Filter";
        }
    }

    public class ImageStorage
    {
        public string Store(string fileName, ICompressor compressor, IFilter filter)
        {
            return $"{compressor.Compress(fileName)} + {filter.Apply(fileName)}";
        }
    }

    //--------------------------------------------VARIANT 2-------------------------------

    public interface IEncryption
    {
        string Encrypt(string msg);
    }

    public enum EncryptionType
    {
        DES,
        AES
    }

    public class DES : IEncryption
    {
        public string Encrypt(string msg)
        {
            return $"{msg} encrypted with DES";
        }
    }

    public class AES : IEncryption
    {
        public string Encrypt(string msg)
        {
            return $"{msg} encrypted with AES";
        }
    }

    public class ChatClient
    {
        public string Send(string msg, EncryptionType encryptionType)
        {
            switch (encryptionType)
            {
                case EncryptionType.DES:
                    return new DES().Encrypt(msg);
                case EncryptionType.AES:
                    return new AES().Encrypt(msg);
            }

            return "none";
        }
    }

    public class Strategy : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;

        ImageStorage im = new ImageStorage();
        ChatClient c = new ChatClient();
        public EncryptionType Encryptor = EncryptionType.AES;

        public string Msg;
        public string FileName;
        public string FileName2;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            InfoText.text = im.Store(FileName, new JpegCompressor(), new ColorFilter());
            Info2Text.text = im.Store(FileName2, new PngCompressor(), new BlackAndWhiteFilter());

            Info3Text.text = c.Send(Msg, Encryptor);
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}