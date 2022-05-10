using UniRx;
using UnityEngine;
using Random = System.Random;

namespace MVP
{
    public class GameOfLifeView : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;
        private GameOfLife _gameOfLife;
        private MapToSend _currentMap;
        private ReactiveProperty<bool>[,] _cells;
        private GameObject[,] _goCells;
        private readonly Random _rnd = new Random();

        public bool autoGeneration = false;
        public float timeToWaitUntilNextGen = 0.5f;
        public float currentTime;
        private void Awake()
        {
            _gameOfLife = new GameOfLife(new Map(30, 30));
            _currentMap = _gameOfLife.GetCurrentMap();
            SetupCells();
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

        private void Update()
        {
            if (!autoGeneration) return;
            currentTime += Time.deltaTime;
            if (!(currentTime >= timeToWaitUntilNextGen)) return;
            IncreaseGeneration();
            currentTime = 0;
        }

        public void ChangeAutoGenerationStatus()
        {
            autoGeneration = !autoGeneration;
            currentTime = 0;
        }

        public void Change()
        {
            for (int i = 0; i < 50; i++)
            {
                ChangeCellAndUpdateMap(_rnd.Next(0,29), _rnd.Next(0,29));
            }
        }

        public void IncreaseGeneration()
        {
            _currentMap = _gameOfLife.IncreaseGenerations(1);
            PopulateCellsWithMapData();
        }

        private void ChangeCellAndUpdateMap(int posX, int posY)
        {
            _currentMap = _gameOfLife.ChangeCell(posX, posY);
            PopulateCellsWithMapData();
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

        private GameObject CreateNewGOCell(int posX, int posY)
        {
            GameObject newCell = Instantiate(cellPrefab, gameObject.transform);

            CellPrefab cellScript = newCell.GetComponent<CellPrefab>();
            
            cellScript.SetArrayPosition(posX, posY);

            newCell.name = $"Cell pos {posX}, {posY}";

            _cells[posX, posY].Subscribe(b => cellScript.SwitchLifeState(b));

            return newCell;
        }
    }
}
