using System;
using System.Collections.Generic;
using System.Text;

namespace Study
{
    class Board : Control
    {
        Cell[,] cells;

        public static int x, y;

        int mineNum;
        int flagnum = 0;
        bool lose = false;

        //Cell cell = new Cell();

        public Board(int x = 9, int y = 9, int num = 10)
        {
            Board.x = x;
            Board.y = y;
            mineNum = num;

            cells = new Cell[y, x];
            //cells[x - 1, y - 1] = new Cell();
            Random r = new Random();

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    cells[i, j] = new Cell();
                    cells[i, j].numMap = 0;
                    cells[i, j].mineMap = cells[i, j].open = false;
                }
            }

            for (int i = 0; i < mineNum; i++)
            {
                int sx = r.Next(0, x);
                int sy = r.Next(0, y);

                if (cells[sy, sx].mineMap == false)
                {
                    cells[sy, sx].mineMap = true;
                    cells[sy, sx].numMap = -1;

                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            if (dx == 0 && dy == 0) continue;

                            int lx = sx + dx;
                            int ly = sy + dy;
                            if (lx >= 0 && lx < x && ly >= 0 && ly < y)
                            {
                                if (cells[ly, lx].mineMap == true) { continue; }
                                cells[ly, lx].numMap += 1;
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
                    if (cells[i, j].open)
                    {
                        Console.SetCursorPosition(j * 2, i);
                        Console.Write(SelectShape(cells[i, j].numMap));
                    }
                    else { }
                }
            }
        }

        void PrintShape(int x, int y)
        {
            Console.SetCursorPosition(x * 2, y);
            Console.Write(SelectShape(cells[y, x].numMap));
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
                        if (cells[ly, lx].open == true) continue;
                        if (cells[ly, lx].flag == true) cells[ly, lx].flag = false;
                        cells[ly, lx].open = true;
                        if (cells[ly, lx].numMap == 0)
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
                        if (cells[ly, lx].flag == true)
                        {
                            sum++;
                        }
                        if (cells[ly, lx].open) opened++;
                    }
                }
            }
            if (opened == 8 - cells[p.Gety(), p.Getx()].numMap)
            {
                return false;
            }
            if (sum == cells[p.Gety(), p.Getx()].numMap)
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
                            if (!cells[ly, lx].flag && !cells[ly, lx].open)
                            {
                                cells[ly, lx].open = true;
                                Console.SetCursorPosition(lx * 2, ly);
                                Console.Write(SelectShape(cells[ly, lx].numMap));
                            }
                            else { continue; }

                            if (cells[ly, lx].numMap == 0)
                            {
                                Invest0(ly, lx);
                            }
                            else if (cells[ly, lx].mineMap == true)
                            {
                                for (int i = 0; i < y; i++)
                                {
                                    for (int j = 0; j < x; j++)
                                    {
                                        cells[i, j].open = true;
                                        if (cells[i, j].flag == true && cells[i, j].numMap != -1)
                                        {
                                            cells[i, j].flag = false;
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
                    if (cells[i, j].open == false) sum++;
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
                    if (cells[p.Gety(), p.Getx()].open == true && cells[p.Gety(), p.Getx()].numMap != 0)
                    {
                        InvestFlag(p);
                    }
                    else if (!cells[p.Gety(), p.Getx()].flag)
                    {
                        cells[p.Gety(), p.Getx()].open = true;
                        PrintShape(p.Getx(), p.Gety());
                    }
                    else { return true; }

                    if (cells[p.Gety(), p.Getx()].numMap == 0)
                    {
                        Invest0(p.Gety(), p.Getx());
                    }
                    else if (cells[p.Gety(), p.Getx()].numMap == -1 || lose)
                    {
                        for (int i = 0; i < y; i++)
                        {
                            for (int j = 0; j < x; j++)
                            {
                                cells[i, j].open = true;
                                if (cells[i, j].flag == true && cells[i, j].numMap != -1)
                                {
                                    cells[i, j].flag = false;
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
                                cells[i, j].open = true;
                                cells[i, j].flag = false;
                                if (cells[i, j].numMap == -1)
                                {
                                    cells[i, j].numMap = -2;
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

                case "flag":
                    if (!cells[p.Gety(), p.Getx()].open)
                        switch (cells[p.Gety(), p.Getx()].flag)
                        {
                            case true:
                                cells[p.Gety(), p.Getx()].flag = false;
                                flagnum--;
                                break;
                            case false:
                                cells[p.Gety(), p.Getx()].flag = true;
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

                if (!cells[p.Gety() - dy, p.Getx() - dx].open)
                {
                    if (cells[p.Gety() - dy, p.Getx() - dx].flag == false)
                    {
                        Console.Write("■");
                    }
                    else
                    {
                        Console.Write("♠");
                    }
                }
                else Console.Write(SelectShape(cells[p.Gety() - dy, p.Getx() - dx].numMap));

                p.Show();
            }
            return true;
        }
    }
}