using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MazeGame
{
    class Trail
    {
        const int size = 20;


        public void DrawSelf(Graphics g, int x, int y)
        {
            SolidBrush brush = new SolidBrush(Color.Crimson);
            g.FillRectangle(brush, (x*30)+5, (y*30)+5, size, size);
        }

    }
}
