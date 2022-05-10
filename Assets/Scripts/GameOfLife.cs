public class GameOfLife
{
    private readonly IMap _map;

    public GameOfLife(IMap map)
    {
        _map = map;
    }

    public int GetMapHeight()
    {
        return _map.GetHeight();
    }

    public int GetMapWidth()
    {
        return _map.GetWidth();
    }

    public MapToSend IncreaseGenerations(int generationsToIncrease)
    {
        _map.IncreaseMultipleGenerations(generationsToIncrease);
        return GetCurrentMap();
    }

    public MapToSend GetCurrentMap()
    {
        return _map.GetMapStatus();
    }

    public MapToSend ChangeCell(int posX, int posY)
    {
        _map.SwitchCellLife(posX, posY);
        return GetCurrentMap();
    }
}
