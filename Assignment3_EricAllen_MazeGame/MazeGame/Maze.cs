using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MazeGame
{
    class Maze
    {
        Cell[,] mazeCells;
        const int width=20;
        const int height=20;
        Stack<Cell> cellStack;
        Random r = new Random();


        public Cell[,] MazeCells
        {
            get { return mazeCells; }
        }

       public Maze()
       {
           mazeCells = new Cell[height,width];
           cellStack = new Stack<Cell>();
           Random r = new Random();
           PrepareWalls();
       }

       public void PrepareWalls()
       {
           for (int i = 0; i < height; i++)
           {
               for (int j = 0; j < width; j++)
               {
                   mazeCells[i, j] = new Cell(1, 1, 1, 1, i, j);
               }
           }
       }

       public void GenerateMaze()
       {
           int visitedCells = 0;
           int totalCells = width * height;
           Cell currentCell = mazeCells[9, 9];
           Cell tempCell;
           cellStack.Clear();
           while (visitedCells < totalCells-1)
           {
               List<Cell> neighbors = GetNeighborsWithWalls(currentCell);
               if (neighbors.Count > 0)
               {
                   tempCell = neighbors[r.Next(0, neighbors.Count)];
                   OpenWall(currentCell, tempCell);
                   cellStack.Push(currentCell);
                   currentCell = tempCell;
                   visitedCells++;
               }
               else
               {
                   currentCell = (Cell)cellStack.Pop();
               }
           }
           cellStack.Clear();

       }

       public void OpenWall(Cell cellOne, Cell cellTwo)
       {
           
           int x = cellOne.X - cellTwo.X;
           int y = cellOne.Y - cellTwo.Y;
           if (x > 0)
           {
               cellOne.Walls[0] = 0;
               cellTwo.Walls[1] = 0;
           }
           else if (x < 0)
           {
               cellOne.Walls[1] = 0;
               cellTwo.Walls[0] = 0;
           }
           if (y > 0)
           {
               cellOne.Walls[2] = 0;
               cellTwo.Walls[3] = 0;
           }
           else if (y < 0)
           {
               cellOne.Walls[3] = 0;
               cellTwo.Walls[2] = 0;
           }


       }

       public List<Cell> GetNeighborsWithWalls(Cell cell)
       {
           Cell tempCell;
           List<Cell> neighbors = new List<Cell>();
           if (cell.X > 0)
           {
               tempCell = mazeCells[cell.X - 1, cell.Y];
                   if (CheckWalls(tempCell))
                   {
                       neighbors.Add(tempCell);
                   }
           }
           if(cell.X<height-1){
               tempCell = mazeCells [cell.X+1, cell.Y];
                if (CheckWalls(tempCell))
                   {
                       neighbors.Add(tempCell);
                   }
           }
           

           if (cell.Y > 0)
           {
               tempCell = mazeCells[cell.X, cell.Y - 1];
                   if (CheckWalls(tempCell))
                   {
                       neighbors.Add(tempCell);
                   }
           }
           if(cell.Y < width -1){
               tempCell = mazeCells [cell.X, cell.Y+1];
                if (CheckWalls(tempCell))
                   {
                       neighbors.Add(tempCell);
                   }
           }
           
           return neighbors;
       }

       private bool CheckWalls(Cell cell)
       {
           for (int i = 0; i < cell.Walls.Length; i++)
           {
               if (cell.Walls[i] == 0)
               {
                   break;
               }
               if (i == cell.Walls.Length - 1)
               {
                   return true;
               }
           }
           return false;
       }

        public void DrawSelf(Graphics g)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    mazeCells[i, j].DrawCell(g);
                }
            }
           
        }

        public void Move(string direction)
        {
            Cell currentCell = cellStack.Peek();
            List<string> moves = GetValidMoves(currentCell);
            if (moves.Contains(direction))
            {
               
                if (direction == cellStack.Peek().ReturnDirection)
                {
                    currentCell.ContainsTraveller = false;
                    cellStack.Pop();
                    cellStack.Peek().ContainsTraveller = true;
                    cellStack.Peek().ContainsTrail = false;
                }
                else if (direction == "left")
                {
                    AddToStack(currentCell, mazeCells[currentCell.X, currentCell.Y-1], "right");
                }
                else if (direction == "right")
                {
                    AddToStack(currentCell, mazeCells[currentCell.X, currentCell.Y+1], "left");
                }
                else if (direction == "up")
                {
                    AddToStack(currentCell, mazeCells[currentCell.X - 1, currentCell.Y], "down");
                }
                else  if (direction == "down")
                {
                    AddToStack(currentCell, mazeCells[currentCell.X + 1, currentCell.Y], "up");
                }
            }
        }

        private void AddToStack(Cell currentCell, Cell moveToCell, string returnDirection)
        {
            currentCell.ContainsTrail = true;
            currentCell.ContainsTraveller = false;
            moveToCell.ContainsTraveller = true;
            moveToCell.ReturnDirection = returnDirection;
            cellStack.Push(moveToCell);
        }

        public bool CheckWin()
        {
            if (cellStack.Peek().ContainsTraveller && cellStack.Peek().ContainsWin)
            {
                return true;
            }
            return false;
        }

        public List<string> GetValidMoves(Cell cell)
        {
            List<string> moves = new List<string>();
            if (cell.Walls[0] == 0)
            {
                moves.Add("up");
            }
            if (cell.Walls[1] == 0)
            {
                moves.Add("down");
            }
            if (cell.Walls[2] == 0)
            {
                moves.Add("left");
            }
            if (cell.Walls[3] == 0)
            {
                moves.Add("right");
            }
            return moves;

        }

        public void SetStartingLocation()
        {
            int x = r.Next(width);
            int y = r.Next(height);
            cellStack.Push(mazeCells[x, y]);
            cellStack.Peek().ContainsTraveller = true;
           
        }

        public void SetFinishLocation()
        {
            int x = r.Next(width);
            int y = r.Next(height);
            mazeCells[x, y].ContainsWin = true;

        }
    }
}
