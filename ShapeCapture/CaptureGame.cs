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
        private int _maxShapes;
        //private ICaptureShape[] _captureShapes;
        private List<ICaptureShape> _captureShapes = new List<ICaptureShape>();

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
            _maxShapes = collectorShapeCount;

            /**
             * Initialize the list to a set of 10 shapes.
             */
            for (int i = 0; i < 2 / 2; i++)
            {
                _captureShapes.Add(new EllipseCaptureShape(_random, new Size(20, 20), _boardSize, Color.Red, -5));
            }
            for (int i = (2/2); i < 2; i++)
            {
                _captureShapes.Add(new RectangleCaptureShape(_random, new Size(20, 20), _boardSize, Color.Green, 5));
            }
        }

        public void DrawCollectorShapes(Graphics graphics, int totalPoints)
        {
            int totalShapes = 0;
            if(totalPoints < 100) 
            {
                totalShapes = 2;
            }
            else if (totalPoints >= 100 && totalPoints < 250)
            {
                totalShapes = 4;
                addShapes(totalShapes, _captureShapes);
            }
            else if (totalPoints >= 250 && totalPoints < 350)
            {
                totalShapes = 6;
                addShapes(totalShapes, _captureShapes);
            }
            else if (totalPoints > 350)
            {
                totalShapes = _maxShapes;
                addShapes(totalShapes, _captureShapes);
            }

            for(int i = 0; i < totalShapes; i++)
            {
                _captureShapes[i].Draw(graphics);
            }
        }

        public void AnimateCollectorShapes()
        {
            for (int i = 0; i < _captureShapes.Count; i++)
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

        public void addShapes(int shapesToAdd, List<ICaptureShape> List)
        {
            for (int i = 0; i < (shapesToAdd / 2); i++)
            {
                _captureShapes.Add(new EllipseCaptureShape(_random, new Size(20, 20), _boardSize, Color.Red, -5));
            }
            for (int i = (shapesToAdd / 2); i < shapesToAdd; i++)
            {
                _captureShapes.Add(new RectangleCaptureShape(_random, new Size(20, 20), _boardSize, Color.Green, 5));
            }
        }
    }
}
