public interface IMap
{
    int GetHeight();
    int GetWidth();
    void SwitchCellLife(int posX, int posY);
    void IncreaseMultipleGenerations(int generationsToIncrease);
    MapToSend GetMapStatus();
}