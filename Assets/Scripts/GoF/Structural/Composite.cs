using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoF.Composite
{
    //-------------------------------------VARIANT 1-----------------------

    public interface IComponent
    {
        string Render();
    }

    public class Shape : IComponent
    {
        public string Render()
        {
            return $"Render {GetType().FullName}";
        }
    }

    public class Group : IComponent
    {
        private List<IComponent> components = new List<IComponent>();

        public void Add(IComponent component)
        {
            components.Add(component);
        }

        public string Render()
        {
            string t = "";
            foreach (var item in components)
            {
                t += item.Render();
            }
            return t;
        }
    }

    //-------------------------------------VARIANT 2-----------------------

    public abstract class Resource
    {
        public abstract string Deploy();
    }

    public class HumanResource : Resource
    {
        public override string Deploy()
        {
            return "Human Resource";
        }
    }

    public class Truck : Resource
    {
        public override string Deploy()
        {
            return "Track";
        }
    }

    public class Team : Resource
    {
        private List<Resource> resources = new List<Resource>();

        public void Add(Resource resource)
        {
            resources.Add(resource);
        }

        public override string Deploy()
        {
            string t = "";
            foreach (var item in resources)
            {
                t += item.Deploy();
            }
            return t;
        }
    }

    public class Composite : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;

        IComponent sh1;
        IComponent sh2;
        IComponent c1;
        IComponent c2;
        Group g1;
        Group g2;
        Group g;

        Resource h1 = new HumanResource();
        Resource h2 = new HumanResource();
        Resource t1 = new Truck();
        Resource t2 = new Truck();
        Team team1 = new Team();
        Team team2 = new Team();
        Team team = new Team();

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            sh1 = new Shape();
            sh2 = new Shape();
            c1 = new Shape();
            c2 = new Shape();
            g1 = new Group();
            g2 = new Group();
            g = new Group();
            g1.Add(sh1);
            g1.Add(sh2);
            g2.Add(c1);
            g2.Add(c1);
            g.Add(g1);
            g.Add(g2);

            InfoText.text = g.Render();

            team1.Add(h1);
            team1.Add(t1);
            team2.Add(h2);
            team2.Add(t2);
            team.Add(team1);
            team.Add(team2);

            Info2Text.text = team.Deploy();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}