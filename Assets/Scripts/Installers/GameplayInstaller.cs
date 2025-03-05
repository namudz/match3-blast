using ApplicationLayer.Services;
using ApplicationLayer.Services.Gameplay;
using ApplicationLayer.Services.Gameplay.CascadeSteps;
using ApplicationLayer.Services.Gameplay.Input;
using ApplicationLayer.Services.Pooling;
using ApplicationLayer.Services.Random;
using ApplicationLayer.Services.SignalDispatcher;
using UnityEngine;
using ViewLayer.Gameplay;
using ViewLayer.Input;

namespace Installers
{
    public class GameplayInstaller : MonoBehaviour
    {
        [Header("Configs")]
        [SerializeField] private LevelGenericConfig _levelGenericConfig;
        [SerializeField] private PieceSpawnConfig[] _piecesToSpawnConfigs;
        
        [Header("Dependencies")]
        [SerializeField] private GameController _gameController;
        [SerializeField] private BoardRenderer _boardRenderer;
        [SerializeField] private PiecesPoolsFacade _piecesPoolsFacade;
        [SerializeField] private InputListener _inputListener;
        
        private ISignalDispatcher _signalDispatcher;
        private IRandomFacade _randomFacade;
        private IGameFlowExecutor _gameFlowExecutor;
        private IInputHandler _inputHandler;

        private void Awake()
        {
            InitializeServices();
            InjectDependencies();
        }

        private void OnDestroy()
        {
            UnregisterServices();
        }

        private void Start()
        {
            _gameController.StartGame();
        }

        private void InitializeServices()
        {
            _signalDispatcher = new SignalDispatcher();
            ServiceLocator.Instance.RegisterService(_signalDispatcher);
            
            _randomFacade = new RandomFacade();
            ServiceLocator.Instance.RegisterService(_randomFacade);

            var cellTappedExecutor = new CellTappedExecutor();
            
            _gameFlowExecutor = new GameFlowExecutor(
                new BoardGenerator(_randomFacade), 
                new BoardFiller(_piecesToSpawnConfigs, _randomFacade),
                cellTappedExecutor,
                new BoardGravityExecutor(),
                _signalDispatcher
            );
            ServiceLocator.Instance.RegisterService(_gameFlowExecutor);
            
            _piecesPoolsFacade.Initialize();

            _inputHandler = new InputHandler(cellTappedExecutor);
        }
        
        private void InjectDependencies()
        {
            _gameController.InjectDependencies(
                _levelGenericConfig,
                _gameFlowExecutor,
                _signalDispatcher,
                _inputHandler
            );
            
            _boardRenderer.Inject(_piecesPoolsFacade, _signalDispatcher);
            
            _inputListener.Inject(_inputHandler);
        }
        
        private void UnregisterServices()
        {
            ServiceLocator.Instance.UnregisterService<ISignalDispatcher>();
            ServiceLocator.Instance.UnregisterService<IRandomFacade>();
            ServiceLocator.Instance.UnregisterService<IGameFlowExecutor>();
        }
    }
}