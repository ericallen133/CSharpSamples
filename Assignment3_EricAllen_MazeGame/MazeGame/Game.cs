using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace MazeGame
{
    public class Game
    {
        Maze maze;
        const string win = "YOU WON!!!!!";
        public Game() {
            maze = new Maze();
            LoadGame();
        }

        public void DrawMaze(Graphics g){
            maze.DrawSelf(g);
        }

        public void LoadGame()
        {
            maze.PrepareWalls();
            maze.GenerateMaze();
            maze.SetStartingLocation();
            maze.SetFinishLocation();
        }

        public void MoveTraveller(string direction)
        {
            maze.Move(direction);           
        }

        public bool CheckWin()
        {
            if (maze.CheckWin())
            {
                return true;
            }
            return false;
        }

        public void WinGame(Graphics g)
        {
           // winThread = new Thread(()=>DisplayWinMessage(g));
            //winThread.Start();
            DisplayWinMessage(g);
            LoadGame();
        }

        public void DisplayWinMessage(Graphics g)
        {
            Font drawFont = new Font("Arial", 20);
            SolidBrush brush = new SolidBrush(Color.DeepSkyBlue);
            SolidBrush rectangleBrush = new SolidBrush(Color.White);
            g.FillRectangle(rectangleBrush, 240, 0, 200, 50);
            g.DrawString(win, drawFont, brush, 250, 10);
            Thread.Sleep(5000);

        }


    }
}
