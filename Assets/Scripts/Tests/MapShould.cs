using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    public class MapShould
    {
        private Map _map;
        private Map _basicMap;
        private const int Height = 30;
        private const int Width = 30;

        [SetUp]
        public void Setup()
        {
            _basicMap = new Map(Height, Width);
        }

        [Test]
        [TestCase(5)]
        [TestCase(3)]
        public void HaveHeight(int height)
        {
            _map = new Map(height, 5);
            Assert.AreEqual(height, _map.GetHeight());
        }

        [Test]
        [TestCase(5)]
        [TestCase(3)]
        public void HaveWidth(int width)
        {
            _map = new Map(5, width);
            Assert.AreEqual(width, _map.GetWidth());
        }

        [Test]
        public void IncreaseGeneration()
        {
            _map = _basicMap;

            _map.IncreaseGeneration();
            
            Assert.AreEqual(2, _map.GetGeneration());
        }

        [Test]
        [TestCase(4)]
        [TestCase(2)]
        public void PassMultipleGenerations(int generationsToIncrease)
        {
            _map = _basicMap;
            
            _map.IncreaseMultipleGenerations(generationsToIncrease);
            
            Assert.AreEqual(1 + generationsToIncrease, _map.GetGeneration());
        }

        [Test]
        [TestCase(1, 6)]
        [TestCase(20, 15)]
        [TestCase(29, 29)]
        public void HaveADeadCellOnEachPointAtStart(int posX, int posY)
        {
            _map = _basicMap;
            
            Assert.IsFalse(_map.CheckCellIfAlive(posX, posY));
        }

        [Test]
        [TestCase(1, 6)]
        [TestCase(20, 15)]
        [TestCase(29, 29)]
        public void BeAbleToSwitchCellsLifeByPosition(int posX, int posY)
        {
            _map = _basicMap;
            
            _map.SwitchCellLife(posX, posY);
            
            Assert.IsTrue(_map.CheckCellIfAlive(posX, posY));
        }

        [Test]
        public void CalculateEnoughNeighboursAndReviveTheCell()
        {
            _map = _basicMap;
            
            _map.SwitchCellLife(15,1);
            _map.SwitchCellLife(15,2);
            _map.SwitchCellLife(15,3);
            
            _map.IncreaseGeneration();

            Assert.IsTrue(_map.CheckCellIfAlive(16,2));
        }

        [Test]
        public void CalculateEnoughNeighboursAndKeepAliveTheCell()
        {
            _map = _basicMap;
            
            _map.SwitchCellLife(2,4);
            _map.SwitchCellLife(4,4);
            _map.SwitchCellLife(3,5);
            //_map.SwitchCellLife(3,4);

            _map.IncreaseGeneration();
            
            Assert.IsTrue(_map.CheckCellIfAlive(3,5), "Two neighbours cell");
        }
        
        [Test]
        public void CalculateNotEnoughNeighboursToKeepAliveTheCell()
        {
            _map = _basicMap;
            
            _map.SwitchCellLife(2,4);
            _map.SwitchCellLife(3,5);

            _map.IncreaseGeneration();
            
            Assert.IsFalse(_map.CheckCellIfAlive(3,5), "One neighbour cell");
        }
        
        [Test]
        public void CalculateTooManyNeighboursAndKillTheCell()
        {
            _map = _basicMap;
            
            _map.SwitchCellLife(2,4);
            _map.SwitchCellLife(2,5);
            _map.SwitchCellLife(4,4);
            _map.SwitchCellLife(3,5);
            _map.SwitchCellLife(3,4);

            _map.IncreaseGeneration();
            
            Assert.IsFalse(_map.CheckCellIfAlive(3,5), "Four neighbours cell");
        }

        [Test]
        public void CalculateNoNeighboursCorrectly()
        {
            _map = new Map(5, 5);

            _map.SwitchCellLife(1, 1);

            Assert.AreEqual(0, _map.CalculateAliveNeighbours(1, 1));
        }
        
        [Test]
        public void CalculateOneNeighbourCorrectly()
        {
            _map = new Map(5, 5);

            _map.SwitchCellLife(1, 1);
            _map.SwitchCellLife(1, 2);
            
            Assert.AreEqual(1, _map.CalculateAliveNeighbours(1, 1), "1,1");
            Assert.AreEqual(1, _map.CalculateAliveNeighbours(1, 2), "1,2");
        }
        
        [Test]
        [TestCase(2,3,2,4)]
        [TestCase(2,5,4,3)]
        [TestCase(4,4,3,5)]                 
        [TestCase(3,3,4,5)]
        public void CalculateTwoNeighbourCorrectly(int firstCellX, int firstCellY, int secondCellX, int secondCellY)
        {
            _map = new Map(6,6);

            _map.SwitchCellLife(3, 4);
            _map.SwitchCellLife(firstCellX, firstCellY);
            _map.SwitchCellLife(secondCellX, secondCellY);

            Assert.AreEqual(2, _map.CalculateAliveNeighbours(3, 4));
        }

        [Test]
        public void BeAbleToExportCurrentMapStatus()
        {
            _map = _basicMap;
            _map.SwitchCellLife(3,3);

            MapToSend mapToSend = _map.GetMapStatus();

            MapToSend expectedMap = new MapToSend
            {
                Height = 30,
                Width = 30,
                Generation = 1,
                CellStatus = new bool[30, 30]
            };

            expectedMap.CellStatus[3, 3] = true;

            mapToSend.Should().BeEquivalentTo(expectedMap);
        }
    }
}