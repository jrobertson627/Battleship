using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip
{
    
    public class Ship
    {
        public Ship(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public int Width { get; }
        public int Height { get; }
        private int Health { get; set; }
    }
}
