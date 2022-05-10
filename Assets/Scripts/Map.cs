using System;
using UnityEngine;

public class Map: IMap, ICloneable
{
    private readonly int _height;
    private readonly int _width;
    private ICell[,] _cells;
    private int _generation = 1;

    public Map(int height, int width)
    {
        _height = height;
        _width = width;
        _cells = new ICell[height, width];
        PopulateCells();
    }

    private void PopulateCells()
    {
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                _cells[i, j] = new Cell();
            }
        }
    }

    public int GetHeight()
    {
        return _height;
    }

    public int GetWidth()
    {
        return _width;
    }

    public int GetGeneration()
    {
        return _generation;
    }

    public void IncreaseGeneration()
    {
        _generation++;
        UpdateCells();
    }

    private void UpdateCells()
    {
        ICell[,] updatedCells = new ICell[_height, _width];
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                int aliveNeighbours = CalculateAliveNeighbours(i, j);
                updatedCells[i,j] = _cells[i,j].CheckGenerationOutcome(aliveNeighbours);
            }
        }

        _cells = updatedCells;
    }

    public int CalculateAliveNeighbours(int posX, int posY)
    {
        int aliveNeighbours = 0;
        int rowStart  = Math.Max(posX - 1, 0);
        int rowFinish = Math.Min(posX + 1, _height - 1);
        int colStart  = Math.Max(posY - 1, 0);
        int colFinish = Math.Min(posY + 1, _width - 1);

        for ( int curRow = rowStart; curRow <= rowFinish; curRow++ ) {
            for ( int curCol = colStart; curCol <= colFinish; curCol++ )
            {
                if (curRow == posX && curCol == posY) { continue; }
                if (_cells[curRow, curCol].IsAlive())
                {
                    aliveNeighbours++;
                }
            }
        }

        return aliveNeighbours;
    }

    public void IncreaseMultipleGenerations(int numberOfGenerationsToPass)
    {
        for (int i = 0; i < numberOfGenerationsToPass; i++)
        {
            IncreaseGeneration();
        }
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public bool CheckCellIfAlive(int posX, int posY)
    {
        return _cells[posX, posY].IsAlive();
    }

    public void SwitchCellLife(int posX, int posY)
    {
        _cells[posX, posY].SwitchState();
    }

    public MapToSend GetMapStatus()
    {
        MapToSend currentMap = new MapToSend()
        {
            Height = _height,
            Width = _width,
            Generation = _generation,
            CellStatus = GenerateBoolArrayFromCells()
        };

        return currentMap;
    }

    private bool[,] GenerateBoolArrayFromCells()
    {
        bool[,] boolCells = new bool[_height, _width];

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                boolCells[i, j] = _cells[i, j].IsAlive();
            }
        }

        return boolCells;
    }
}