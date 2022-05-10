public class GameOfLifeService
{
        private readonly IGameOfLife _gameOfLife = new GameOfLife(new Map(100,100));

        public IGameOfLife GetDefaultMap()
        {
                return _gameOfLife;
        }
}