using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BattleShip.Test
{
    [TestClass]
    public class TestMap
    {
        /// <summary>
        /// Test that the map initializes, and every cell is "empty"
        /// </summary>
        [TestMethod]
        public void TestMap_Initializes()
        {
            Map m = new Map();

            m.IterateCells(0, 0, (cell) =>
            {
                Assert.IsNull(cell.Ship);
                return true;
            });
        }

        [TestMethod]
        public void TestMap_ShipPlacement_HappyVertical()
        {
            Map m = new Map();
            Ship ship = new Ship(1, 4);

            m.PlaceShip(ship, Map.Orientation.Vertical, 0, 1);

            Assert.IsTrue(m.IsOccupied(0, 1));
            Assert.IsTrue(m.IsOccupied(0, 2));
            Assert.IsTrue(m.IsOccupied(0, 3));
            Assert.IsTrue(m.IsOccupied(0, 4));
        }

        [TestMethod]
        public void TestMap_ShipPlacement_HappyHorizontal()
        {
            Map m = new Map();
            Ship ship = new Ship(1, 4);


            m.PlaceShip(ship, Map.Orientation.Horizontal, 0, 1);
            Assert.IsTrue(m.IsOccupied(0, 1));
            Assert.IsTrue(m.IsOccupied(1, 1));
            Assert.IsTrue(m.IsOccupied(2, 1));
            Assert.IsTrue(m.IsOccupied(3, 1));
        }

        [TestMethod]
        public void TestMap_ShipPlacement_OutOfBounds()
        {
            Map m = new Map();
            Ship ship = new Ship(1, 4);

            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            {
                m.PlaceShip(ship, Map.Orientation.Horizontal, -1, 0);
            });

            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            {
                m.PlaceShip(ship, Map.Orientation.Horizontal, 0, -1);
            });

            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            {
                m.PlaceShip(ship, Map.Orientation.Horizontal, 8, 0);
            });

            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            {
                m.PlaceShip(ship, Map.Orientation.Horizontal, 0, 8);
            });
        }

        [TestMethod]
        public void TestMap_ShipPlacement_ShipDoesNotFit()
        {
            Map m = new Map();
            Ship ship = new Ship(1, 4);

            // TODO: Implement

        }
    }
}
