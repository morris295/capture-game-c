using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeCapture
{
    public class Collector : ICollector
    {
        private Point _location;
        public Point Location
        {
            get { return _location; }
            set { _location = value; }
        }
        private Size _dimensions;
        public Size Dimensions
        {
            get { return _dimensions; }
            set { _dimensions = value; }
        }
        private Color _fillColor;
        public Color FillColor
        {
            get { return _fillColor; }
            set { _fillColor = value; }
        }
        private int _collected;
        public int Collected
        {
            get { return _collected; }
        }
        private int _collectedPoints;
        public int CollectedPoints
        {
            get { return _collectedPoints; }
        }
        public Collector(Color color, Point location, Size dimensions)
        {
            _fillColor = color;
            _location = location;
            _dimensions = dimensions;
        }
        public void Collect(ICaptureShape collectorShape, Random random, Size boardSize)
        {
            _collected++;
            _collectedPoints += collectorShape.Points;
            collectorShape.OnCollected(random, boardSize);
        }

        public void Reset()
        {
            _collected = 0;
            _collectedPoints = 0;
        }

        public void Draw(Graphics graphics)
        {
            using (SolidBrush brush = new SolidBrush(_fillColor))
                graphics.FillRectangle(brush, new Rectangle(_location, _dimensions));
        }
    }
}
