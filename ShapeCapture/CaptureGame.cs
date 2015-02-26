using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeCapture
{
    public class CaptureGame
    {
        private Size _boardSize;
        public Size BoardSize { set { _boardSize = value; } }
        private Random _random = new Random();
        private Collector _collector;
        private ICaptureShape[] _captureShapes;

        public int CollectorHits { get { return _collector.Collected; } }
        public int CollectorPoints { get { return _collector.CollectedPoints; } }
        public Point CollectorPosition
        {
            set
            {
                value.X -= _collector.Dimensions.Width;
                value.Y -= _collector.Dimensions.Height;
                _collector.Location = value;
            }
        }

        public CaptureGame(int collectorShapeCount, Size boardSize)
        {
            _boardSize = boardSize;
            _collector = new Collector(Color.Blue, new Point(0, 0), new Size(30, 30));
            _captureShapes = new ICaptureShape[collectorShapeCount];

            for (int i = 0; i < _captureShapes.Length / 2; i++)
            {
                _captureShapes[i] = new EllipseCaptureShape(_random, new Size(20, 20), _boardSize, Color.Red, -5);
            }
            for (int i = 3; i < _captureShapes.Length; i++)
            {
                _captureShapes[i] = new RectangleCaptureShape(_random, new Size(20, 20), _boardSize, Color.Green, 5);
            }
        }

        public void DrawCollectorShapes(Graphics graphics)
        {
            // foreach in C# is read-only (immutable)
            foreach (ICaptureShape collectorShape in _captureShapes)
                collectorShape.Draw(graphics);
        }

        public void AnimateCollectorShapes()
        {
            for (int i = 0; i < _captureShapes.Length; i++)
            {
                _captureShapes[i].Animate(_boardSize);
                if (_captureShapes[i].Hit(_collector.Location, _collector.Dimensions))
                    _collector.Collect(_captureShapes[i], _random, _boardSize);
            }
        }

        public void DrawCollector(Graphics graphics)
        {
            _collector.Draw(graphics);
        }

        public void Reset()
        {
            _collector.Reset();
        }
    }
}
