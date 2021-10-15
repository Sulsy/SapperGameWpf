using System;
using System.Linq;


namespace SapperLogic
{
    /// <summary>
    /// Extension class for Grid Check Win or Lose Player
    /// </summary>
    public static class GameOperator 
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
        /// Operator of the Grid class that checks the state of the game
        /// </summary>
        /// <param name="cell">Open Cell/Flag Cell </param>
        /// <returns></returns>
        public static bool? GameState(Cell cell)
        {
            var instance = GetInstance();
            if (cell.Check)
            {
                if (!Lose(instance, cell))
                {
                    return Win(instance);
                }
                else return null;

            }
            else
            {
                if (cell.Flag)
                {
                    return Win(instance);
                }
            }

            return false;
        }

        /// <summary>
        /// Winner player or no
        /// </summary>
        /// <returns>true is winner false is not</returns>
        private static bool Win(Grid instance)
        {
            var Defuse = instance.ListFlag.Count(cell => cell.Bomb);

            if (Defuse == instance.ListBomb.Count && instance.ListFlag.Count == instance.ListBomb.Count|| NoBombsOpened(instance))
            {
                Console.WriteLine("You Win");
                return true;
            }
            else
            {
                return false;
            }


        }

        /// <summary>
        ///  /// Lose player or no
        /// </summary>
        /// <param name="cell">Cell open</param>
        /// <returns>true is lose false is no</returns>
        private static bool Lose(Grid instance, Cell cell)
        {
            if (!cell.Bomb) return false;
            instance = null;
            Grid.State = false;
            return true;


        }
        /// <summary>
        /// Checks if all cells are open except bombs
        /// </summary>
        /// <param name="instance">Reference to an Grid</param>
        /// <returns>True is all cells are open except bombs false is no</returns>
        private static bool NoBombsOpened(Grid instance)
        {
            return instance.Girds.Cast<Cell>().All(cell => cell.Bomb || cell.Check);
        }
    }
}
