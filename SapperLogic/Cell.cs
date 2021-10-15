using System.Collections.Generic;

namespace SapperLogic
{
    /// <summary>
    /// Class Cell
    /// </summary>
    public class Cell : ICell
    {
        public bool Flag { get; set; }
        public int Around { get; set; }
        public bool Bomb { get; set; }
        public bool Check { get; set; }
        public int Y { get; set; }
        public int X { get; set; }
        public int Id { get; set; }
        /// <summary>
        /// Constructor Cell 
        /// </summary>
        /// <param name="around">Bombs nearby </param>
        /// <param name="check">This Cell is Open?</param>
        /// <param name="bomb">This Cell is Bomb?</param>
        /// <param name="y">Coordinate fo y </param>
        /// <param name="x">Coordinate fo x </param>
        public Cell(int around, bool check, bool bomb, int y, int x,int id)
        {
            Around = around;
            Bomb = bomb;
            Check = check;
            Y = y;
            X = x;
            Flag = false;
            Id = id;
        }
        public override string ToString()
        {
            return " My Flag is " + Flag + " My Around is " + Around + " My Bomb is " + Bomb + " My Check is " + Check + " My Y is " + Y + " My X is " + X;
        }
    }
}
