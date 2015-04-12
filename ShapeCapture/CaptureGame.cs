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

            //Initialize the list.
            _captureShapes = populateShapeList(
                setShapePopulation(_collector.CollectedPoints),
                _captureShapes);
        }

        public void DrawCollectorShapes(Graphics graphics, int totalPoints)
        {
            //Re-populate the list based on score.
            _captureShapes = populateShapeList(
                setShapePopulation(_collector.CollectedPoints), 
                _captureShapes);

            //Draw all shapes in population.
            for(int i = 0; i < _captureShapes.Count; i++)
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

        public List<ICaptureShape> populateShapeList(int shapesToAdd, List<ICaptureShape> list)
        {
            //Empty list so that it isn't just adding shapes perpetually.
            if (list != null)
            {
                list.Clear();
            }

            //Add shapes splitting the population between the negative ellipse shape and the positive rectangle shape.
            for (int i = 0; i < (shapesToAdd / 2); i++)
            {
                list.Add(new EllipseCaptureShape(_random, new Size(20, 20), _boardSize, Color.Red, -5));
            }
            for (int i = (shapesToAdd / 2); i < shapesToAdd; i++)
            {
                list.Add(new RectangleCaptureShape(_random, new Size(20, 20), _boardSize, Color.Green, 5));
            }

            return list;
        }

        public int setShapePopulation(int totalScore)
        {
            int population = 0;

            if(totalScore <= 300)
            {
                population = 10;
                return population;
            }
            else if (totalScore <= 500)
            {
                population = 15;
                return population;
            }
            else if (totalScore <= 700)
            {
                population = _maxShapes;
                return population;
            }
            else
            {
                population = _maxShapes;
                return population;
            }
        }
    }
}
