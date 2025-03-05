using System.Collections.Generic;
using ApplicationLayer.Services.Gameplay.CascadeSteps;
using ApplicationLayer.Services.Gameplay.DTOs;
using ApplicationLayer.Services.Gameplay.Signals;
using ApplicationLayer.Services.SignalDispatcher;
using DomainLayer.Gameplay;
using UnityEngine;

namespace ApplicationLayer.Services.Gameplay
{
    public class GameFlowExecutor : IGameFlowExecutor
    {
        private readonly IBoardGenerator _boardGenerator;
        private readonly IBoardFiller _boardFiller;
        private readonly ICellTappedExecutor _cellTappedExecutor;
        private readonly IBoardGravityExecutor _boardGravityExecutor;
        private readonly ISignalDispatcher _signalDispatcher;

        private Board _board;

        private bool _hasStarted;
        public bool IsAlive => _hasStarted;

        public GameFlowExecutor(
            IBoardGenerator boardGenerator,
            IBoardFiller boardFiller,
            ICellTappedExecutor cellTappedExecutor,
            IBoardGravityExecutor boardGravityExecutor,
            ISignalDispatcher signalDispatcher)
        {
            _boardGenerator = boardGenerator;
            _boardFiller = boardFiller;
            _cellTappedExecutor = cellTappedExecutor;
            _boardGravityExecutor = boardGravityExecutor;
            _signalDispatcher = signalDispatcher;

            _cellTappedExecutor.OnCellTapped += HandleCellTapped;
        }

        ~GameFlowExecutor()
        {
            _cellTappedExecutor.OnCellTapped -= HandleCellTapped;
        }

        public Board CreateBoard(Vector2Int rowsRange, Vector2Int columnsRange)
        {
            _board = _boardGenerator.GenerateBoard(rowsRange, columnsRange);
            _boardFiller.Fill(_board);
            
            return _board;
        }

        public void Start()
        {
            _hasStarted = true;
        }
        
        private void HandleCellTapped(CascadeMatchStep matchStep)
        {
            TryCalculateCascadeSteps(matchStep);
        }
        
        private void TryCalculateCascadeSteps(ICascadeStep initialStep)
        {
            var cascadeSteps = new List<ICascadeStep>();
            
            if (initialStep is not null)
            {
                cascadeSteps.Add(initialStep);
            }
            
            TryCalculateCascadeSteps(cascadeSteps);
            _signalDispatcher.Dispatch(new CascadeStartedSignal(cascadeSteps));
        }
        
        private void TryCalculateCascadeSteps(in ICollection<ICascadeStep> cascadeSteps)
        {
            var gravityStep = _boardGravityExecutor.TryApplyGravity(_board);
            cascadeSteps.Add(gravityStep);
            
            var refillStep = _boardFiller.Refill(_board);
            cascadeSteps.Add(refillStep);
        }
    }
}