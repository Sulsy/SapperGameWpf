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

namespace SapperUI.ViewModel
{

    public partial class SapperVM : ContentControl
    {
        public static readonly DependencyProperty DifficultyProperty = DependencyProperty.Register(nameof(Difficulty), typeof(String), typeof(String), new PropertyMetadata(default(String)));

        public String Difficulty
        {
            get { return (String)GetValue(DifficultyProperty); }
            set { SetValue(DifficultyProperty, value); }
        }

        SapperLogic.Grid GameGrid;

        private Int32 sizeGrid;
        private Int32 bombCount;

        public Int32 SizeGrid {
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
            switch (Difficulty.ToLower()) 
            {
                default:
                    SizeGrid =10;
                    BombCount =10;
                    break;

                case "medium":
                    SizeGrid =18;
                    BombCount =40;
                    break;

                case "hard":
                    SizeGrid =24;
                    BombCount =99;
                    break;
            }
        }

        private ContentControl CreateTabble() 
        {
            DifficultyParser();
            GameGrid = SapperLogic.Grid.GetInstance(BombCount, SizeGrid);


            return null;
        }

        public SapperVM()
        {
            Content = CreateTabble();
            InitializeComponent();

        }
    }
}
