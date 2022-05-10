using MVP;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    public class GameOfLifePresenterShould
    {
        private IGameOfLifeView _view = Substitute.For<IGameOfLifeView>();
        private IGameOfLife _gameOfLife = Substitute.For<IGameOfLife>();
        private IGameOfLifePresenter _presenter;
        private GameOfLifeService _gameOfLifeService = new GameOfLifeService();

        [SetUp]
        public void Setup()
        {
            _presenter = new GameOfLifePresenter(_view, _gameOfLife);
            
        }

        [Test]
        public void BeAbleToChangeRandomCells()
        {
            _presenter.ChangeButton();
            _gameOfLife.ReceivedWithAnyArgs(50).ChangeCell(default, default);
        }
        
        [Test]
        public void BeAbleToIncreaseGenerations()
        {
            _presenter.IncreaseGeneration();
            _gameOfLife.Received(1).IncreaseGenerations(1);
        }
    }
}
