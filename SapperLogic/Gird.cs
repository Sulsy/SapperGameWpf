using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SapperLogic
{
    /// <summary>
    /// Singleton  Grid
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// Does an instance of the class exist
        /// </summary>
        public static bool State { get; set; }

        /// <summary>
        /// Reference field to the current class object
        /// </summary>
        private static Grid _instance;

        /// <summary>
        /// Static Grid size
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// First open Cells flag
        /// </summary>
        private bool FirstOpen { get; set; }

        /// <summary>
        /// Count Bomb fo  Grid
        /// </summary>
        public int CountBombs { get; set; }

        /// <summary>
        /// 2D array Grid of Cell objects
        /// </summary>
        public Cell[,] Girds { get; private set; }

        /// <summary>
        /// List of Cell that are Bobmbs
        /// </summary>
        public List<Cell> ListBomb { get; set; }

        /// <summary>
        /// List fo Flags Cells Players
        /// </summary>
        public List<Cell> ListFlag { get; set; }

        /// <summary>
        /// Singleton Constructor
        /// </summary>
        /// <param name="bombs">Count Bombs</param>
        /// <param name="size">Size Grid</param>
        private Grid(int bombs, int size)
        {
            CountBombs = bombs;
            Size = size;
            State = true;
        }

        /// <summary>
        /// Generating a grid of cells and randomly generating bomb slots
        /// </summary>
        private static void GenerateGirds()
        {
            var ID = new HashSet<int>();
            _instance.ListBomb = new List<Cell>();
            _instance.ListFlag = new List<Cell>();
            _instance.Girds = new Cell[_instance.Size, _instance.Size];
            GenerateGirdsManipulation.RandomID(ref ID);
            GenerateGirdsManipulation.GridsInit();
            GenerateGirdsManipulation.BombsFoCells(ref ID);
            GenerateGirdsManipulation.AroundAllGrid();
        }

        /// <summary>
        /// Method for creating / returning a reference to this class instance
        /// </summary>
        /// <returns>A reference to the new / current instance of the class</returns>
        public static Grid GetInstance()
        {
            const int Default = 3;
            if (Grid._instance != null) return Grid._instance;
            _instance = new Grid(Default, Default)
            {
                FirstOpen = true
            }; 
            GenerateGirds();
            return Grid._instance;
        }

        /// <summary>
        /// Method for creating / returning a reference to this class instance
        /// </summary>
        /// <param name="bombs">Count Bombs</param>
        /// <param name="size">Size Grid</param>
        /// <returns>A reference to the new / current instance of the class</returns>
        public static Grid GetInstance(int bombs, int size)
        {
            if (_instance == null)
            {
                _instance = new Grid(bombs, size)
                {
                    FirstOpen = true
                };
                GenerateGirds();
            }
            else
            {
                _instance.CountBombs = bombs;
                _instance.Size = size;
            }

            return _instance;
        }

        public static void Clean()
        {
            _instance = null;
        }

        /// <summary>
        /// Put Flag on Cell
        /// </summary>
        /// <param name="y">Coordinate fo y </param>
        /// <param name="x">Coordinate fo x</param>
        public bool? Flag(int y, int x)
        {
            if (!Girds[y, x].Flag && !Girds[y, x].Check)
            {
                Girds[y, x].Flag = true;
                ListFlag.Add(Girds[y, x]);
            }
            else
            {
                Girds[y, x].Flag = false;
                ListFlag.Remove(Girds[y, x]);
            }

            return GameOperator.GameState(Girds[y, x]);



        }

        /// <summary>
        /// Put Flag on Cell
        /// </summary>
        /// <param name="cell">Cell fo flag </param>
        public bool? Flag(Cell cell)
        {
            int x = cell.X, y = cell.Y;
            return Flag(x, y);
        }

        /// <summary>
        /// Cell opening
        /// </summary>
        /// <param name="y">Coordinate fo y </param>
        /// <param name="x">Coordinate fo x</param>
        public bool? OpenCell(int y, int x)
        {
            if (FirstOpen && _instance.Girds[y, x].Bomb)
            {
                GenerateGirdsManipulation.FirstCellOpen(y, x);
                
            }
            _instance.FirstOpen = false;


            if (Girds[y, x].Check)
            {
                var flags = Around.AroundFlag(y, x);
                if (flags == _instance.Girds[y, x].Around)
                {
                    Around.AroundOpen(y, x);
                }
            }
            else
            {
                if (Girds[y, x].Flag) return GameOperator.GameState(Girds[y,x]);
                Around.AroundNull(y, x);
                Girds[y, x].Check = true;

            }

            return GameOperator.GameState(Girds[y, x]);
        }

        /// <summary>
        /// Cell opening
        /// </summary>
        /// <param name="cell">Cell fo open</param>
        public bool? OpenCell(Cell cell)
        {
            int x = cell.X, y = cell.Y;
            return OpenCell(x, y);

        }
    }

   
}