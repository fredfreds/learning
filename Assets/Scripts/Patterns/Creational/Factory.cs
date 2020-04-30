using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace Patterns 
{

    public interface IProduct
    {
        string Run();
    }

    public class ProductA : IProduct
    {
        public string Run()
        {
            return "ProductA";
        }
    }

    public class ProductB : IProduct
    {
        public string Run()
        {
            return "ProductB";
        }
    }

    public class Creator
    {
        public virtual IProduct CreateProduct()
        {
            return new ProductA();
        }

        private void OnInstall(IProduct product)
        {
            if(!Factory.Products.Contains(product))
            Factory.Products.Add(product);
        }

        public IProduct Install()
        {
            IProduct product = CreateProduct();
            OnInstall(product);
            return product;
        }
    }

    public class CreatorA : Creator
    {
        public override IProduct CreateProduct()
        {
            return new ProductA();
        }
    }

    public class CreatorB : Creator
    {
        public override IProduct CreateProduct()
        {
            return new ProductB();
        }
    }

    public enum CreatorType
    {
        CreatorA,
        CreatorB
    }

    public class Factory : MonoBehaviour
    {
        public static List<IProduct> Products = new List<IProduct>();
        public List<string> ProductsVisible = new List<string>();

        public Text Text;
        public Text Text2;
        public Button RunButton;

        CreatorA CA = new CreatorA();
        CreatorB CB = new CreatorB();

        public CreatorType Type = CreatorType.CreatorA;

        private void OnEnable()
        {
            RunButton.onClick.AddListener(Create);
        }

        private void OnDisable()
        {
            RunButton.onClick.RemoveListener(Create);
        }

        private void Visible(string item)
        {
            if (!ProductsVisible.Contains(item))
            {
                ProductsVisible.Add(item);
                Text2.text += item;
            }

            foreach (var VARIABLE in Products)
            {
                Debug.Log(VARIABLE.Run());
            }
        }

        public void Create()
        {
            switch (Type)
            {
                case CreatorType.CreatorA:
                    Text.text = CA.CreateProduct().Run();
                    Visible(CA.CreateProduct().Run());
                    break;
                case CreatorType.CreatorB:
                    Text.text = CB.CreateProduct().Run();
                    Visible(CB.CreateProduct().Run());
                    break;
                default:
                    Text.text = CA.CreateProduct().Run();
                    Visible(CB.CreateProduct().Run());
                    break;
            }
        }
    }
}