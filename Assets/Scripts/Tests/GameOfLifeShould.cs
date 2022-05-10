using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    public class GameOfLifeShould
    {
        private GameOfLife _gameOfLife;
        private IMap _map;
        
        [SetUp]
        public void Setup()
        {
            _map = Substitute.For<IMap>();
            _gameOfLife = new GameOfLife(_map);
        }
        
        [Test]
        [TestCase(30, 30)]
        [TestCase(50, 50)]
        public void CreateAMapDefinedByTheUser(int mapHeight, int mapWidth)
        {
            _map.GetHeight().Returns(mapHeight);
            _map.GetWidth().Returns(mapWidth);
            
            Assert.AreEqual(mapHeight, _gameOfLife.GetMapHeight(), "Map height should be equal");
            Assert.AreEqual(mapWidth, _gameOfLife.GetMapWidth(), "Map width should be equal");
        }

        [Test]
        public void ChangeCellStatusByPosition()
        {
            _gameOfLife.ChangeCell(15, 15);
            
            _map.Received(1).SwitchCellLife(15, 15);
        }

        [Test]
        public void BeAbleToIncreaseMultipleGenerationsInTheMap()
        {
            _gameOfLife.IncreaseGenerations(5);
            
            _map.Received(1).IncreaseMultipleGenerations(5);
        }

        [Test]
        public void BeAbleToExportTheCurrentMapStatus()
        {
            MapToSend basicMapExport = new MapToSend
            {
                Height = 30,
                Width = 30,
                Generation = 1,
                CellStatus = new bool[30, 30]
            };
            
            _map.GetMapStatus().Returns(basicMapExport);

            MapToSend gameOfLifeExport = _gameOfLife.GetCurrentMap();
                
            MapToSend expectedMap = new MapToSend
            {
                Height = 30,
                Width = 30,
                Generation = 1,
                CellStatus = new bool[30, 30]
            };

            gameOfLifeExport.Should().BeEquivalentTo(expectedMap);
        }
    }
}
