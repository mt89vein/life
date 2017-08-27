using System.Drawing;
using System.Windows.Forms;

namespace life
{

    public class Cell
    {
        public Rectangle Bounds { get; set; }

        public bool IsAlive { get; set; }

        public Cell(Point position, bool alive = false, int size = 10)
        {
            Bounds = new Rectangle(position.X, position.Y, size, size);

            IsAlive = alive;
        }

    }
}