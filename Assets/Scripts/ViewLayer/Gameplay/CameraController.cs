using DomainLayer.Gameplay;
using UnityEngine;

namespace ViewLayer.Gameplay
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _cameraTransform;
        
        public void CenterOnBoard(Board board)
        {
            SetPosition(board);
            SetOrthographicSize(board);
        }

        private void SetPosition(Board board)
        {
            _cameraTransform.position = new Vector3(
                board.Columns / 2f,
                Mathf.CeilToInt(board.Rows / 2f) + 1,
                _cameraTransform.position.z
            );
        }

        private void SetOrthographicSize(Board board)
        {
            var size = board.Rows > board.Columns
                ? board.Rows
                : board.Columns + 1;
            _camera.orthographicSize = size;
        }
    }
}