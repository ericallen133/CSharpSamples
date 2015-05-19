using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MazeGame
{
    class Finish
    {
        Image finishPic;
        const int size = 20;

        public Finish()
        {
            object pic = MazeGame.Properties.Resources.ResourceManager.GetObject("toast");
            finishPic= (Image)pic;
        }

        public void DrawSelf(Graphics g, int x, int y)
        {
            //SolidBrush brush = new SolidBrush(Color.DarkGreen);
            //g.FillRectangle(brush, (x*30)+6, (y*30)+6, size, size);
            g.DrawImage(finishPic, x * 30, y * 30);
        }
    }
}
