using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SapperUI.Game_ViewModel
{
    /// <summary>
    /// View Model for SapperUI
    /// </summary>
    static class SapperVM
    {
        /// <summary>
        /// Object type Grid
        /// </summary>
        public static SapperLogic.Grid GameGrid;

        /// <summary>
        /// Size Bomb for Grid
        /// </summary>
        public static Int32 sizeGrid;
        /// <summary>
        /// BombCount for Grid
        /// </summary>
        public static Int32 bombCount;
        /// <summary>
        /// Size Grid
        /// </summary>
        public static Int32 SizeGrid
        {
            get => sizeGrid;
            set => sizeGrid = value;
        }
        /// <summary>
        /// Bomb Grid
        /// </summary>
        public static Int32 BombCount
        {
            get => bombCount;
            set => bombCount = value;
        }
        /// <summary>
        /// Set Flag for Cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Children"></param>
        public static void SetFlag(object sender, UIElementCollection Children)
        {
            var cell = sender as Button;
            SapperVM.GameGrid.Flag((int)cell.GetValue(Grid.RowProperty), (int)cell.GetValue(Grid.ColumnProperty));
            CheckGridState(Children);
        }
        /// <summary>
        /// Open Cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Children"></param>
        /// <returns></returns>
        public static bool OpenCell(Object sender, UIElementCollection Children)
        {
            var cell = sender as Button;
            var AroundOpen = SapperVM.GameGrid.OpenCell((int)cell.GetValue(Grid.RowProperty), (int)cell.GetValue(Grid.ColumnProperty));
            var IsWin = SapperLogic.GameOperator.GameState(SapperVM.GameGrid.Girds[(int)cell.GetValue(Grid.RowProperty), (int)cell.GetValue(Grid.ColumnProperty)]);
            CheckGridState(Children);
            if (IsWin != null && IsWin.Value) { MessageBox.Show("You Win!");return false; }
            if (IsWin == null || AroundOpen == null) { MessageBox.Show("You Lose!"); return false; }

            return true;
        }
        /// <summary>
        /// Level Complexity
        /// </summary>
        /// <param name="lvl"></param>
        public static void DifficultyParser(string lvl)
        {
            switch (lvl)
            {
                default:
                    SizeGrid = 10;
                    BombCount = 10;
                    break;

                case "medium":
                    SizeGrid = 18;
                    BombCount = 40;
                    break;

                case "hard":
                    SizeGrid = 24;
                    BombCount = 99;
                    break;
            }
        }
        /// <summary>
        /// Cell state for View
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static string CellStatus(SapperLogic.Cell cell) => !cell.Check ? cell.Flag ? "f" : " " : cell.Bomb ? "b" : cell.Around.ToString();

        /// <summary>
        /// Cell color for grid
        /// </summary>
        /// <param name="Children"></param>
        public static void Clean()
        {
            SapperLogic.Grid.Clean();
        }
       public static void CheckGridState(UIElementCollection Children)
        {
            foreach (Button cell in Children)
            {
                var status = SapperVM.CellStatus(SapperVM.GameGrid.Girds[(int)cell.GetValue(Grid.RowProperty), (int)cell.GetValue(Grid.ColumnProperty)]);

                switch (status)
                {
                    case " ":
                        cell.Background = Brushes.LightGreen;
                        break;
                    case "b":
                        cell.Background = Brushes.Red;
                        break;
                    case "f":
                        cell.Background = Brushes.Aqua;
                        break;
                    default:
                        cell.Background = Brushes.White;
                        cell.Content = status;
                        break;
                }
            }
        }
    }


}
