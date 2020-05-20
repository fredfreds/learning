using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoF.Visitor
{
    //-------------------------------------------------VARIANT 1------------------------

    public interface IHtmlNode
    {
        string Execute(IOperation operation);
    }

    public interface IOperation
    {
        string Apply(HeadingNode heading);
        string Apply(AnchorNode anchoring);
    }

    public class HighlightOperation : IOperation
    {
        public string Apply(HeadingNode heading)
        {
            return $"highlight {heading}";
        }

        public string Apply(AnchorNode anchoring)
        {
            return $"highlight {anchoring}";
        }
    }

    public class PlainTextOperation : IOperation
    {
        public string Apply(HeadingNode heading)
        {
            return $"text {heading}";
        }

        public string Apply(AnchorNode anchoring)
        {
            return $"text {anchoring}";
        }
    }

    public class AnchorNode : IHtmlNode
    {
        public string Execute(IOperation operation)
        {
            return $"Anchor {operation.Apply(this)}";
        }
    }

    public class HeadingNode : IHtmlNode
    {
        public string Execute(IOperation operation)
        {
            return $"Heading {operation.Apply(this)}";
        }
    }

    public class HtmlDocument
    {
        private List<IHtmlNode> nodes = new List<IHtmlNode>();

        public void Add(IHtmlNode node)
        {
            nodes.Add(node);
        }

        public string Execute(IOperation operation)
        {
            string t = "";

            foreach (var item in nodes)
            {
                t += item.Execute(operation);
            }

            return t;
        }
    }

    //-------------------------------------------------VARIANT 2------------------------

    public abstract class ISegment
    {
        public abstract string Execute(IFilter operation);
    }

    public interface IFilter
    {
        string Apply(FactSegment segment);
        string Apply(FormatSegment segment);
    }

    public class FormatSegment : ISegment
    {
        public override string Execute(IFilter operation)
        {
            return operation.Apply(this);
        }
    }

    public class FactSegment : ISegment
    {
        public override string Execute(IFilter operation)
        {
            return operation.Apply(this);
        }
    }

    public class NoiseFilter : IFilter
    {
        public string Apply(FactSegment segment)
        {
            return "Noise to " + segment.ToString();
        }

        public string Apply(FormatSegment segment)
        {
            return "Noise to " + segment.ToString(); 
        }
    }

    public class ReverbFilter : IFilter
    {
        public string Apply(FactSegment segment)
        {
            return "Reverb to " + segment.ToString();
        }

        public string Apply(FormatSegment segment)
        {
            return "Reverb to " + segment.ToString();
        }
    }

    public class NormalizeFilter : IFilter
    {
        public string Apply(FactSegment segment)
        {
            return "Normalize to " + segment.ToString();
        }

        public string Apply(FormatSegment segment)
        {
            return "Normalize to " + segment.ToString();
        }
    }

    public class WavFile
    {
        private List<ISegment> segments = new List<ISegment>();
        public string Status;
        public void Add(ISegment segment)
        {
            segments.Add(segment);
        }

        public void ApplyFilter(IFilter filter)
        {
            foreach (var item in segments)
            {
                Status += item.Execute(filter) + "\n";
            }
        }
    }

    public class Visitor : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;
        public Text Info5Text;
        public Text Info6Text;
        public Text Info7Text;

        WavFile wav = new WavFile();

        HtmlDocument doc = new HtmlDocument();

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            string t = "";
            doc.Add(new AnchorNode());
            t += doc.Execute(new HighlightOperation());
            doc.Add(new HeadingNode());
            t += doc.Execute(new HighlightOperation());

            InfoText.text = t;

            Info2Text.text = doc.Execute(new PlainTextOperation());

            wav.Add(new FormatSegment());
            wav.Add(new FactSegment());
            wav.ApplyFilter(new NoiseFilter());
            wav.ApplyFilter(new NormalizeFilter());
            wav.ApplyFilter(new ReverbFilter());
            Info3Text.text = wav.Status;

            wav = new WavFile();
            wav.Add(new FormatSegment());
            wav.ApplyFilter(new NoiseFilter());
            Info4Text.text = wav.Status;

            wav = new WavFile();
            wav.Add(new FactSegment());
            wav.ApplyFilter(new NormalizeFilter());
            Info5Text.text = wav.Status;

            wav = new WavFile();
            wav.Add(new FormatSegment());
            wav.ApplyFilter(new NormalizeFilter());
            wav.ApplyFilter(new ReverbFilter());
            wav.ApplyFilter(new NoiseFilter());
            Info6Text.text = wav.Status;

            wav = new WavFile();
            wav.Add(new FactSegment());
            wav.ApplyFilter(new NormalizeFilter());
            wav.ApplyFilter(new ReverbFilter());
            wav.ApplyFilter(new NoiseFilter());
            wav.ApplyFilter(new NoiseFilter());
            Info7Text.text = wav.Status;
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}