using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SOLID
{
    public enum TestEnum
    {
        Small, Medium, Large
    }

    public class TestClass
    {
        public string Name;
        public TestEnum testEnum;

        public TestClass(string name, TestEnum testEnum)
        {
            Name = name;
            this.testEnum = testEnum;
        }
    }

    public interface ITestSpec
    {
        bool IsCorrect(TestClass t);
    }

    public interface ITestFilter
    {
        List<TestClass> Filter(List<TestClass> items, TestSpec spec);
    }
    
    public class TestSpec : ITestSpec
    {
        private TestEnum _enum;

        public TestSpec(TestEnum @enum)
        {
            _enum = @enum;
        }
        
        public bool IsCorrect(TestClass t)
        {
            return t.testEnum == _enum;
        }
    }

    public class TestFilter : ITestFilter
    {
        public List<TestClass> Filter(List<TestClass> items, TestSpec spec)
        {
            List<TestClass> itms = new List<TestClass>();
            
            for (int i = 0; i < items.Count; i++)
            {
                if (spec.IsCorrect(items[i]))
                {
                    itms.Add(items[i]);
                }
            }
            
            return itms;
        }
    }

    public enum Enumerator
    {
        Small, Medium, Large
    }

    public interface ISpec<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilt<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpec<T> parameters);
    }

    public class TestOCP
    {
        public string Name;
        public Enumerator enumerator;
        
        public TestOCP(string n, Enumerator e)
        {
            Name = n;
            enumerator = e;
        }
    }

    public class EnumSpec : ISpec<TestOCP>
    {
        public Enumerator enumerator;

        public EnumSpec(Enumerator e)
        {
            this.enumerator = e;
        }
        
        public bool IsSatisfied(TestOCP t)
        {
            return t.enumerator == enumerator;
        }
    }

    public class EnumFilter : IFilt<TestOCP>
    {
        public IEnumerable<TestOCP> Filter(IEnumerable<TestOCP> items, ISpec<TestOCP> parameters)
        {
            foreach (var item in items)
            {
                if (parameters.IsSatisfied(item))
                {
                    yield return item;
                }
            }
        }
    }
    
    public enum EnemySize
    {
        Small, Medium, Large
    }

    public enum EnemyColor
    {
        Red, Green, Blue
    }

    public class Enemy
    {
        public string Name;
        public EnemySize Size;
        public EnemyColor Color;

        public Enemy(string name, EnemySize size, EnemyColor color)
        {
            if (name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }

            Name = name;
            Size = size;
            Color = color;
        }
    }

    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification : ISpecification<Enemy> 
    {
        private EnemyColor color;

        public ColorSpecification(EnemyColor color)
        {
            this.color = color;
        }

        public bool IsSatisfied(Enemy e)
        {
            return e.Color == color;
        }
    }

    public class SizeSpecification : ISpecification<Enemy>
    {
        private EnemySize size;

        public SizeSpecification(EnemySize size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Enemy e)
        {
            return e.Size == size;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this.first = first ?? throw new ArgumentNullException(paramName: nameof(first));
            this.second = second ?? throw new ArgumentNullException(paramName: nameof(second));
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t); 
        }
    }

    public class EnemyFilter : IFilter<Enemy>
    {
        public IEnumerable<Enemy> Filter(IEnumerable<Enemy> items, ISpecification<Enemy> spec)
        {
            foreach (var item in items)
            {
                if(spec.IsSatisfied(item))
                {
                    yield return item;
                }
            }
        }
    }

    public class OpenClosedPrinciple : MonoBehaviour
    {
        Enemy Groot = new Enemy("Groot", EnemySize.Large, EnemyColor.Blue);
        Enemy Gret = new Enemy("Gret", EnemySize.Large, EnemyColor.Green);
        Enemy Gret2 = new Enemy("Gret 2", EnemySize.Small, EnemyColor.Green);
        
        TestOCP _testOcp = new TestOCP("Test", Enumerator.Small);
        EnumFilter _filter = new EnumFilter();
        TestOCP[] tests = new TestOCP[1];
        
        TestClass tc = new TestClass("TestClass", TestEnum.Small);
        TestFilter tf = new TestFilter();
        List<TestClass> tcs = new List<TestClass>();
        
        Enemy[] enemies = new Enemy[3];

        EnemyFilter enemyFilter = new EnemyFilter();

        public Button FilterBySizeButton;
        public Button FilterByColorButton;
        public Button AndFilterButton;
        public Text InfoTextInit;
        public Text InfoText;
        public Dropdown FilterSizeDropdown;
        public Dropdown FilterColorDropdown;

        private void OnEnable()
        {
            FilterBySizeButton.onClick.AddListener(FilterBySize);
            FilterByColorButton.onClick.AddListener(FilterByColor);
            AndFilterButton.onClick.AddListener(AndFilter);
        }

        private void Start()
        {
            tcs.Add(tc);
            tests[0] = _testOcp;
            enemies[0] = Groot;
            enemies[1] = Gret;
            enemies[2] = Gret2;

            string text = "";

            for (int i = 0; i < enemies.Length; i++)
            {
                text += enemies[i].Name + " " + enemies[i].Size 
                    + " " + enemies[i].Color + "\n";
            }

            InfoTextInit.text = text;
        }

        private void AndFilter()
        {
            InfoText.text = "";

            foreach (var item in enemyFilter.Filter(enemies,
                new AndSpecification<Enemy>(
                new ColorSpecification((EnemyColor)FilterColorDropdown.value),
                new SizeSpecification((EnemySize)FilterSizeDropdown.value))))
            {
                InfoText.text += $" - {item.Name} is {FilterColorDropdown.options[FilterColorDropdown.value].text} " +
                    $"and {FilterSizeDropdown.options[FilterSizeDropdown.value].text}\n";
            }
        }

        private void FilterByColor()
        {
            InfoText.text = "";

            foreach (var item in enemyFilter.Filter(enemies, 
                new ColorSpecification((EnemyColor)FilterColorDropdown.value)))
            {
                InfoText.text += $" - {item.Name} is {FilterColorDropdown.options[FilterColorDropdown.value].text}\n";
            }
        }

        private void FilterBySize()
        {
            InfoText.text = "";

            for (int i = 0; i < tf.Filter(tcs, new TestSpec((TestEnum) FilterSizeDropdown.value)).Count; i++)
            {
                InfoText.text += $" - {tf.Filter(tcs, new TestSpec((TestEnum) FilterSizeDropdown.value))[i].Name} is {FilterColorDropdown.options[FilterColorDropdown.value].text}\n";
            }
            
            
            
            /*foreach (var item in _filter.Filter(tests,
                new EnumSpec((Enumerator)FilterSizeDropdown.value)))
            {
                InfoText.text += $" - {item.Name} is {FilterSizeDropdown.options[FilterSizeDropdown.value].text}\n";
            }*/
        }

        private void OnDisable()
        {
            FilterBySizeButton.onClick.RemoveListener(FilterBySize);
            FilterByColorButton.onClick.RemoveListener(FilterByColor);
        }
    }
}