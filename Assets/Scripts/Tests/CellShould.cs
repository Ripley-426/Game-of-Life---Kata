using System.Collections.Generic;
using NUnit.Framework;

namespace Tests
{
    public class CellShould
    {
        private ICell _cell;
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
            _cell.SwitchState();
            Assert.IsTrue(_cell.IsAlive());
        }

        [Test]
        public void BeAbleToDieAgain()
        {
            _cell.SwitchState();
            _cell.SwitchState();
            Assert.IsFalse(_cell.IsAlive());
        }

        [Test]
        public void DieIfNotEnoughNearbyLiveCells()
        {
            _cell.SwitchState();
            ICell updatedCell = _cell.CheckGenerationOutcome(_aliveNeighbourCells);
            
            Assert.IsFalse(updatedCell.IsAlive());
        }

        [Test]
        public void ContinueToLiveIfCorrectAmountOfNeighbours()
        {
            _cell.SwitchState();
            _aliveNeighbourCells = 2;
            
            _cell.CheckGenerationOutcome(_aliveNeighbourCells);
            
            Assert.IsTrue(_cell.IsAlive());
        }

        [Test]
        public void DieIfTooManyNeighbours()
        {
            _cell.SwitchState();
            _aliveNeighbourCells = 4;
            
            ICell updatedCell = _cell.CheckGenerationOutcome(_aliveNeighbourCells);
            
            Assert.IsFalse(updatedCell.IsAlive());
        }

        [Test]
        public void ReviveIfCorrectAmountOfNeighbours()
        {
            _aliveNeighbourCells = 3;
            
            ICell updatedCell = _cell.CheckGenerationOutcome(_aliveNeighbourCells);
            
            Assert.IsTrue(updatedCell.IsAlive());
        }
    }
}