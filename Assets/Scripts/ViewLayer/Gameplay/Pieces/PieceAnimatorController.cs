using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace ViewLayer.Gameplay.Pieces
{
    public class PieceAnimatorController : MonoBehaviour
    {
        [Header("Configs - Destroy")]
        [SerializeField] private float _destroyDuration = .15f;
        [SerializeField] private Ease _destroyEase = Ease.InBack;
        
        private Tween _destroyTween;
        
        private void OnDestroy()
        {
            _destroyTween?.Kill();
        }

        public UniTask DestroyPiece()
        {
            var tweenCompleted = false;
            _destroyTween = transform
                .DOScale(Vector3.zero, _destroyDuration)
                .SetEase(_destroyEase)
                .Play()
                .OnComplete(() => tweenCompleted = true);

            return UniTask.WaitUntil(() => tweenCompleted);
        }
    }
}