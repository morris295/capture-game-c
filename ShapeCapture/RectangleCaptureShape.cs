using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeCapture
{
    public class RectangleCaptureShape : Shape, ICaptureShape
    {
        //J.M. RectangleCaptureShape constructor.  Uses base constructor.
        public RectangleCaptureShape(Random random, Size dimensions, Size boardSize, Color color, int points)
            : base(color)
        {
            _points = points;
            base.Dimensions = dimensions;
            base.Reset(random, boardSize);
        }

        //J.M. Integer points value.
        private int _points;
        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }

        //J.M. Overridden draw method from the shape abstract class, draws shape in game area.
        public override void Draw(Graphics graphics)
        {
            using (SolidBrush brush = new SolidBrush(base.FillColor))
                graphics.FillRectangle(brush, new Rectangle(base.Location, base.Dimensions));
        }

        //J.M. Overridden on collected method from ICaptureShape interface.  Defines the actions to be performed 
        //once a shape has been collected in the game area.
        public void OnCollected(Random random, Size boardSize)
        {
            base.Reset(random, boardSize);
        }
    }
}
