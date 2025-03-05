using ApplicationLayer.Services.Gameplay.Input;
using DomainLayer.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using ViewLayer.Gameplay;

namespace ViewLayer.Input
{
    public class InputListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
    {
        [SerializeField] private Camera _gameCamera;
        [SerializeField] private GameController _gameController;
        
        private bool _isDragInProgress;

        private IInputHandler _inputHandler;
        private bool IsInputEnabled => _gameController.IsGameAlive && _gameController.IsBoardReady;

        public void Inject(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!IsInputEnabled) { return; }
            
            var worldPosition = GetWorldPosition(eventData.pressPosition);
            _inputHandler.HandleClick(worldPosition);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!IsInputEnabled) { return; }
            
            var worldPosition = GetWorldPosition(eventData.pressPosition);
            _inputHandler.HandleClick(worldPosition);
        }
        
        private Vector2 GetWorldPosition (Vector3 screenPosition)
        {
            Ray ray = _gameCamera.ScreenPointToRay(screenPosition);
            Vector3 p = ray.origin - ray.direction * (ray.origin.z / ray.direction.z);
            
            return new Vector2(
                p.x - Cell.PieceOffset.x + 0.5f, 
                p.y - Cell.PieceOffset.y + 0.5f
            );
        }
    }
}