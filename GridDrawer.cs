using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace life
{
    public class GridDrawer : Grid
    {
        public Graphics Graphics { get; private set; }
        public Image Image { get; private set; }

        public List<KeyValuePair<Cell[,], int>> GenerationsHistory;

        public int CellSize { get; private set; }

        public GridDrawer(Point size, int cellSize, bool isAlive = false)
            : base(size, cellSize, isAlive)
        {
            CellSize = cellSize;

            Image = new Bitmap(size.X * cellSize, size.Y * cellSize);
            Graphics = Graphics.FromImage(Image);
            GenerationsHistory = new List<KeyValuePair<Cell[,], int>>();
        }

        /// <summary>
        /// Отрисовка одной ячейки
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="print"></param>
        private void PaintCell(Graphics graphics, int x, int y, bool print = true)
        {
            Cell cell = GetCurrentGenerationCell(x, y);

            if (cell == null) return;

            using (Pen pen = new Pen(Color.Silver))
            {
                lock (Graphics)
                {
                    // Рисуем ячейку и её границу
                    Graphics.FillRectangle(cell.IsAlive ? Brushes.DarkGreen : Brushes.White, cell.Bounds);
                    Graphics.DrawRectangle(pen, cell.Bounds);
                }

                // Рисуем на экран
                if (print)
                    graphics.DrawImage(Image, cell.Bounds.Left, cell.Bounds.Top, cell.Bounds, GraphicsUnit.Pixel);
            }
        }

        /// <summary>
        /// Проверка на завершение цикла жизни.
        /// TODO: проверка на закицкливание, проверка на устойчивость.
        /// </summary>
        /// <returns></returns>
        public bool CheckEnd()
        {
            int aliveCount = 0;
           
            for (int x = 0; x < Size.X; x++)
                for (int y = 0; y < Size.Y; y++)
                {
                    Cell current = GetCurrentGenerationCell(x, y);

                    if (current.IsAlive)
                        aliveCount++;
                }
            
            if (aliveCount == 0)
                return true;

            return false;
        }
        /// <summary>
        /// Левая кнопка мыши - добавляет, правая удаляет
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="button"></param>
        public void MouseClick(Graphics graphics, int x, int y, MouseButtons button)
        {
            //приводим x и y к индексам массива.
            x /= CellSize;
            y /= CellSize;

            if ((button & MouseButtons.Left) != 0)
                SetCurrentGenerationCell(x, y, true);
            else if ((button & MouseButtons.Right) != 0)
                SetCurrentGenerationCell(x, y, false);

            PaintCell(graphics, x, y);
        }

        /// <summary>
        /// Отрисовывает 
        /// </summary>
        /// <param name="graphics"></param>
        public void DrawGrid(Graphics graphics)
        {
            for (int x = 0; x < Size.X; x++)
                for (int y = 0; y < Size.Y; y++)
                    PaintCell(graphics, x, y, false);

            graphics.DrawImage(Image, 0, 0);
        }

        /// <summary>
        /// Очищаем поле, чистим счетчик и историю.
        /// </summary>
        /// <param name="graphics"></param>
        public void Clear(Graphics graphics)
        {
            Clear();
            GenerationsHistory.Clear();
            AliveCells.Count = 0;
            DrawGrid(graphics);
        }


        ~GridDrawer()
        {
            Graphics.Dispose();
            Image.Dispose();
        }

        /// <summary>
        /// Берем из истории состояние и применяем текущему.
        /// TODO: Не работает отрисовка хождения по истории..
        /// </summary>
        /// <param name="step"></param>
        public void SetGeneration(int step)
        {
            Cell[,] history = GenerationsHistory[step].Key;
            base.AliveCells.Count = GenerationsHistory[step].Value;

            for (int x = 0; x < Size.X; x++)
                for (int y = 0; y < Size.Y; y++)
                    Cells[x, y].IsAlive = history[x, y].IsAlive;
        }
    }
}