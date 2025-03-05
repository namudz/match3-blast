using ApplicationLayer.Services.Gameplay;
using ApplicationLayer.Services.Gameplay.DTOs;
using ApplicationLayer.Services.Gameplay.Input;
using ApplicationLayer.Services.Gameplay.Signals;
using ApplicationLayer.Services.SignalDispatcher;
using Cysharp.Threading.Tasks;
using DomainLayer.Gameplay;
using UnityEngine;

namespace ViewLayer.Gameplay
{
    public class GameController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private BoardRenderer _boardRenderer;
        [SerializeField] private CameraController _cameraController;

        private LevelGenericConfig _levelGenericConfig;
        private IGameFlowExecutor _gameFlowExecutor;
        private ISignalDispatcher _signalDispatcher;
        private IInputHandler _inputHandler;
        private Board _board;
        public bool IsBoardReady { get; private set; } = true;
        public bool IsGameAlive => _gameFlowExecutor.IsAlive;

        public void InjectDependencies(
            LevelGenericConfig levelGenericConfig,
            IGameFlowExecutor gameFlowExecutor,
            ISignalDispatcher signalDispatcher, 
            IInputHandler inputHandler)
        {
            _levelGenericConfig = levelGenericConfig;
            _gameFlowExecutor = gameFlowExecutor;
            _signalDispatcher = signalDispatcher;
            _inputHandler = inputHandler;
            
            _signalDispatcher.Subscribe<CascadeStartedSignal>(HandleCascadeStarted);
        }

        public void StartGame()
        {
            LoadGame();
            RenderGame();
            
            // _signalDispatcher.Dispatch(new LevelReadySignal());

            _gameFlowExecutor.Start();
        }

        private void LoadGame()
        {
            // Create board
            _board = _gameFlowExecutor.CreateBoard(_levelGenericConfig.BoardRowsRange, _levelGenericConfig.BoardColumnsRange);
            
            // Setup input
            _inputHandler.SetBoard(_board);

            // Center camera    
            _cameraController.CenterOnBoard(_board);
        }

        private void RenderGame()
        {
            _boardRenderer.RenderBoard(_board);
        }
        
        private void HandleCascadeStarted(CascadeStartedSignal signal)
        {
            ExecuteCascadeSteps(signal).Forget();
        }

        private async UniTask ExecuteCascadeSteps(CascadeStartedSignal signal)
        {
            IsBoardReady = false;

            foreach (var cascadeStep in signal.CascadeSteps)
            {
                if (cascadeStep is CascadeMatchStep matchStep)
                {
                    await _boardRenderer.DestroyMatchPieces(matchStep.MatchCells);
                    continue;
                }
                
                if (cascadeStep is CascadeGravityStep gravityStep)
                {
                    await _boardRenderer.ApplyGravity(gravityStep.Steps);
                    continue;
                }

                if (cascadeStep is CascadeRefillStep refillStep)
                {
                    await _boardRenderer.Refill(refillStep.Steps);
                    continue;
                }
            }

            IsBoardReady = true;
        }
    }
}