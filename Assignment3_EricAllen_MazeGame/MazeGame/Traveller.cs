using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MazeGame
{
    class Traveller
    {
        Image travelerPic;
        const int size = 28;

        public Traveller()
        {
            object pic = MazeGame.Properties.Resources.ResourceManager.GetObject("shovel");
            travelerPic = (Image)pic;
        }

        public void DrawSelf(Graphics g, int x, int y)
        {
            //SolidBrush brush = new SolidBrush(Color.Bisque);
            //g.FillRectangle(brush, x*30, y*30, size, size);
            g.DrawImage(travelerPic, x * 30, y * 30);
        }

    }
}
