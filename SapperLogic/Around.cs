
using System;

namespace SapperLogic
{
    /// <summary>
    /// Extension class for Grid
    /// </summary>
    public static class Around
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
        /// Searches for a cage with a bomb within a radius of one cage from the given one
        /// </summary>
        /// <param name="y">Coordinate fo y</param>
        /// <param name="x">Coordinate fo x</param>
        /// <returns>Number of cells with a bomb nearby</returns>
        public static int AroundBomb(int y, int x)
        {
            var instance = GetInstance();
            var count = 0;
            for (var i = y - 1; i < y + 2; i++)
            {
                for (var j = x - 1; j < x + 2; j++)
                {
                    if (CountCoordinate(instance, i, j) && instance.Girds[i, j].Bomb)
                    {
                        count++;
                    }

                }
            }

            return count;
        }
        /// <summary>
        /// Searches for a cage with a flag Cell within a radius of one cage from the given one
        /// </summary>
        /// <param name="y">Coordinate fo y</param>
        /// <param name="x">Coordinate fo x</param>
        /// <returns></returns>
        public static int AroundFlag(int y, int x)
        {
            var instance = GetInstance();
            var count = 0;
            for (var i = y - 1; i < y + 2; i++)
            {
                for (var j = x - 1; j < x + 2; j++)
                {
                    if (CountCoordinate(instance, i, j) && instance.Girds[i, j].Flag)
                    {
                        count++;
                    }

                }
            }

            return count;
        }
        /// <summary>
        /// Opens cells nearby, provided that the number of flags nearby is equal to the number of
        /// Cell.Around (the number of bombs nearby)
        /// </summary>
        /// <param name="y">Coordinate fo y</param>
        /// <param name="x">Coordinate fo x</param>
        public static void AroundOpen(int y, int x)
        {
            var instance = GetInstance();
            for (var i = y - 1; i < y + 2; i++)
            {
                for (var j = x - 1; j < x + 2; j++)
                {
                    if (!CountCoordinate(instance, i, j) || instance.Girds[i, j].Flag ||
                        instance.Girds[i, j].Check) continue;
                    instance.OpenCell(i, j);
                    GameOperator.GameState(instance.Girds[i, j]);

                }
            }
        }
        /// <summary>
        /// Searches for a cage with a not zero Around Cell within a radius of one cage from the given one
        /// </summary>
        /// <param name="y">Coordinate fo y </param>
        /// <param name="x">Coordinate fo x</param>
        public static void AroundNoNull(int y, int x)
        {
            var instance = GetInstance();
            for (var i = y - 1; i < y + 2; i++)
            {
                for (var j = x - 1; j < x + 2; j++)
                {
                    if (CountCoordinate(instance, i, j) && !instance.Girds[i, j].Check && !instance.Girds[i, j].Bomb)
                    {
                        instance.Girds[i, j].Check = true;
                    }

                }
            }


        }
        /// <summary>
        /// Searches for a cage with a zero Around Cell within a radius of one cage from the given one
        /// </summary>
        /// <param name="y">Coordinate fo y </param>
        /// <param name="x">Coordinate fo x</param>
        public static void AroundNull(int y, int x)
        {
            var instance = GetInstance();
            for (var i = y - 1; i < y + 2; i++)
            {
                for (var j = x - 1; j < x + 2; j++)
                {
                    if (!CountCoordinate(instance, i, j) || instance.Girds[i, j].Bomb ||
                        instance.Girds[i, j].Around != 0 || instance.Girds[i, j].Check) continue;
                    instance.Girds[i, j].Check = true;
                    AroundNull(i, j);
                    AroundNoNull(i, j);

                }
            }


        }
        /// <summary>
        /// Checking if cells exist around a given point
        /// </summary>
        /// <param name="instance">return gird</param>
        /// <param name="y">Coordinate fo y</param>
        /// <param name="x">Coordinate fo x</param>
        /// <returns>returns true if indices are within the bounds</returns>
        private static bool CountCoordinate(Grid instance, int y,int x) => instance.Size > y && instance.Size > x && y >= 0 && x >= 0;
    }
}
