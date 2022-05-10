using UniRx;
using UnityEngine;
using Random = System.Random;

namespace MVP
{
    public class GameOfLifePresenter: IGameOfLifePresenter
    {
        private readonly IGameOfLifeView _view;
        private readonly IGameOfLife _gameOfLife;
        private MapToSend _currentMap;
        private ReactiveProperty<bool>[,] _cells;
        private GameObject[,] _goCells;
        private readonly Random _rnd = new Random();


        public GameOfLifePresenter(IGameOfLifeView view, IGameOfLife gameOfLife)
        {
            _view = view;
            _gameOfLife = gameOfLife;
            _currentMap = _gameOfLife.GetCurrentMap();
            SetupCells();
            PopulateCellsWithMapData();
        }

        public void ChangeButton()
        {
            for (int i = 0; i < 50; i++)
            {
                ChangeCellAndUpdateMap(_rnd.Next(0,_currentMap.Height), _rnd.Next(0,_currentMap.Width));
            }
        }

        public void IncreaseGeneration()
        {
            _currentMap = _gameOfLife.IncreaseGenerations(1);
            PopulateCellsWithMapData();
        }
        
        private void SetupCells()
        {
            _cells = new ReactiveProperty<bool>[_currentMap.Height, _currentMap.Width];
            _goCells = new GameObject[_currentMap.Height, _currentMap.Width];
            
            for (int i = 0; i < _currentMap.Height; i++)
            {
                for (int j = 0; j < _currentMap.Width; j++)
                {
                    _cells[i, j] = new ReactiveProperty<bool>();
                    _goCells[i,j] = CreateNewGOCell(i,j);
                }
            }
        }

        private void PopulateCellsWithMapData()
        {
            for (int i = 0; i < _currentMap.Height; i++)
            {
                for (int j = 0; j < _currentMap.Width; j++)
                {
                    _cells[i, j].Value = _currentMap.CellStatus[i, j];
                }
            }
        }

        private void ChangeCellAndUpdateMap(int posX, int posY)
        {
            _currentMap = _gameOfLife.ChangeCell(posX, posY);
            PopulateCellsWithMapData();
        }

        private GameObject CreateNewGOCell(int posX, int posY)
        {
            GameObject newCell = _view.InstantiateNewGoCell();
            CellPrefab cellScript = newCell.GetComponent<CellPrefab>();
            
            cellScript.SetArrayPosition(posX, posY);
            newCell.name = $"Cell pos {posX}, {posY}";
            _cells[posX, posY].Subscribe(b => cellScript.SwitchLifeState(b));
            
            return newCell;
        }
    }
}
