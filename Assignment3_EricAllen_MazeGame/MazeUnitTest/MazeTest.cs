using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MazeGame;



namespace MazeUnitTest
{
    [TestClass]
    public class MazeTest
    {
        [TestMethod]
        public void GameTests()
        {
            Game game = new Game();
            game.MoveTraveller("right");
            game.CheckWin();
        }

    }
}
