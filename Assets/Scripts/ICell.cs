public interface ICell
{
    void SwitchState();
    bool IsAlive();
    ICell CheckGenerationOutcome(int aliveNeighbours);
}