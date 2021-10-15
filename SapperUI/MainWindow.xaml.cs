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

namespace SapperUI
{
    public partial class MainWindow : Window
    {
        public SapperLogic.Grid GameGrid;

        private Int32 sizeGrid;
        private Int32 bombCount;

        public Int32 SizeGrid
        {
            get => sizeGrid;
            set => sizeGrid = value;
        }

        public Int32 BombCount
        {
            get => bombCount;
            set => bombCount = value;
        }

        private void DifficultyParser()
        {
            switch (Difficulty.SelectedItem.ToString().ToLower())
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


        public void Start_Click(object sender, RoutedEventArgs e)
        {
            DifficultyParser();
            SapperLogic.Grid.Clean();
            GameGrid = SapperLogic.Grid.GetInstance(BombCount, SizeGrid);
            m_Grid.Children.Clear();
            m_Grid.ColumnDefinitions.Clear();
            m_Grid.RowDefinitions.Clear();



            for (int x = 0; x < SizeGrid; x++)
            {
                m_Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star), });
            }

            for (int y = 0; y < SizeGrid; y++)
            {
                m_Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star), });
            }
            for (int x = 0; x < SizeGrid; x++)
            {
                for (int y = 0; y < SizeGrid; y++)
                {
                    var rect = new Button();
                    rect.Click += Button_OpenCell;
                    rect.MouseDown += Button_SetFlag;
                    rect.SetValue(Grid.RowProperty, y);
                    rect.SetValue(Grid.ColumnProperty, x);

                    m_Grid.Children.Add(rect);
                }
            }
            CheckGridState();
            m_Grid.IsEnabled = true;
        }

        private string CellStatus(SapperLogic.Cell cell) => !cell.Check ? cell.Flag ? "f" : " " : cell.Bomb ? "b" : cell.Around.ToString();

        private void Button_OpenCell(object sender, RoutedEventArgs args)
        {
            var cell = sender as Button;
            var ar= GameGrid.OpenCell((int)cell.GetValue(Grid.RowProperty), (int)cell.GetValue(Grid.ColumnProperty));
            CheckGridState();
            var IsWin = SapperLogic.GameOperator.GameState(GameGrid.Girds[(int)cell.GetValue(Grid.RowProperty), (int)cell.GetValue(Grid.ColumnProperty)]);

            if (IsWin != null && IsWin.Value) { MessageBox.Show("You Win!"); m_Grid.IsEnabled = false; }
            if (IsWin == null||ar==null) { MessageBox.Show("You Lose!"); m_Grid.IsEnabled = false; }
        }

        private void Button_SetFlag(object sender, MouseButtonEventArgs args)
        {
            var cell = sender as Button;
            GameGrid.Flag((int)cell.GetValue(Grid.RowProperty), (int)cell.GetValue(Grid.ColumnProperty));
            CheckGridState();
        }

        private void CheckGridState() {
            foreach (Button cell in m_Grid.Children)
            {
                var status = CellStatus(GameGrid.Girds[(int)cell.GetValue(Grid.RowProperty), (int)cell.GetValue(Grid.ColumnProperty)]);

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
            BombRemain.Content = BombCount - GameGrid.ListFlag.Where(x => x.Flag).Count();
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
