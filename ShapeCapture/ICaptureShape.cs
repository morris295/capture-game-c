using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeCapture
{
    public interface ICaptureShape : IShape
    {
        int Points { get; set; }
        void OnCollected(Random random, Size boardSize);
    }

}
