using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoF.Memento
{
    //-------------------------------------------------VARIANT 1------------------------------

    public class Unit
    {
        private string abilityName;

        public UnitState CreateState()
        {
            return new UnitState(abilityName);
        }

        public void RestoreState(UnitState state)
        {
            abilityName = state.GetAbility();
        }

        public void SetAbility(string abilityName)
        {
            this.abilityName = abilityName;
        }

        public string GetAbility()
        {
            return abilityName;
        }
    }

    public class UnitAbilitiesHistory
    {
        private List<UnitState> abilities = new List<UnitState>();

        public void Add(UnitState u)
        {
            abilities.Add(u);

            for (int i = 0; i < abilities.Count; i++)
            {
                Debug.Log(abilities[i].GetAbility());
            }
        }

        public UnitState Undo()
        {
            if (abilities.Count > 0)
            {
                int lastIndex = abilities.Count - 1;
                UnitState lastState = abilities[lastIndex];
                abilities.Remove(lastState);

                for (int i = 0; i < abilities.Count; i++)
                {
                    Debug.Log(abilities[i].GetAbility());
                }

                return lastState;
            }
            else
            {
                for (int i = 0; i < abilities.Count; i++)
                {
                    Debug.Log(abilities[i].GetAbility());
                }

                return new UnitState("Default");
            }
        }
    }

    public class UnitState
    {
        private string abilityName;

        public UnitState(string abilityName)
        {
            this.abilityName = abilityName;
        }

        public string GetAbility()
        {
            return abilityName;
        }
    }

    //-------------------------------------------------VARIANT 2------------------------------

    public class Document
    {
        private string content;
        private string fontName;
        private int fontSize;

        public string GetContent()
        {
            return content;
        }

        public DocumentState CreateState()
        {
            return new DocumentState(content, fontName, fontSize);
        }

        public void Restore(DocumentState state)
        {
            this.content = state.GetContent();
            this.fontName = state.GetFontName();
            this.fontSize = state.GetFontSize();
        }

        public void SetContent(string content)
        {
            this.content = content;
        }

        public string GetFontName()
        {
            return fontName;
        }

        public void SetFontName(string fontName)
        {
            this.fontName = fontName;
        }

        public int GetFontSize()
        {
            return fontSize;
        }

        public void SetFontSize(int fontSize)
        {
            this.fontSize = fontSize;
        }

        public override string ToString()
        {
            return $"Content: {content}, Font Name: {fontName}, Font Size: {fontSize}";
        }
    }

    public class DocumentState
    {
        private string content;
        private string fontName;
        private int fontSize;

        public DocumentState(string content, string fontName, int fontSize)
        {
            this.content = content;
            this.fontName = fontName;
            this.fontSize = fontSize;
        }

        public string GetContent()
        {
            return content;
        }

        public int GetFontSize()
        {
            return fontSize;
        }

        public string GetFontName()
        {
            return fontName;
        }

        public override string ToString()
        {
            return $"Content: {content}, Font Name: {fontName}, Font Size: {fontSize}";
        }
    }

    public class DocumentHistory
    {
        private Stack<DocumentState> documents = new Stack<DocumentState>();

        public void Add(DocumentState state)
        {
            documents.Push(state);

            foreach (var item in documents)
            {
                Debug.Log(item.ToString());
            }
        }

        public DocumentState Undo()
        {
            if (documents.Count > 0)
            {
                foreach (var item in documents)
                {
                    Debug.Log(item.ToString());
                }

                return documents.Pop();
            }
            else
            {
                return new DocumentState("Default", "Default", 12);
            }
        }
    }

    public class Memento : MonoBehaviour
    {
        Unit u = new Unit();
        UnitAbilitiesHistory unitAbilities = new UnitAbilitiesHistory();
        Document d = new Document();
        DocumentHistory dh = new DocumentHistory();
        public Button BuildButton;
        public Button BuildButton1;
        public Text InfoText;
        public Text InfoText2;

        public string AbilityName;
        public string DocContent;
        public string DocFontName;
        public int DocFontSize;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
            BuildButton1.onClick.AddListener(Build1);
        }

        private void Build()
        {
            u.SetAbility(AbilityName);
            unitAbilities.Add(u.CreateState());
            InfoText.text = u.GetAbility();

            d.SetContent(DocContent);
            d.SetFontName(DocFontName);
            d.SetFontSize(DocFontSize);
            dh.Add(d.CreateState());
            InfoText2.text = d.ToString();
        }

        private void Build1()
        {
            u.RestoreState(unitAbilities.Undo());
            InfoText.text = u.GetAbility();
            d.Restore(dh.Undo());
            InfoText2.text = d.ToString();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
            BuildButton1.onClick.RemoveListener(Build1);
        }
    }
}