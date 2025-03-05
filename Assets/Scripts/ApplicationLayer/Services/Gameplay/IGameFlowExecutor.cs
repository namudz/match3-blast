using DomainLayer.Gameplay;
using UnityEngine;

namespace ApplicationLayer.Services.Gameplay
{
    public interface IGameFlowExecutor
    {
        bool IsAlive { get; }
        
        Board CreateBoard(Vector2Int rowsRange, Vector2Int columnsRange);
        void Start();
    }
}