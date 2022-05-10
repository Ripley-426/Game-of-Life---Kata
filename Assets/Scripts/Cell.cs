using System;

public class Cell: ICloneable
{
    private bool _isAlive = false;

    public bool IsAlive()
    {
        return _isAlive;
    }

    public void Revive()
    {
        _isAlive = true;
    }

    public void Die()
    {
        _isAlive = false;
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public Cell CheckGenerationOutcome(int aliveNeighbours)
    {
        Cell outcomeCell = (Cell) Clone();
        
        if (IsAlive())
        {
            if (aliveNeighbours < 2 || aliveNeighbours > 3)
            {
                outcomeCell.SwitchState();
            }  
        }
        else
        {
            if (aliveNeighbours == 3)
            {
                outcomeCell.SwitchState();
            }
        }
        
        return outcomeCell;
    }

    public void SwitchState()
    {
        if (_isAlive) { Die(); } else { Revive(); }
    }
}