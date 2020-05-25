using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace GoF.Flyweight
{
    //-----------------------------------------VARIANT 1------------------

    public class Point
    {
        private  int x;
        private int y;
        private PointIcon pi;

        public Point(int x, int y, PointIcon pi)
        {
            this.x = x;
            this.y = y;
            this.pi = pi;
        }

        public string Draw()
        {
            return $"{pi.Type} at ({x}, {y})";
        }
    }

    public class PointIcon
    {
        private PointType type;
        private byte[] icon;

        public PointIcon(PointType type, byte[] icon)
        {
            this.Type = type;
            this.icon = icon;
        }

        public PointType Type { get => type; set => type = value; }
    }

    public class PointIconFactory
    {
        private Dictionary<PointType, PointIcon> icons = new Dictionary<PointType, PointIcon>(); 

        public PointIcon GetPointIcon(PointType pt)
        {
            if(!icons.ContainsKey(pt))
            {
                PointIcon ic = new PointIcon(pt, null);
                icons.Add(pt, ic);
            }

            return icons[pt];
        }
    }

    public class PointService
    {
        private PointIconFactory iconFactory;

        public PointService(PointIconFactory iconFactory)
        {
            this.iconFactory = iconFactory;
        }

        public List<Point> GetPoints()
        {
            List<Point> points = new List<Point>();
            Point p1 = new Point(1, 2, iconFactory.GetPointIcon(PointType.CAFE));
            points.Add(p1);
            return points;
        }
    }

    public enum PointType
    {
        HOSPITAL,
        CAFE,
        RESTAURANT
    }

    //-----------------------------------------VARIANT 2------------------

    public class Cell
    {
        private int row;
        private int column;
        private string content;
        private Font font;

        public Cell(int row, int column, Font f)
        {
            this.row = row;
            this.column = column;
            this.font = f;
        }

        public string Content { get => content; set => content = value; }
        public Font Font { get => font; set => font = value; }

        public string Render()
        {
            return $"({row}, {column}): {content}, {Font.FontFamily}";
        }
    }

    public class Font
    {
        private string fontFamily;
        private string fontSize;
        private string isBold;

        public Font(string fontFamily, string fontSize, string isBold)
        {
            this.FontFamily = fontFamily;
            this.FontSize = fontSize;
            this.IsBold = isBold;
        }

        public string FontFamily { get => fontFamily; set => fontFamily = value; }
        public string FontSize { get => fontSize; set => fontSize = value; }
        public string IsBold { get => isBold; set => isBold = value; }

        public override int GetHashCode()
        {
            return Tuple.Create(fontFamily, fontSize, isBold).GetHashCode();
        }
    }

    public class SpreatSheet
    {
        private int maxRows = 100;
        private int maxCols = 100;

        private Cell[,] cells;
        private FontFactory ff;

        public SpreatSheet(FontFactory ff)
        {
            this.ff = ff;

            GenerateCells();
        }

        public void SetContent(int r, int c, string t)
        {
            EnsureCellExists(r, c);

            cells[r, c].Content = t;
        }

        public void SetFontFamily(int r, int c, string fontFamily)
        {
            EnsureCellExists(r, c);
            Cell cell = cells[r, c];
            Font cf = cell.Font;
            Font cont = ff.GetContext(fontFamily, cf.FontSize, cf.IsBold);
            cells[r, c].Font = cont;
        }

        private void EnsureCellExists(int r, int c)
        {
            if(r < 0 || r >= maxRows)
            {

            }
            if (c < 0 || c >= maxRows)
            {

            }
        }

        private void GenerateCells()
        {
            cells = new Cell[maxRows, maxCols];

            for (int i = 0; i < maxRows; i++)
            {
                for (int j = 0; j < maxCols; j++)
                {
                    Cell c = new Cell(i, j, GetFont());
                    cells[i, j] = c;
                }
            }
        }

        private Font GetFont()
        {
            return new Font("Times", "12", "False");
        }

        public string Render()
        {
            string t = "";
            for (int i = 0; i < maxRows; i++)
            {
                for (int j = 0; j < maxCols; j++)
                {
                    t += cells[i, j].Render() + "\n";
                }
            }
            return t;
        }
    }

    public class FontFactory
    {
        private Dictionary<int, Font> fonts = new Dictionary<int, Font>();

        public Font GetContext(string fontFamily, string fontSize, string isBold)
        {
            //Font f = new Font(fontFamily, fontSize, isBold);
            //int hash = f.GetHashCode();
            int hash = Tuple.Create(fontFamily, fontSize, isBold).GetHashCode();

            if(!fonts.ContainsKey(hash))
            {
                //fonts.Add(hash, f);
                fonts.Add(hash, new Font(fontFamily, fontSize, isBold));
            }
            return fonts[hash];
        }
    }

    public class Flyweight : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;
        public Text Info5Text;

        PointService ps;
        FontFactory ff = new FontFactory();
        SpreatSheet ss;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            ps = new PointService(new PointIconFactory());
            foreach (var item in ps.GetPoints())
            {
                InfoText.text += item.Draw() + "\n";
            }

            //A: Setup and stuff you don't want timed
            var timer = new Stopwatch();
            timer.Start();

            //B: Run stuff you want timed
            

            ss = new SpreatSheet(ff);
            Info2Text.text = ss.Render();

            ss.SetContent(2, 2, "Test2");
            Info3Text.text = ss.Render();

            ss.SetFontFamily(1, 2, "Arial");
            ss.SetFontFamily(2, 1, "Arial2");
            Info4Text.text = ss.Render();

            ss.SetFontFamily(1, 1, "Sans");
            ss.SetFontFamily(0, 2, "Sans");
            ss.SetFontFamily(0, 0, "Sans");
            ss.SetFontFamily(2, 0, "Sans");
            Info5Text.text = ss.Render();

            ss.SetFontFamily(1, 0, "Toronto");
            timer.Stop();

            TimeSpan timeTaken = timer.Elapsed;
            string foo = "Time taken: " + timeTaken.ToString(@"m\:ss\.fff");
            UnityEngine.Debug.Log(foo);
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}