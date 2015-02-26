using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeCapture
{
    public partial class MainForm : Form
    {
        CaptureGame _captureGame;               // Collector game object
        private bool _playGame = true;          // Play/pause toggle
        private int _maxShapes = 30;            // Default maximum number of shapes

        public MainForm()
        {
            InitializeComponent();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _captureGame.Reset();
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _playGame = !_playGame;
            pauseToolStripMenuItem.Checked = !_playGame;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            _captureGame = new CaptureGame(_maxShapes, mainPictureBox.ClientSize);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            _captureGame.BoardSize = mainPictureBox.ClientSize;
        }

        private void mainPictureBox_Paint(object sender, PaintEventArgs e)
        {
            _captureGame.getShapeMaximum(_captureGame.CollectorPoints);
            _captureGame.DrawCollector(e.Graphics);
            _captureGame.DrawCollectorShapes(e.Graphics);
            string gameStatus = "Hits: " + _captureGame.CollectorHits + "  -  Points: " + _captureGame.CollectorPoints;
            using (Font font = new Font("Arial", 12, FontStyle.Bold))
            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                e.Graphics.DrawString(gameStatus, font, brush, new Point(5, 5));
            }
            gameTimer.Enabled = true;
        }

        private void mainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_playGame) _captureGame.CollectorPosition = new Point(e.X, e.Y);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (_playGame)
            {
                gameTimer.Enabled = false;
                _captureGame.AnimateCollectorShapes();
                mainPictureBox.Invalidate();
            }
        }
    }
}
