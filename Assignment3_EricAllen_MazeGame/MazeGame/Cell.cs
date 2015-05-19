using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MazeGame
{
    class Cell
    {
        private Traveller traveller;
        private Trail trail;
        private Finish finish;
        private bool containsTraveller = false;
        private bool containsTrail = false;
        private bool containsWin = false;
        private string returnDirection;

        public bool ContainsWin
        {
            get { return containsWin; }
            set { containsWin = value; }
        }

        public bool ContainsTrail
        {
            get { return containsTrail; }
            set { containsTrail = value; }
        }

        public string ReturnDirection
        {
            get { return returnDirection; }
            set { returnDirection = value; }
        }
        private int x;

        public bool ContainsTraveller
        {
            get { return containsTraveller; }
            set { containsTraveller = value; }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        private int y;

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        private int[] walls;

        public int[] Walls
        {
            get { return walls; }
            set { walls = value; }
        }
        private const int size=30;

        public Cell(int top, int bottom, int left, int right, int x, int y)
        {
            this.x = x;
            this.y = y;
            walls = new int[4];
            walls[0] = top;
            walls[1] = bottom;
            walls[2] = left;
            walls[3] = right;
            traveller = new Traveller();
            trail = new Trail();
            finish = new Finish();
        }

        public void DrawCell(Graphics g)
        {
            Pen pen = new Pen(Color.Cyan);
            if (walls[0]!=0)
            {
                g.DrawLine(pen, y * size, x * size, (y+1) * size, x * size);
            }
            if (walls[1] != 0)
            {
                g.DrawLine(pen, y * size, (x+1) * size, (y+1) * size, (x + 1) * size);
            }
            if (walls[2] != 0)
            {
                g.DrawLine(pen, y * size, x * size, y * size, (x+1) * size);
            }
            if (walls[3] != 0)
            {
                g.DrawLine(pen, (y+1) * size, x * size, (y + 1) * size, (x + 1) * size);
            }
            if (containsTraveller)
            {
                traveller.DrawSelf(g, y, x);
            }
            if (ContainsTrail)
            {
                trail.DrawSelf(g, y, x);
            }
            if (ContainsWin)
            {
                finish.DrawSelf(g, y, x);
            }
            
        }


    }
}
