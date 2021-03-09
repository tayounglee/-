using System;
using System.Collections.Generic;
using System.Text;

namespace Study
{
    class Cell
    {
        public static int[,] numMap = new int[Board.y, Board.x];
        public static bool[,] open = new bool[Board.y, Board.x];
        public static bool[,] mineMap = new bool[Board.y, Board.x];
        public static bool[,] flag = new bool[Board.y, Board.x];

        public Cell()
        {
            
        }
    }
}
