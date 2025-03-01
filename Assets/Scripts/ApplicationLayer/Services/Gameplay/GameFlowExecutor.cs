using System.Threading;
using Cysharp.Threading.Tasks;
using DomainLayer.Gameplay;
using UnityEngine;

namespace ApplicationLayer.Services.Gameplay
{
    public class GameFlowExecutor : IGameFlowExecutor
    {
        private readonly IBoardGenerator _boardGenerator;

        private Board _board;

        private bool _hasStarted;
        private bool _isOver;
        public bool IsAlive => _hasStarted && !_isOver;

        public GameFlowExecutor(IBoardGenerator boardGenerator)
        {
            _boardGenerator = boardGenerator;
        }

        public Board CreateBoard(Vector2Int rowsRange, Vector2Int columnsRange)
        {
            _board = _boardGenerator.GenerateBoard(rowsRange, columnsRange);
            
            return _board;
        }

        public async UniTask Start(CancellationToken cancellationToken)
        {
            _hasStarted = true;
            _isOver = false;
        }
    }
}