using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace life
{
    /// <summary>
    /// Поле
    /// </summary>
    public class Grid
    {
        public Point Size { get; private set; }
        public Cell[,] Cells { get; set; }
        public Counter AliveCells;
        public Cell[,] NextCellStates { get; set; }

        public Grid(Point size, int cellSize, bool isAlive = false)
        {
            Size = size;
            Cells = new Cell[Size.X, Size.Y];
            NextCellStates = new Cell[Size.X, Size.Y];
            AliveCells = new Counter();


            //забиваем дефолтными значениями
            for (int i = 0; i < Size.X; i++)
            {
                for (int j = 0; j < Size.Y; j++)
                {
                    Cells[i, j] = new Cell(new Point(i * cellSize, j * cellSize), isAlive, cellSize);
                    NextCellStates[i, j] = new Cell(new Point(i * cellSize, j * cellSize), isAlive, cellSize);
                }
            }
        }

        /// <summary>
        /// Базовое получение ячейки, как текущего поколения, так и следующего
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="nextState"></param>
        /// <returns></returns>
        public Cell GetCell(int x, int y, bool nextState)
        {
            return nextState ? NextCellStates[x, y] : Cells[x, y];
        }

        /// <summary>
        /// Получить ячейку текущего поколения
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Cell GetCurrentGenerationCell(int x, int y)
        {
            return GetCell(x, y, false);
        }
        /// <summary>
        /// Получить ячейку следующего поколения
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Cell GetNextGenerationCell(int x, int y)
        {
            return GetCell(x, y, true);
        }

        /// <summary>
        /// Подсчитываем количество живых ячеек
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        private void CountAliveCells(int x, int y, bool value)
        {
            if (GetCurrentGenerationCell(x, y).IsAlive == value) return;

            if (value)
                AliveCells.Count++;
            else
                AliveCells.Count--;
        }

        /// <summary>
        /// Базовое изменение состояния клетки как текущего поколения, так и следующего.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        /// <param name="applyToNextGeneration"></param>
        private void SetCellState(int x, int y, bool value, bool applyToNextGeneration)
        {
            Cell cell = GetCell(x, y, applyToNextGeneration);

            if (cell == null) return;

            CountAliveCells(x, y, value);
            cell.IsAlive = value;
        }

        /// <summary>
        /// Изменения состояния клетки следующего поколения
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="alive"></param>
        protected void SetNextGenerationCellState(int x, int y, bool alive)
        {
            SetCellState(x, y, alive, true);
        }

        /// <summary>
        /// Изменения состояния клетки текущего поколения
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="alive"></param>
        protected void SetCurrentGenerationCell(int x, int y, bool alive)
        {
            SetCellState(x, y, alive, false);
        }

        /// <summary>
        /// Очищаем поле
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < Size.X; i++)
                for (int j = 0; j < Size.Y; j++)
                    NextCellStates[i, j].IsAlive = false;

            SetNextState();
        }

        /// <summary>
        /// Подсчет количества живых соседей для текущей клетки на замкнутой плоскости
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int AliveNeighborsCount(int x, int y)
        {
            int aliveCellsCount = 0;

            //гуляем вокруг клетки
            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                {
                    int i = x + dx;
                    int j = y + dy;

                    //если i < 0, смотрим значение на другом конце 
                    if (i < 0)
                        i = Size.X - 1;
                    else
                    //если i > размера поля смотрим значение на другом конце
                    if (i > Size.X - 1)
                        i = 0;

                    //если j < 0, смотрим значение на другом конце 
                    if (j < 0)
                        j = Size.Y - 1;
                    else
                        //если j > размера поля смотрим значение на другом конце
                    if (j > Size.Y - 1)
                        j = 0;

                    //если этот сосед живой, добавляем 1
                    if (GetCurrentGenerationCell(i, j).IsAlive)
                        aliveCellsCount++;
                }
            //убираем из подсчета текущую, если она была живой
            if (GetCurrentGenerationCell(x, y).IsAlive) { aliveCellsCount--; }

            return aliveCellsCount;
        }


        /// <summary>
        /// Получаем состояние клетки на следующее поколение
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool GetNextGenerationCellState(int x, int y)
        {
            bool isCurrentAlive = GetCurrentGenerationCell(x, y).IsAlive;
            bool result = false;
            int count = AliveNeighborsCount(x, y);

            //если текущая живая и соседей меньше двух -> она умирает
            if (isCurrentAlive && count < 2)
                result = false;

            //если текущая живая и количество соседей 2 или 3, то она продолжает жить
            if (isCurrentAlive && (count == 2 || count == 3))
                result = true;

            // если текущая живая и количество соседей больше трех, то умирает от перенаселения
            if (isCurrentAlive && count > 3)
                result = false;

            // если текущая не живая, и соседей 3, то в ней зарождается жизнь
            if (!isCurrentAlive && count == 3)
                result = true;

            return result;
        }

        /// <summary>
        /// Меняет поколение
        /// </summary>
        /// <returns></returns>
        public void SetNextGeneration()
        {
            bool changed = false;

            //гуляем по полю
            for (int x = 0; x < Size.X; x++)
                for (int y = 0; y < Size.Y; y++)
                {
                    
                    bool oldState = GetCurrentGenerationCell(x, y).IsAlive;
                    bool newState = GetNextGenerationCellState(x, y);

                    //если состояние клетки изменилось, поднимаем флажок
                    if (newState != oldState)
                        changed = true;
                    //изменям состояние клетки
                    SetNextGenerationCellState(x, y, newState);
                }

            //если что-то изменилось, меняем поколение.
            if (changed)
                SetNextState();
        }


        /// <summary>
        /// Смена поколения
        /// </summary>
        public void SetNextState()
        {
            for (int x = 0; x < Size.X; x++)
                for (int y = 0; y < Size.Y; y++)
                    Cells[x, y].IsAlive = NextCellStates[x, y].IsAlive;
        }
    }



}