using UniRx;
using UnityEngine;

namespace MVP
{
    public interface IGameOfLifeView
    {
        GameObject InstantiateNewGoCell();
    }
}