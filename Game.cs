using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip
{
    public class Game
    {
        private Map Player1 { get; } = new Map();
        private Map Player2 { get; } = new Map();

        enum State {  MainMenu, PlaceShips, Player1Turn, Player2Turn, GameOver }


    }
}
