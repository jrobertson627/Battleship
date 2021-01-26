using System;

namespace BattleShip.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Map m = new Map();
            Ship s = new Ship(1, 4);
            Ship s2 = new Ship(1, 2);

            m.PlaceShip(s, Map.Orientation.Vertical, 0, 1);
            m.PlaceShip(s2, Map.Orientation.Horizontal, 3, 0);

            m.Attack(0, 1, new Weapon());
            m.Attack(2, 1, new Weapon());

            Drawing.DrawingParams playerParams = new Drawing.DrawingParams() { HitColor = ConsoleColor.Red, MissColor = ConsoleColor.Green, ShowShips = true, ShowHits = true };
            Drawing.DrawingParams computerParams = new Drawing.DrawingParams() { HitColor = ConsoleColor.Green, MissColor = ConsoleColor.Red, ShowShips = false, ShowHits = true };

            Drawing.DrawMap(m, 4, 2, "Player 1", playerParams);
            Drawing.DrawMap(m, 36, 2, "Computer", computerParams);
        }
    }
}
