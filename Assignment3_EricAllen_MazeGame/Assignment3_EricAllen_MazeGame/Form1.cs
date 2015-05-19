using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MazeGame;

namespace Assignment3_EricAllen_MazeGame
{
    public partial class Form1 : Form
    {
        Game game;   
        public Form1()
        {
            InitializeComponent();
            game = new Game();
            this.WindowState = FormWindowState.Maximized;
            Invalidate();
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            game.DrawMaze(e.Graphics);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            string s = e.KeyValue.ToString();
            if (e.KeyValue.ToString() == "87")
            {
                game.MoveTraveller("up");
            }
            else if (e.KeyValue.ToString() == "83")
            {
                game.MoveTraveller("down");
            }
            else if (e.KeyValue.ToString() == "68")
            {
                game.MoveTraveller("right");
            }
            else if (e.KeyValue.ToString() == "65")
            {
                game.MoveTraveller("left");
            }


            Invalidate();
            if (game.CheckWin())
            {
                game.WinGame(this.CreateGraphics());
            }
        }


    }
}
