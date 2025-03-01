using ApplicationLayer.Services.Gameplay;
using ApplicationLayer.Services.SignalDispatcher;
using DomainLayer.Gameplay;
using UnityEngine;
using ViewLayer.Gameplay.Signals;

namespace ViewLayer.Gameplay
{
    public class GameController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private BoardRenderer _boardRenderer;
        [SerializeField] private CameraController _cameraController;

        private IGameFlowExecutor _gameFlowExecutor;
        private ISignalDispatcher _signalDispatcher;
        private LevelGenericConfig _levelGenericConfig;
        private Board _board;
        public bool IsBoardReady { get; private set; } = true;
        public bool IsGameAlive => _gameFlowExecutor.IsAlive;

        public void InjectDependencies(
            LevelGenericConfig levelGenericConfig,
            IGameFlowExecutor gameFlowExecutor,
            ISignalDispatcher signalDispatcher)
        {
            _levelGenericConfig = levelGenericConfig;
            _gameFlowExecutor = gameFlowExecutor;
            _signalDispatcher = signalDispatcher;
        }

        public void StartGame()
        {
            LoadGame();
            RenderGame();
            
            _signalDispatcher.Dispatch(new LevelReadySignal());
        }

        private void LoadGame()
        {
            // Create board
            _board = _gameFlowExecutor.CreateBoard(_levelGenericConfig.BoardRowsRange, _levelGenericConfig.BoardColumnsRange);

            // Center camera    
            _cameraController.CenterOnBoard(_board);
        }

        private void RenderGame()
        {
            _boardRenderer.RenderBoard(_board);
        }
    }
}