using ApplicationLayer.Services.Pooling;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ViewLayer.Gameplay.Pieces
{
    public class PieceController : MonoBehaviour, IPoolable
    {
        [SerializeField] protected PieceAnimatorController _animatorController;
    }
}