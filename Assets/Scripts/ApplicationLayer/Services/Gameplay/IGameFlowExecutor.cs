using System.Threading;
using Cysharp.Threading.Tasks;
using DomainLayer.Gameplay;
using UnityEngine;

namespace ApplicationLayer.Services.Gameplay
{
    public interface IGameFlowExecutor
    {
        bool IsAlive { get; }
        
        Board CreateBoard(Vector2Int rowsRange, Vector2Int columnsRange);
        UniTask Start(CancellationToken cancellationToken);
    }
}