using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SapperUI.Game_ViewModel;

namespace SapperUI
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Generate Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Start_Click(object sender, RoutedEventArgs e)
        {
            SapperVM.DifficultyParser(Difficulty.SelectedItem.ToString().ToLower());
            SapperVM.Clean();
            SapperVM.GameGrid = SapperLogic.Grid.GetInstance(SapperVM.BombCount, SapperVM.sizeGrid);
            m_Grid.ColumnDefinitions.Clear();
            m_Grid.RowDefinitions.Clear();
            ButtonGenerate();
            BombRemain.Content = SapperVM.BombCount - SapperVM.GameGrid.ListFlag.Count(x => x.Flag);
        }
        /// <summary>
        /// Button Generate on Grid
        /// </summary>
        private void ButtonGenerate()
        {
            for (int x = 0; x < SapperVM.sizeGrid; x++)
            {
                m_Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star), });
            }

            for (int y = 0; y < SapperVM.sizeGrid; y++)
            {
                m_Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star), });
            }
            for (int x = 0; x < SapperVM.sizeGrid; x++)
            {
                for (int y = 0; y < SapperVM.sizeGrid; y++)
                {
                    var rect = new Button();
                    rect.Click += Button_OpenCell;
                    rect.MouseDown += Button_SetFlag;
                    rect.SetValue(Grid.RowProperty, y);
                    rect.SetValue(Grid.ColumnProperty, x);

                    m_Grid.Children.Add(rect);
                }
            }
            SapperVM.CheckGridState(m_Grid.Children);
            m_Grid.IsEnabled = true;
        }
        /// <summary>
        /// Handler Button of Opening Cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Button_OpenCell(object sender, RoutedEventArgs args)
        {
            m_Grid.IsEnabled= SapperVM.OpenCell(sender, m_Grid.Children);
            BombRemain.Content = SapperVM.BombCount - SapperVM.GameGrid.ListFlag.Count(x => x.Flag);
        }
        /// <summary>
        /// Handler Button of Opening Flag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Button_SetFlag(object sender, MouseButtonEventArgs args)
        {
            SapperVM.SetFlag(sender, m_Grid.Children);
            BombRemain.Content = SapperVM.BombCount - SapperVM.GameGrid.ListFlag.Count(x => x.Flag);
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
