using SapperLogic;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GridTests
{
    [TestClass]
    public class GridTestsNoSizeDefault
    {
        private Grid grid = null, gridTwo = null;
        [TestMethod]
        public void GridInstanceSize()
        {
            //arange
            int size = 10, bomb = 10;
            //act
            grid = Grid.GetInstance(bomb, size);
            //assert   
            Assert.AreEqual(size, grid.Size);
            Grid.Clean();
        }
        [TestMethod]
        public void GridOverridingSize()
        {
            //arange
            int sizeOne = 10, sizeTwo = 5, bomb = 10;
            //act
            grid = Grid.GetInstance(bomb,sizeOne);
            gridTwo = Grid.GetInstance(bomb, grid.Size);
            grid = Grid.GetInstance(bomb,sizeTwo);
            //assert   
            Assert.AreEqual(grid.Size, gridTwo.Size);
            Grid.Clean();
        }
        [TestMethod]
        public void GridOpenCell()
        {
            //arange
            int x = 3, y = 4, size = 5, bomb = 10;
            bool Open = true;
            //act
            grid = Grid.GetInstance(bomb, size);
            grid.OpenCell(y, x);
            //assert   
            Assert.AreEqual(Open, grid.Girds[y, x].Check);
            Grid.Clean();
        }
        [TestMethod]
        public void GridFlag()
        {
            //arange
            int x = 3, y = 4, size = 5, bomb = 10;
            bool Flag = true;
            //act
            grid = Grid.GetInstance(bomb,size);
            grid.Flag(y, x);
            //assert   
            Assert.AreEqual(Flag, grid.Girds[y, x].Flag);
            Grid.Clean();
        }
        [TestMethod]
        public void GridLose()
        {
            //arange
            bool? lose = null;
            int size = 6, bomb = 10;
            bool? girdLose = true;
            //act
            grid = Grid.GetInstance(bomb,size);
            foreach (var cell in grid.Girds)
            {
                girdLose = grid.OpenCell(cell);
                if (girdLose == null)
                {
                    break;
                }
            }

            //assert   
            Assert.AreEqual(lose, girdLose);
            Grid.Clean();
        }
        [TestMethod]
        public void GridWin()
        {
            //arange
            bool win = false;
            int size = 6, bomb = 10;
            bool? girdWin = false;
            //act
            grid = Grid.GetInstance(bomb,size);
            for (int i = 0; i < grid.ListBomb.Count; i++)
            {
                girdWin = grid.Flag(grid.ListBomb[i]);
            }
            //assert   
            Assert.AreEqual(win, girdWin);
            Grid.Clean();
        }
        [TestMethod]
        public void GridFlagClose()
        {
            //arange
            bool flag = true;
            int size = 6, bomb = 10;
            //act
            grid = Grid.GetInstance(bomb,size);
            grid.Flag(grid.Girds[0, 0]);
            grid.Flag(grid.Girds[0, 0]);
            //assert   
            Assert.AreNotEqual(flag, grid.Girds[0, 0]);
            Grid.Clean();
        }
    }
    [TestClass]
    public class GridTestsSizeDefault
    {
        private Grid grid = null, gridTwo = null;
        [TestMethod]
        public void GridInstance()
        {
            //arange
            //act
            grid = Grid.GetInstance();
            //assert   
            Assert.IsNotNull(grid);
            Grid.Clean();
        }
        [TestMethod]
        public void GridOverriding()
        {
            //arange
            //act
            grid = Grid.GetInstance();
            gridTwo = Grid.GetInstance();
            grid = Grid.GetInstance();
            //assert   
            Assert.AreEqual(gridTwo, grid);
            Grid.Clean();
        }
        [TestMethod]
        public void GridOpenCell()
        {
            //arange
            int x = 0, y = 0;
            bool Open = true;
            //act
            grid = Grid.GetInstance();
            grid.OpenCell(y, x);
            //assert   
            Assert.AreEqual(Open, grid.Girds[y, x].Check);
            Grid.Clean();
        }
        [TestMethod]
        public void GridFlag()
        {
            //arange
            int x = 1, y = 1;
            bool Flag = true;
            //act
            grid = Grid.GetInstance();
            grid.Flag(y, x);
            //assert   
            Assert.AreEqual(Flag, grid.Girds[y, x].Flag);
            Grid.Clean();
        }
        [TestMethod]
        public void GridLose()
        {
            //arange
            bool? lose = null;
            bool? girdLose = null;
            //act
            grid = Grid.GetInstance();
            foreach (var cell in grid.Girds)
            {
                girdLose = grid.OpenCell(cell);
                if (girdLose == null)
                {
                    break;
                }
            }
            //assert   
            Assert.AreEqual(lose, girdLose);
            Grid.Clean();
        }
        [TestMethod]
        public void GridFlagClose()
        {
            //arange
            bool flag = true;
            //act
            grid = Grid.GetInstance();
            grid.Flag(grid.Girds[0, 0]);
            grid.Flag(grid.Girds[0, 0]);
            //assert   
            Assert.AreNotEqual(flag, grid.Girds[0, 0]);
            Grid.Clean();
        }


    }
}
