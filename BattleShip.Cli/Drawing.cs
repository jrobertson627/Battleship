using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip
{
    public static class Drawing
    {
        public struct DrawingParams
        {
            public ConsoleColor HitColor { get; set; }
            public ConsoleColor MissColor { get; set; }
            public bool ShowHits { get; set; }
            public bool ShowShips { get; set; }
        }
        public static void DrawMap(Map m, int originX, int originY, string title, DrawingParams @params)
        {
            Console.SetBufferSize(80, 300);

            Console.SetCursorPosition(originX, originY);
            Console.WriteLine(title);
            Console.WriteLine();
            originY += 2;


            // draw top line
            for (int y = originY; y < originY+m.Height+1; ++y)
            {
                Console.SetCursorPosition(originX, y);

                for (int x = originX; x < originX+m.Width+1; ++x)
                {
                    if (y == originY)
                    {
                        if (x == originX)
                        {
                            Console.Write("  ");
                        }
                        if (x == originX + m.Width)
                            continue;
                        Console.Write(' ');
                        Console.Write((char)('A' + (x - originX)));
                        Console.Write(' ');
                    }
                    else
                    {
                        if (x == originX)
                        {
                            Console.Write((char)('0' + (y - originY)));
                            Console.Write(' ');
                        }
                        else
                        {
                            int posY = y - (originY+1);
                            int posX = x - (originX+1);
                            bool occupied = m.IsOccupied(posX, posY);
                            bool hit = m.IsHit(posX, posY);
                            if (occupied && hit && @params.ShowHits)
                            {
                                var color = Console.ForegroundColor;
                                Console.Write('[');
                                Console.ForegroundColor = @params.HitColor;
                                Console.Write('X');
                                Console.ForegroundColor = color;
                                Console.Write(']');
                            }
                            else if (!occupied && hit && @params.ShowHits)
                            {
                                var color = Console.ForegroundColor;
                                Console.Write('[');
                                Console.ForegroundColor = @params.MissColor;
                                Console.Write('X');
                                Console.ForegroundColor = color;
                                Console.Write(']');
                            }
                            else if (m.IsOccupied(posX, posY) && @params.ShowShips)
                                Console.Write("[O]");
                            else
                                Console.Write("[ ]");
                        }
                        
                       
                    }
                }

                Console.WriteLine();
            }
            
        }
    }
}
