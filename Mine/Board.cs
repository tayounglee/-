using System;
using System.Collections.Generic;
using System.Text;

namespace Study
{
    class Board : Control
    {
        public static int x, y;
        int mineNum;
        int[,] numMap;
        bool[,] open;
        bool[,] mineMap;
        bool[,] flag;
        int flagnum = 0;
        bool lose = false;

        public Board(int x = 9, int y = 9, int num = 10)
        {
            Board.x = x;
            Board.y = y;
            mineNum = num;
            numMap = new int[y, x];
            mineMap = new bool[y, x];
            open = new bool[y, x];
            flag = new bool[y, x];

            Random r = new Random();

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    numMap[i, j] = 0;
                    mineMap[i, j] = open[i, j] = false;
                }
            }

            for (int i = 0; i < mineNum; i++)
            {
                int sx = r.Next(0, x);
                int sy = r.Next(0, y);

                if (mineMap[sy, sx] == false)
                {
                    mineMap[sy, sx] = true;
                    numMap[sy, sx] = -1;

                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            if (dx == 0 && dy == 0) continue;

                            int lx = sx + dx;
                            int ly = sy + dy;
                            if (lx >= 0 && lx < x && ly >= 0 && ly < y)
                            {
                                if (mineMap[ly, lx] == true) { continue; }
                                numMap[ly, lx] += 1;
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
        public override void show()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    if (open[i, j])
                    {
                        Console.SetCursorPosition(j * 2, i);
                        Console.Write(selectShape(numMap[i, j]));
                    }
                    else { }
                }
            }
        }

        void printShape(int x, int y)
        {
            Console.SetCursorPosition(x * 2, y);
            Console.Write(selectShape(numMap[y, x]));
        }

        string selectShape(int s)
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

        public void invest0(int sy, int sx)
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
                        if (open[ly, lx] == true) continue;
                        if (flag[ly, lx] == true) flag[ly, lx] = false;
                        open[ly, lx] = true;
                        if (numMap[ly, lx] == 0)
                        {
                            invest0(ly, lx);

                        }
                        printShape(lx, ly);
                    }
                }
            }
        }

        public bool investFlag(Player p)
        {
            int sum = 0;
            int opened = 0;
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {

                    if (dx == 0 && dy == 0) continue;
                    int lx = p.getx() + dx;
                    int ly = p.gety() + dy;

                    if (availablePoint(lx, ly))
                    {
                        if (flag[ly, lx] == true)
                        {
                            sum++;
                        }
                        if (open[ly, lx]) opened++;
                    }
                }
            }
            if (opened == 8 - numMap[p.gety(), p.getx()])
            {
                return false;
            }
            if (sum == numMap[p.gety(), p.getx()])
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        int lx = p.getx() + dx;
                        int ly = p.gety() + dy;
                        if (dx == 0 && dy == 0) continue;

                        if (availablePoint(lx, ly))
                        {
                            if (!flag[ly, lx] && !open[ly, lx])
                            {
                                open[ly, lx] = true;
                                Console.SetCursorPosition(lx * 2, ly);
                                Console.Write(selectShape(numMap[ly, lx]));
                            }
                            else { continue; }

                            if (numMap[ly, lx] == 0)
                            {
                                invest0(ly, lx);
                            }
                            else if (mineMap[ly, lx] == true)
                            {
                                for (int i = 0; i < y; i++)
                                {
                                    for (int j = 0; j < x; j++)
                                    {
                                        open[i, j] = true;
                                        if (flag[i, j] == true && numMap[i, j] != -1)
                                        {
                                            flag[i, j] = false;
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

        public bool winCheck()
        {
            int sum = 0;
            for (int i = 0; i < Board.y; i++)
            {
                for (int j = 0; j < Board.x; j++)
                {
                    if (open[i, j] == false) sum++;
                }
            }

            if (mineNum == sum)
            {
                return true;
            }
            else return false;
        }

        public bool availablePoint(int x, int y)
        {
            if (x >= 0 && x < Board.x && y >= 0 && y < Board.y)
            {
                return true;
            }
            else return false;
        }

        public bool receive(Player p)
        {
            int dx = 0, dy = 0;
            bool move = false;
            Mine mine = new Mine();

            switch (p.send())
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
                    if (open[p.gety(), p.getx()] == true && numMap[p.gety(), p.getx()] != 0)
                    {
                        investFlag(p);
                    }
                    else if (!flag[p.gety(), p.getx()])
                    {
                        open[p.gety(), p.getx()] = true;
                        printShape(p.getx(), p.gety());
                    }
                    else { return true; }

                    if (numMap[p.gety(), p.getx()] == 0)
                    {
                        invest0(p.gety(), p.getx());
                    }
                    else if (numMap[p.gety(), p.getx()] == -1 || lose)
                    {
                        for (int i = 0; i < y; i++)
                        {
                            for (int j = 0; j < x; j++)
                            {
                                open[i, j] = true;
                                if (flag[i, j] == true && numMap[i, j] != -1)
                                {
                                    flag[i, j] = false;
                                }
                            }
                        }
                        show();
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
                    if (winCheck())
                    {
                        for (int i = 0; i < y; i++)
                        {
                            for (int j = 0; j < x; j++)
                            {
                                open[i, j] = true;
                                flag[i, j] = false;
                                if (numMap[i, j] == -1)
                                {
                                    numMap[i, j] = -2;
                                }
                            }
                        }
                        show();
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
                    if (!open[p.gety(), p.getx()])
                        switch (flag[p.gety(), p.getx()])
                        {
                            case true:
                                flag[p.gety(), p.getx()] = false;
                                flagnum--;
                                break;
                            case false:
                                flag[p.gety(), p.getx()] = true;
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
                Console.SetCursorPosition((p.getx() - dx) * 2, (p.gety() - dy));

                if (!open[p.gety() - dy, p.getx() - dx])
                {
                    if (flag[p.gety() - dy, p.getx() - dx] == false)
                    {
                        Console.Write("■");
                    }
                    else
                    {
                        Console.Write("♠");
                    }
                }
                else Console.Write(selectShape(numMap[p.gety() - dy, p.getx() - dx]));

                p.show();
            }
            return true;
        }
    }
}