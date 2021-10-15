using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SapperLogic
{
    /// <summary>
    /// Extension class for Grid
    /// </summary>
    public static class GenerateGirdsManipulation
    {
        /// <summary>
        /// Given reference to an Grid
        /// </summary>
        /// <returns>Reference to an Grid</returns>
        private static Grid GetInstance()
        {
            return Grid.GetInstance();
        }
        /// <summary>
        /// Random create id of bomb
        /// </summary>
        /// <param name="ID">HashSet Collection of unique random numbers</param>
        public static void RandomID(ref HashSet<int> ID)
        {
            var instance = GetInstance();
            var r = new Random();
            while (ID.Count < instance.CountBombs)
            {
                ID.Add(r.Next(0, instance.Size * instance.Size));
            }
        }
        /// <summary>
        /// Giving the cells the state of the bomb
        /// </summary>
        /// <param name="ID">HashSet Collection of unique random numbers</param>
        public static void BombsFoCells(ref HashSet<int> ID)
        {
            var instance = GetInstance();
            foreach (var cell in instance.Girds)
            {
                foreach (var ids in ID.Where(ids => cell.Id == ids))
                {
                    cell.Bomb = true;
                    instance.ListBomb.Add(instance.Girds[cell.Y, cell.Y]);
                }
            }
        }
        /// <summary>
        /// Filling the grid with cells
        /// </summary>
        public static void GridsInit()
        {
            var instance= GetInstance();
            var id = 0;
            for (var i = 0; i < instance.Size; i++)
            {
                for (var j = 0; j < instance.Size; j++)
                {
                    instance.Girds[i, j] = new Cell(0, false, false, i, j, id);
                    id++;
                }
            }
        }
        /// <summary>
        /// Calculation of the nearest bombs for the entire grid
        /// </summary>
        public static void AroundAllGrid()
        {
            var instance = GetInstance();
            foreach (var cell in instance.Girds)
            {
                cell.Around = Around.AroundBomb(cell.Y, cell.X);
            }
        }
        /// <summary>
        /// First opening of the cage (Protection of the player from defeat on 1 turn)
        /// </summary>
        /// <param name="y">Coordinate first open</param>
        /// <param name="x">Coordinate first open</param>
        public static void FirstCellOpen(int y, int x)
        {
            var instance = GetInstance();

            foreach (var cell in instance.Girds)
            {
                if (cell.Bomb && !cell.Check) continue;
                instance.Girds[cell.Y, cell.X].Bomb = true;
                instance.Girds[y, x].Bomb = false;
                break;
            }
            AroundAllGrid();
        }
    }
}
