using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoF.State
{
    //-----------------------------------------------------VARIANT 1------------------------

    public enum ToolType
    {
        Selection,
        Brush,
        Eraser
    }

    public class Canvas
    {
        private ITool currentTool;

        public string MouseUp()
        {
            return currentTool.MouseUp();
        }

        public string MouseDown()
        {
            return currentTool.MouseDown();
        }

        public ITool GetCurrentTool()
        {
            return currentTool;
        }

        public void SetCurrentTool(ITool tool)
        {
            this.currentTool = tool;
        }
    }

    public class SelectionTool : ITool
    {
        public string MouseDown()
        {
            return "Selection Tool Pressed";
        }

        public string MouseUp()
        {
            return "Selection Tool Unpressed";
        }
    }

    public class BrushTool : ITool
    {
        public string MouseDown()
        {
            return "Brush Tool Pressed";
        }

        public string MouseUp()
        {
            return "Brush Tool Unpressed";
        }
    }

    public class EraserTool : ITool
    {
        public string MouseDown()
        {
            return "Eraser Tool Pressed";
        }

        public string MouseUp()
        {
            return "Eraser Tool Unpressed";
        }
    }

    public interface ITool
    {
         string MouseUp();
         string MouseDown();
    }

    //-----------------------------------------------------VARIANT 2------------------------

    public enum TravelMode
    {
        Driving,
        Bicycling,
        Transit,
        Walking
    }

    public interface IMode
    {
        string GetDirection();
        string GetEta();
    }

    public class DrivingMode : IMode
    {
        public string GetDirection()
        {
            return "Driving Direction";
        }

        public string GetEta()
        {
            return "Driving Eta";
        }
    }

    public class BicyclingMode : IMode
    {
        public string GetDirection()
        {
            return "Bicycling Direction";
        }

        public string GetEta()
        {
            return "Bicycling Eta";
        }
    }

    public class TransitMode : IMode
    {
        public string GetDirection()
        {
            return "Transit Direction";
        }

        public string GetEta()
        {
            return "Transit Eta";
        }
    }

    public class WalkingMode : IMode
    {
        public string GetDirection()
        {
            return "Walking Direction";
        }

        public string GetEta()
        {
            return "Walking Eta";
        }
    }

    public class Mode
    {
        private IMode mode;

        public string GetDirection()
        {
            return mode.GetDirection();
        }

        public string GetEta()
        {
            return mode.GetEta();
        }

        public void SetMode(IMode mode) 
        {
            this.mode = mode;
        }
    }

    public class State : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;

        Canvas c = new Canvas();
        Mode m = new Mode();

        public TravelMode TravelModes = TravelMode.Walking;
        public ToolType ToolTypes = ToolType.Brush;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            switch (ToolTypes)
            {
                case ToolType.Selection:
                    c.SetCurrentTool(new SelectionTool());
                    break;
                case ToolType.Brush:
                    c.SetCurrentTool(new BrushTool());
                    break;
                case ToolType.Eraser:
                    c.SetCurrentTool(new EraserTool());
                    break;
                default:
                    break;
            }

            InfoText.text = c.MouseDown();
            Info2Text.text = c.MouseUp();

            switch (TravelModes)
            {
                case TravelMode.Driving:
                    m.SetMode(new DrivingMode());
                    break;
                case TravelMode.Bicycling:
                    m.SetMode(new BicyclingMode());
                    break;
                case TravelMode.Transit:
                    m.SetMode(new TransitMode());
                    break;
                case TravelMode.Walking:
                    m.SetMode(new WalkingMode());
                    break;
                default:
                    break;
            }

            Info3Text.text = m.GetDirection();
            Info4Text.text = m.GetEta();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}