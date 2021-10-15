using System;

namespace Sapper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Gird g = Gird.GetInstance(12);
        }
    }

    public interface ICell
    {
        public int Around { get; set; }
        public bool Bomb { get; set; }  
        public bool Check { get; set; }
        public int Y { get; set; }
        public int X { get; set; }
    }

    public class Cell : ICell
    {
        public int Around { get; set; }
        public bool Bomb { get; set; }
        public bool Check { get; set; }
        public int Y { get; set; }
        public int X { get; set; }

        public Cell(int around, bool check,bool bomb,int y,int x)
        {
            Around = around;
            Bomb = bomb;
            Check = check;
            Y = y;
            X = x;
        }
    }

    public class Gird
    {
        private static Gird instance;
        public int Size { get; private set; }
        public static Cell[,] Girds { get; private set; }  

        private Gird(int size)
        {
            this.Size = size;
            GenerateGirds(size);
            OutPut();
        }
        public static Gird GetInstance(int size)
        {
            if (instance==null)
                instance = new Gird(size);

            return instance;
        }

        private static void GenerateGirds(int size)
        {
            var rand = new Random();

            Girds= new Cell[size,size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (rand.Next()%4==0)
                    {
                        Girds[i, j] = new Cell(0, false,true, i, j);
                    }
                    else
                    {
                        Girds[i, j] = new Cell(0, false, false, i, j);
                    }
                  
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (!Girds[i,j].Bomb)
                    {
                        Girds[i, j].Around = AroundBomb(size, i, j);

                    }
                }
            }
        }

        private static int AroundBomb(int size,int y,int x)
        {
            
            int count=0;
            for (int i = y - 1; i < y+2; i++)
            {
                for (int j = x - 1; j < x+2; j++)
                {
                    if (size > i && size> j && i>=0 && j>=0 && Girds[i, j].Bomb)
                    {
                        count++;
                    }

                }
            }

            return count;
        }

        public void OutPut()
        {
            for (int i = 0; i <Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Console.Write($"   {Girds[i,j].Around}  {Girds[i, j].Bomb}  ");
                }   
                Console.WriteLine();
            }
        }
        public void OpenCell(int y, int x)
        {
            //Girds[y, x] = ;
        }
    }
}
