using ApplicationLayer.Services.Pooling;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ViewLayer.Gameplay.Pieces
{
    public class PieceController : MonoBehaviour, IPoolable
    {
        [SerializeField] protected PieceAnimatorController _animatorController;

        public UniTask DestroyPiece()
        {
            return _animatorController.DestroyPiece();
        }
        
        public UniTask FallTo(Vector3 to, int dropHeightCells)
        {
            return _animatorController.FallTo(to, dropHeightCells);
        }

        public UniTask SpawnAndFallTo(Vector3 to, int dropHeightCells, float delay)
        {
            return _animatorController.SpawnAndFallTo(to, dropHeightCells, delay);
        }
    }
}