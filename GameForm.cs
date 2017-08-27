using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace life
{
    public partial class GameForm : Form
    {
        private GridDrawer _gridDrawer;
        private Graphics _graphics;
        private Thread _lifeProcess;
        private Counter _stepCount;
        private Counter _currentStep;
        private volatile bool _threadStopped = true;

        public GameForm()
        {
            InitializeComponent();
        }
        private void GameForm_Load(object sender, EventArgs e)
        {
            int columns = 30;
            int rows = 30;
            int cellSize = 15;

            _stepCount = new Counter();
            _currentStep = new Counter();

            Picture.Width = (columns * cellSize) + 2;
            Picture.Height = (rows * cellSize) + 2;
            _graphics = Picture.CreateGraphics();
            CenteredGameFieldPicture();

            // Создаем и выводим игровое поле
            _gridDrawer = new GridDrawer(new Point(columns, rows), cellSize);
            _gridDrawer.DrawGrid(_graphics);

            ShowStatus();

            //создаем экзепляр потока
            _lifeProcess = new Thread(Worker);
            _lifeProcess.Start();
        }
        private void Worker()
        {
            while (true)
            {
                //если остановлено или все клетки погибли.
                if (_threadStopped || _gridDrawer.CheckEnd())
                {
                    Thread.Sleep(1);
                    continue;
                }

                if (_currentStep.Count < _stepCount.Count)
                {
                    _gridDrawer.SetGeneration(_currentStep.Count++);
                }
                else
                {
                    _gridDrawer.SetNextGeneration();
                    ++_currentStep.Count;
                    ++_stepCount.Count;
                    _gridDrawer.GenerationsHistory.Add(
                        new KeyValuePair<Cell[,], int>(_gridDrawer.Cells, _gridDrawer.AliveCells.Count));
                }

                ShowStatus();
                _gridDrawer.DrawGrid(_graphics);
                Thread.Sleep(200);
            }

        }

        private void CenteredGameFieldPicture()
        {
            Picture.Left = ((this.Width - (Tools.Height / 2)) / 2) - (Picture.Width / 2);
            Picture.Top = ((this.Height - (Tools.Height / 2)) / 2) - (Picture.Height / 2);
        }

        private void Picture_MouseClick(object sender, MouseEventArgs e)
        {
            if (_currentStep.Count != _stepCount.Count)
            {
                MessageBox.Show(@"Мы находимся в истории");
            }
            else
            {
                _gridDrawer.MouseClick(_graphics, e.X, e.Y, e.Button);
                ShowStatus();
            }
        }

        private void Picture_Paint(object sender, PaintEventArgs e)
        {
            _gridDrawer.DrawGrid(e.Graphics);
        }



        private void btnStart_Click(object sender, EventArgs e)
        {
            _threadStopped = !_threadStopped;
            ToolStripButton toolStripButton = sender as ToolStripButton;
            if (toolStripButton != null) toolStripButton.Text = _threadStopped ? @"Старт" : @"Пауза";
        }

        private void btnStepForward_Click(object sender, EventArgs e)
        {

            if (_currentStep.Count < _stepCount.Count)
            {
                _gridDrawer.SetGeneration(_currentStep.Count++);
                ShowStatus();
                _gridDrawer.DrawGrid(_graphics);
            }
            else  // Если мы вычисляем новое поколение
            {
                _gridDrawer.GenerationsHistory.Add(new KeyValuePair<Cell[,], int>(_gridDrawer.Cells, _gridDrawer.AliveCells.Count));
                _gridDrawer.SetNextGeneration();
                ++_stepCount.Count;
                ++_currentStep.Count;
                ShowStatus();
                _gridDrawer.DrawGrid(_graphics);
            }
        }

        private void btnStepBackward_Click(object sender, EventArgs e)
        {
            if (_currentStep.Count - 1 < 0)
            {
                MessageBox.Show(@"Мы на старте");
            }
            else
            {
                _gridDrawer.SetGeneration(--_currentStep.Count);
                ShowStatus();
                _gridDrawer.DrawGrid(_graphics);
            }
        }

        private void ShowStatus()
        {
            Population.Text = $@"Клеток: {_gridDrawer.AliveCells.Count}";
            StepLabel.Text = $@"Шагов: {_stepCount.Count}";
            CurrentStep.Text = $@"Текущий шаг: {_currentStep.Count}";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _gridDrawer.Clear(_graphics);
            _currentStep.Count = 0;
            _stepCount.Count = 0;
            ShowStatus();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_lifeProcess.IsAlive)
                _lifeProcess.Abort();
        }

        ~GameForm()
        {
            _graphics.Dispose();
        }


    }
}
