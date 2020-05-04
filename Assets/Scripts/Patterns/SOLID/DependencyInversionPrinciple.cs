using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SOLID
{   
    public enum Relationship
    {
        Good,
        Bad,
        Neutral
    }

    public class Unit
    {
        public string UnitName; 
    }

    public interface IRelationship
    {
        IEnumerable<Unit> FindRelationOf(string n, Relationship r);
        void Clear();
    }

    public class Relationships : IRelationship
    {
        private List<(Unit, Relationship, Unit)> relations = new List<(Unit, Relationship, Unit)>();
        
        public void AddRelationship(Unit p, Relationship r1, Relationship r2, Unit c)
        {
            relations.Add((p, r1, c));
            relations.Add((c, r2, p));
        }

        public IEnumerable<Unit> FindRelationOf(string n, Relationship r)
        {
            return from b in relations.Where(
                             x => x.Item1.UnitName == n &&
                             x.Item2 == r
                             )
                   select b.Item3;
        }

        public void Clear()
        {
            relations.Clear();
        }

        //public List<(Unit, Relationship, Unit)> Relations => relations;
    }

    public class FindRelationships
    {
        //public FindRelationships(Relationships relationships)
        //{
        //    List<(Unit, Relationship, Unit)> relations = relationships.Relations;

        //    foreach (var r in relations.Where(
        //        x => x.Item1.UnitName == "U1" &&
        //        x.Item2 == Relationship.Good
        //        ))
        //    {

        //    }
        //}

        private string units;

        public FindRelationships(IRelationship relationship, string n, Relationship r)
        {
            units = r.ToString() + "\n";

            foreach (var b in relationship.FindRelationOf(n, r))
            {
                units += b.UnitName + "/";
            }
        }

        public string GetRelationship()
        {
            return units;
        }

        public string ClearRelationship()
        {
            units = "";
            return units;
        }
    }

    public class DependencyInversionPrinciple : MonoBehaviour
    {
        Unit U1 = new Unit { UnitName = "U1" };
        Unit U2 = new Unit { UnitName = "U2" };
        Unit U3 = new Unit { UnitName = "U3" };

        Relationships relationships = new Relationships();

        FindRelationships findRelationships;

        public Text InfoText;
        public Button BuildButton;

        public Relationship R1;
        public Relationship R2;

        public Relationship R;

        public string N;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            relationships.AddRelationship(U1, R1, R2, U2);
            relationships.AddRelationship(U1, R1, R2, U3);

            findRelationships = new FindRelationships(relationships, N, R);
            InfoText.text = findRelationships.GetRelationship();

            relationships.Clear();
            findRelationships.ClearRelationship();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}