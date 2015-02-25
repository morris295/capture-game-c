using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeCapture
{
    public interface IShape
    {
        Size Dimensions { get; set; }
        Point Location { get; set; }
        Color FillColor { get; set; }
        void Draw(Graphics graphics);
        bool Hit(Point location, Size dimensions);
        void Animate(Size boardSize);
    }
}
