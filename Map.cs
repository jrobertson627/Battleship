using BattleShip;
using System;
using System.Collections.Generic;

/// 
///  [ ][ ][ ][ ][ ][ ][ ][ ]
///  [ ][ ][ ][ ][ ][ ][ ][ ]
///  [ ][ ][ ][ ][ ][ ][ ][ ]
///  [ ][ ][ ][ ][ ][ ][ ][ ]
///  [ ][ ][ ][ ][ ][ ][ ][ ]
///  [ ][ ][ ][ ][ ][ ][ ][ ]
///  [ ][ ][ ][ ][ ][ ][ ][ ]
///  [ ][ ][ ][ ][ ][ ][ ][ ]
///  
///  Place [ X X X X ] horizontal 0,1
///  
///  [ ][ ][ ][ ][ ][ ][ ][ ]
///  [X][X][X][X][ ][ ][ ][ ]
///  [ ][ ][ ][ ][ ][ ][ ][ ]
///  [ ][ ][ ][ ][ ][ ][ ][ ]
///  [ ][ ][ ][ ][ ][ ][ ][ ]
///  [ ][ ][ ][ ][ ][ ][ ][ ]
///  [ ][ ][ ][ ][ ][ ][ ][ ]
///  [ ][ ][ ][ ][ ][ ][ ][ ]
///  
///  Place [ X X X X ] vertical 0,1
///  
///  [ ][ ][ ][ ][ ][ ][ ][ ]
///  [X][ ][ ][ ][ ][ ][ ][ ]
///  [X][ ][ ][ ][ ][ ][ ][ ]
///  [X][ ][ ][ ][ ][ ][ ][ ]
///  [X][ ][ ][ ][ ][ ][ ][ ]
///  [ ][ ][ ][ ][ ][ ][ ][ ]
///  [ ][ ][ ][ ][ ][ ][ ][ ]
///  [ ][ ][ ][ ][ ][ ][ ][ ]

public class Map
{
	public int Width { get; } = 8;
	public int Height { get; } = 8;
	public enum Orientation { Vertical, Horizontal };
	public class Cell
	{
		public Cell(int x, int y)
		{
			Row = x;
			Column = y;
		}
		public int Row { get; set; }
		public int Column { get; set; }
		public Ship Ship { get; set; }
		public bool IsHit { get; set; }
	}

	public Map()
	{
		// rows
		Board = new Cell[Width][];
		for (int i = 0; i < Width; ++i)
		{
			// columns
			Board[i] = new Cell[Height];
		}

		InitializeBoard();
	}

	public void InitializeBoard()
	{
		for (int x = 0; x < Width; ++x)
		{
			for (int y = 0; y < Height; ++y)
			{
				Board[x][y] = new Cell(x, y);
			}
		}
	}

	public void IterateCells(int startX, int startY, Func<Cell, bool> predicate)
	{
		AssertBounds(startX, startY);
		for (int x = 0; x < Width; ++x)
		{
			for (int y = 0; y < Height; ++y)
			{
				if (!predicate(Board[x][y]))
					return;
			}
		}
	}


	/// <summary>
	/// Returns whether a cell is occupied.
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <returns></returns>
	public bool IsOccupied(int x, int y)
	{
		AssertBounds(x, y);
		return Board[x][y].Ship != null;
	}

	public bool IsHit(int x, int y)
	{
		AssertBounds(x, y);
		return Board[x][y].IsHit;
	}

	/// <summary>
	/// Places a ship on the gameboard
	/// </summary>
	/// <param name="ship"></param>
	/// <param name="orientation"></param>
	/// <param name="position"></param>

	public void PlaceShip(Ship ship, Orientation orientation, int positionX, int positionY)
	{
		AssertBounds(positionX, positionY);	

		if (!WillFit(ship, orientation, positionX, positionY))
			throw new Exception("Ship won't fit there");

		int placementWidth = CalculatePlacementWidth(ship, orientation);
		int placementHeight = CalculatePlacementHeight(ship, orientation);

		for (int x = positionX; x < positionX+placementWidth; ++x)
		{
			for (int y = positionY; y < positionY+placementHeight; ++y)
			{
				Board[x][y].Ship = ship;
			}
		}

	}

	public void Attack(int positionX, int positionY, Weapon w)
	{
		AssertBounds(positionX, positionY);

		if (Board[positionX][positionY].IsHit)
			return;
		Board[positionX][positionY].IsHit = true;
		var ship = Board[positionX][positionY].Ship;
		if(ship != null)
			ship.Hit();

	}

	private bool InBounds(int positionX, int positionY)
	{
		return positionX >= 0 && positionX < Width && positionY >= 0 && positionY < Height;
	}

	private void AssertBounds(int positionX, int positionY)
	{
		if (!InBounds(positionX,positionY))
			throw new IndexOutOfRangeException("position out of bounds");
	}

	private bool WillFit(Ship ship, Orientation orientation, int positionX, int positionY)
	{
		int placementWidth = CalculatePlacementWidth(ship, orientation);
		int placementHeight = CalculatePlacementHeight(ship, orientation);

		if (positionX + placementWidth >= Width || positionY + placementHeight >= Height)
			return false; // the ship is too big for the placement

		for (int x = positionX; x < placementWidth; ++x)
		{
			for (int y = positionY; y < placementHeight; ++y)
			{
				if (IsOccupied(x, y))
					return false;
			}
		}
		return true;
	}

	private int CalculatePlacementWidth(Ship ship, Orientation orientation)
	{
		return orientation == Orientation.Vertical ? ship.Width : ship.Height;
	}

	private int CalculatePlacementHeight(Ship ship, Orientation orientation)
	{
		return orientation == Orientation.Vertical ? ship.Height : ship.Width;
	}


	private Cell[][] Board { get; }

}
