using System;

public class Cell: ICloneable, ICell
{
    private bool _isAlive = false;

    public bool IsAlive()
    {
        return _isAlive;
    }

    public void SwitchState()
    {
        if (_isAlive) { Die(); } else { Revive(); }
    }

    public ICell CheckGenerationOutcome(int aliveNeighbours)
    {
        ICell outcomeCell = (ICell) Clone();
        
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

    private void Revive()
    {
        _isAlive = true;
    }

    private void Die()
    {
        _isAlive = false;
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}