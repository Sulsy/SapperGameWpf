using System;

namespace SapperLogic
{
    /// <summary>
    /// Interface for Cell Class
    /// </summary>
    public interface ICell
    {
        public bool Flag { get; set; }
        public int Around { get; set; }
        public bool Bomb { get; set; }
        public bool Check { get; set; }
        public int Y { get; set; }
        public int X { get; set; }
        public int Id { get; set; }

    }

}
