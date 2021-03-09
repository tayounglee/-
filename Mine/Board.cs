using System;
using System.Collections.Generic;
using System.Text;

namespace Study
{
    class Board : Control
    {
        Cell[,] cells; // ?

        public static int x, y;

        int mineNum;
        int flagnum = 0;
        bool lose = false;

        public Board(int x = 9, int y = 9, int num = 10)
        {
            Board.x = x;
            Board.y = y;
            mineNum = num;
            //cells = new Cell[y, x];
            Random r = new Random();

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    Cell.numMap[i, j] = 0; 
                    Cell.mineMap[i, j] = Cell.open[i, j] = false;
                }
            }

            for (int i = 0; i < mineNum; i++)
            {
                int sx = r.Next(0, x);
                int sy = r.Next(0, y);

                if (Cell.mineMap[sy, sx] == false)
                {
                    Cell.mineMap[sy, sx] = true;
                    Cell.numMap[sy, sx] = -1;

                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            if (dx == 0 && dy == 0) continue;

                            int lx = sx + dx;
                            int ly = sy + dy;
                            if (lx >= 0 && lx < x && ly >= 0 && ly < y)
                            {
                                if (Cell.mineMap[ly, lx] == true) { continue; }
                                Cell.numMap[ly, lx] += 1;
                            }
                        }
                    }
                    continue;
                }
                else { i--; }
            }
            /*
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    Console.Write("■");
                }
                Console.WriteLine();
            }
            */
        }
        public override void Show()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    if (Cell.open[i, j])
                    {
                        Console.SetCursorPosition(j * 2, i);
                        Console.Write(SelectShape(Cell.numMap[i, j]));
                    }
                    else { }
                }
            }
        }

        void PrintShape(int x, int y)
        {
            Console.SetCursorPosition(x * 2, y);
            Console.Write(SelectShape(Cell.numMap[y, x]));
        }

        string SelectShape(int s)
        {
            string[] shape = {
                "□","①","②","③","④","⑤","⑥","⑦","⑧"
            };
            switch (s)
            {
                case -1:
                    return "⊙";
                case -2:
                    return "★";
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 0:
                    return shape[s];
            }
            return "■";
        }

        public void Invest0(int sy, int sx)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue;

                    int lx = sx + dx;
                    int ly = sy + dy;
                    if (lx >= 0 && lx < x && ly >= 0 && ly < y)
                    {
                        if (Cell.open[ly, lx] == true) continue;
                        if (Cell.flag[ly, lx] == true) Cell.flag[ly, lx] = false;
                        Cell.open[ly, lx] = true;
                        if (Cell.numMap[ly, lx] == 0)
                        {
                            Invest0(ly, lx);

                        }
                        PrintShape(lx, ly);
                    }
                }
            }
        }

        public bool InvestFlag(Player p)
        {
            int sum = 0;
            int opened = 0;
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {

                    if (dx == 0 && dy == 0) continue;
                    int lx = p.Getx() + dx;
                    int ly = p.Gety() + dy;

                    if (AvailablePoint(lx, ly))
                    {
                        if (Cell.flag[ly, lx] == true)
                        {
                            sum++;
                        }
                        if (Cell.open[ly, lx]) opened++;
                    }
                }
            }
            if (opened == 8 - Cell.numMap[p.Gety(), p.Getx()])
            {
                return false;
            }
            if (sum == Cell.numMap[p.Gety(), p.Getx()])
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        int lx = p.Getx() + dx;
                        int ly = p.Gety() + dy;
                        if (dx == 0 && dy == 0) continue;

                        if (AvailablePoint(lx, ly))
                        {
                            if (!Cell.flag[ly, lx] && !Cell.open[ly, lx])
                            {
                                Cell.open[ly, lx] = true;
                                Console.SetCursorPosition(lx * 2, ly);
                                Console.Write(SelectShape(Cell.numMap[ly, lx]));
                            }
                            else { continue; }

                            if (Cell.numMap[ly, lx] == 0)
                            {
                                Invest0(ly, lx);
                            }
                            else if (Cell.mineMap[ly, lx] == true)
                            {
                                for (int i = 0; i < y; i++)
                                {
                                    for (int j = 0; j < x; j++)
                                    {
                                        Cell.open[i, j] = true;
                                        if (Cell.flag[i, j] == true && Cell.numMap[i, j] != -1)
                                        {
                                            Cell.flag[i, j] = false;
                                        }
                                    }
                                }
                                lose = true;
                                return true;
                            }
                        }
                    }
                }
            }
            lose = false;
            return false;
        }

        public bool WinCheck()
        {
            int sum = 0;
            for (int i = 0; i < Board.y; i++)
            {
                for (int j = 0; j < Board.x; j++)
                {
                    if (Cell.open[i, j] == false) sum++;
                }
            }

            if (mineNum == sum)
            {
                return true;
            }
            else return false;
        }

        public bool AvailablePoint(int x, int y)
        {
            if (x >= 0 && x < Board.x && y >= 0 && y < Board.y)
            {
                return true;
            }
            else return false;
        }

        public bool Receive(Player p)
        {
            int dx = 0, dy = 0;
            bool move = false;
            Mine mine = new Mine();

            switch (p.Send())
            {
                case "left":
                    dx = -1;
                    move = true;
                    break;
                case "right":
                    dx = 1;
                    move = true;
                    break;
                case "up":
                    dy = -1;
                    move = true;
                    break;
                case "down":
                    dy = 1;
                    move = true;
                    break;
                case "enter":
                    if (Cell.open[p.Gety(), p.Getx()] == true && Cell.numMap[p.Gety(), p.Getx()] != 0)
                    {
                        InvestFlag(p);
                    }
                    else if (!Cell.flag[p.Gety(), p.Getx()])
                    {
                        Cell.open[p.Gety(), p.Getx()] = true;
                        PrintShape(p.Getx(), p.Gety());
                    }
                    else { return true; }

                    if (Cell.numMap[p.Gety(), p.Getx()] == 0)
                    {
                        Invest0(p.Gety(), p.Getx());
                    }
                    else if (Cell.numMap[p.Gety(), p.Getx()] == -1 || lose)
                    {
                        for (int i = 0; i < y; i++)
                        {
                            for (int j = 0; j < x; j++)
                            {
                                Cell.open[i, j] = true;
                                if (Cell.flag[i, j] == true && Cell.numMap[i, j] != -1)
                                {
                                    Cell.flag[i, j] = false;
                                }
                            }
                        }
                        Show();
                        if(Mine.select == 1)
                        {
                            Console.SetCursorPosition(1, 12);
                            Console.Write("Game Over");
                            Console.SetCursorPosition(1, 13);
                            Console.Write("Please press Enter");

                        }
                        else if(Mine.select == 2)
                        {
                            Console.SetCursorPosition(10, 20);
                            Console.Write("Game Over");
                            Console.SetCursorPosition(10, 21);
                            Console.Write("Please press Enter");
                        }
                        else
                        {
                            Console.SetCursorPosition(30, 26);
                            Console.Write("Game Over");
                            Console.SetCursorPosition(30, 27);
                            Console.Write("Please press Enter");
                        }
                        
                        while (Console.ReadKey(true).KeyChar != (char)13) { }
                        Console.Clear();
                        return false;
                    }
                    if (WinCheck())
                    {
                        for (int i = 0; i < y; i++)
                        {
                            for (int j = 0; j < x; j++)
                            {
                                Cell.open[i, j] = true;
                                Cell.flag[i, j] = false;
                                if (Cell.numMap[i, j] == -1)
                                {
                                    Cell.numMap[i, j] = -2;
                                }
                            }
                        }
                        Show();
                        if (Mine.select == 1)
                        {
                            Console.SetCursorPosition(1, 12);
                            Console.Write("You Win!!");
                            Console.SetCursorPosition(1, 13);
                            Console.Write("Please press Enter");

                        }
                        else if (Mine.select == 2)
                        {
                            Console.SetCursorPosition(10, 20);
                            Console.Write("You Win!!");
                            Console.SetCursorPosition(10, 21);
                            Console.Write("Please press Enter");
                        }
                        else
                        {
                            Console.SetCursorPosition(30, 26);
                            Console.Write("You Win!!");
                            Console.SetCursorPosition(30, 27);
                            Console.Write("Please press Enter");
                        }
                        while (Console.ReadKey(true).KeyChar != (char)13) { };
                        Console.Clear();
                        return false;
                    }

                    break;

                case "Cell.flag":
                    if (!Cell.open[p.Gety(), p.Getx()])
                        switch (Cell.flag[p.Gety(), p.Getx()])
                        {
                            case true:
                                Cell.flag[p.Gety(), p.Getx()] = false;
                                flagnum--;
                                break;
                            case false:
                                Cell.flag[p.Gety(), p.Getx()] = true;
                                flagnum++;
                                break;
                        }
                    Console.SetCursorPosition(18, 26);
                    Console.Write("     ");
                    Console.SetCursorPosition(1, 26);
                    Console.Write("남은 지뢰 갯수: {0}", mineNum - flagnum);
                    break;
                case "quit":
                    Console.Clear();
                    return false;
            }
            if (move)
            {
                Console.SetCursorPosition((p.Getx() - dx) * 2, (p.Gety() - dy));

                if (!Cell.open[p.Gety() - dy, p.Getx() - dx])
                {
                    if (Cell.flag[p.Gety() - dy, p.Getx() - dx] == false)
                    {
                        Console.Write("■");
                    }
                    else
                    {
                        Console.Write("♠");
                    }
                }
                else Console.Write(SelectShape(Cell.numMap[p.Gety() - dy, p.Getx() - dx]));

                p.Show();
            }
            return true;
        }
    }
}