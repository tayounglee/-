using System;

namespace Study
{
    public class Mine
    {
        public static int select;
        public static void Main(string[] args)
        {
            Player p = new Player();
            Board board = new Board();
            Console.CursorVisible = false;
            int minenum = 0;
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("┌───────────────────────────────────┐");
                Console.WriteLine("│       난이도를 설정해주세요.      │");
                Console.WriteLine("│                                   │");
                Console.WriteLine("│                                   │");
                Console.WriteLine("│            1.쉬움(9x9)            │");
                Console.WriteLine("│            2.보통(19x19)          │");
                Console.WriteLine("│            3.어려움(39x24)        │");
                Console.WriteLine("│            4.설명                 │");
                Console.WriteLine("│            5.종료                 │");
                Console.WriteLine("│                                   │");
                Console.WriteLine("└───────────────────────────────────┘");

                select = Convert.ToInt32(Console.ReadLine());

                switch (select)
                {
                    case 1:
                        minenum = 10;
                        board = new Board(9, 9, 10);
                        break;
                    case 2:
                        minenum = 51;
                        board = new Board(19, 19, 51);
                        break;
                    case 3:
                        minenum = 111;
                        board = new Board(39, 24, 111);
                        break;
                        /*
                    case 4:
                        Console.WriteLine("가로(9~39): ");
                        int x = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("세로(9~24): ");
                        int y = Convert.ToInt32(Console.ReadLine());
                        minenum = x * y / 7;
                        board = new Board(x, y, minenum);
                        break;
                        */
                    case 4:
                        Console.Clear();
                        Console.WriteLine("┌───────────────────────────────────┐");
                        Console.WriteLine("│                                   │");
                        Console.WriteLine("│                                   │");
                        Console.WriteLine("│                                   │");
                        Console.WriteLine("│          ←→↑↓: 방향키         │");
                        Console.WriteLine("│          a: 선택, s:플래그        │");
                        Console.WriteLine("│          q: 나가기                │");
                        Console.WriteLine("│         Please press Enter        │");
                        Console.WriteLine("│                                   │");
                        Console.WriteLine("│                                   │");
                        Console.WriteLine("└───────────────────────────────────┘");
                        while (Console.ReadKey(true).KeyChar != (char)13){ };
                        continue;
                    case 5:
                        return;

                }
                p.setx(Board.x / 2);
                p.sety(Board.y / 2);
                Console.Clear();

                for (int i = 0; i < Board.y; i++)
                {
                    for (int j = 0; j < Board.x; j++)
                    {
                        
                        Console.Write("■");
                    }
                    Console.WriteLine();
                }
                p.show();

                Console.SetCursorPosition(1, 26);
                Console.Write("남은 지뢰 갯수: {0}", minenum);

                while (true)
                {
                    if (board.receive(p.input(Console.ReadKey(true).Key)) == false)
                    {
                        break;
                    }
                }
            }
        }
    }
}