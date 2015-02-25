using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeCapture
{
    public class EllipseCaptureShape : Shape, ICaptureShape
    {
        public EllipseCaptureShape(Random random, Size dimensions, Size boardSize, Color color, int points)
            : base(color)
        {
            _points = points;
            base.Dimensions = dimensions;
            base.Reset(random, boardSize);
        }

        private int _points;
        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }

        public override void Draw(Graphics graphics)
        {
            using (SolidBrush brush = new SolidBrush(base.FillColor))
                graphics.FillEllipse(brush, new Rectangle(base.Location, base.Dimensions));
        }

        public void OnCollected(Random random, Size boardSize)
        {
            base.Reset(random, boardSize);
        }
    }
}
