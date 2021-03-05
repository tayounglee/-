using System;
using System.Collections.Generic;
using System.Text;

namespace Study
{
    class Player : Control
    {
        int x, y;
        string shape = "나";
        string message = "";

        public Player(int x = 0, int y = 0)
        {
            this.x = x;
            this.y = y;
        }

        public override void show()
        {
            Console.SetCursorPosition(x * 2, y);
            Console.Write(shape);
        }

        public Player input(ConsoleKey key)
        {
            message = "";
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (y > 0)
                    {
                        y--;
                        message = "up";
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (y + 1 < Board.y)
                    {
                        y++;
                        message = "down";
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (x > 0)
                    {
                        x--;
                        message = "left";
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (x + 1 < Board.x)
                    {
                        x++;
                        message = "right";
                    }
                    break;
                    
                case ConsoleKey.S:
                    message = "flag";
                    break;

                case ConsoleKey.A:
                    message = "enter";
                    break;

                case ConsoleKey.Q:
                    message = "quit";
                    break;
                    
            }
            return this;
        }

        public int getx() { return x; }
        public int gety() { return y; }
        public void setx(int x) { this.x = x; }
        public void sety(int y) { this.y = y; }
        public string send()
        {
            return message;
        }
    }
}
