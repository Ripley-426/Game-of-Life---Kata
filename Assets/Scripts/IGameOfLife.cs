public interface IGameOfLife
{
    MapToSend GetCurrentMap();
    MapToSend IncreaseGenerations(int i);
    MapToSend ChangeCell(int posX, int posY);
}