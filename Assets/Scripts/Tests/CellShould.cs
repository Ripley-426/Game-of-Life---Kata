using System.Collections.Generic;
using NUnit.Framework;

namespace Tests
{
    public class CellShould
    {
        private Cell _cell;
        private int _aliveNeighbourCells;

        [SetUp]
        public void Setup()
        {
            _cell = new Cell();
            _aliveNeighbourCells = 1;
        }

        [Test]
        public void NotBeAliveWhenCreated()
        {
            Assert.IsFalse(_cell.IsAlive());
        }

        [Test]
        public void BeAbleToComeToLife()
        {
            _cell.Revive();
            Assert.IsTrue(_cell.IsAlive());
        }

        [Test]
        public void BeAbleToDieAgain()
        {
            _cell.Revive();
            _cell.Die();
            Assert.IsFalse(_cell.IsAlive());
        }

        [Test]
        public void DieIfNotEnoughNearbyLiveCells()
        {
            _cell.Revive();
            Cell updatedCell = _cell.CheckGenerationOutcome(_aliveNeighbourCells);
            
            Assert.IsFalse(updatedCell.IsAlive());
        }

        [Test]
        public void ContinueToLiveIfCorrectAmountOfNeighbours()
        {
            _cell.Revive();
            _aliveNeighbourCells = 2;
            
            _cell.CheckGenerationOutcome(_aliveNeighbourCells);
            
            Assert.IsTrue(_cell.IsAlive());
        }

        [Test]
        public void DieIfTooManyNeighbours()
        {
            _cell.Revive();
            _aliveNeighbourCells = 4;
            
            Cell updatedCell = _cell.CheckGenerationOutcome(_aliveNeighbourCells);
            
            Assert.IsFalse(updatedCell.IsAlive());
        }

        [Test]
        public void ReviveIfCorrectAmountOfNeighbours()
        {
            _aliveNeighbourCells = 3;
            
            Cell updatedCell = _cell.CheckGenerationOutcome(_aliveNeighbourCells);
            
            Assert.IsTrue(updatedCell.IsAlive());
        }
    }
}