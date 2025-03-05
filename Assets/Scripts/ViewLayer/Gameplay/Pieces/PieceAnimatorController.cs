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
        
        [Header("Configs - Fall")]
        [SerializeField] private float _fallDurationPerCell = .1f;
        [SerializeField] private AnimationCurve _fallCurve;
        [Header("Configs - Fall Squeeze")]
        [SerializeField] private Vector3 _fallSqueezeScale = new Vector3(.9f, 1.1f);
        [SerializeField] private float _fallSqueezeDuration = .1f;
        [SerializeField] private Ease _fallSqueezeEase = Ease.OutQuart;
        [SerializeField] private float _fallSqueezeRestoreDuration = .1f;
        [SerializeField] private Ease _fallSqueezeRestoreEase = Ease.OutBack;
        
        [Header("Configs - Spawn")]
        [SerializeField] private float _spawnFadeInDuration = .15f;
        [SerializeField] private Ease _spawnFadeInEase = Ease.OutQuart;
        
        private Tween _destroyTween;
        private Sequence _fallSequence;
        private Sequence _spawnSequence;
        
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
        
        public UniTask FallTo(Vector3 to, int dropHeightCells)
        {
            var tweenCompleted = false;

            _fallSequence = DOTween.Sequence();
            
            // Squeeze
            _fallSequence.Insert(
                0f,
                transform.DOScale(_fallSqueezeScale, _fallSqueezeDuration).SetEase(_fallSqueezeEase)
            );
            
            // Fall
            _fallSequence.Insert(
                0f,
                transform.DOMove(to, _fallDurationPerCell * dropHeightCells).SetEase(_fallCurve).OnComplete(() => tweenCompleted = true)
            );
            
            // Restore scale
            _fallSequence.Append(
                transform.DOScale(Vector3.one, _fallSqueezeRestoreDuration).SetEase(_fallSqueezeRestoreEase)
            );

            _fallSequence.Play();

            return UniTask.WaitUntil(() => tweenCompleted);
        }

        public UniTask SpawnAndFallTo(Vector3 to, int dropHeightCells, float delay)
        {
            var fallTweenCompleted = false;

            _spawnSequence = DOTween.Sequence();

            if (delay > 0f)
            {
                _spawnSequence.Insert(
                    0f,
                    transform.DOScale(Vector3.zero, 0f)
                );
            }
            
            
            // Fade In (with squeeze)
            _spawnSequence.Insert(
                delay,
                transform.DOScale(_fallSqueezeScale, _spawnFadeInDuration).SetEase(_spawnFadeInEase)
            );

            // Fall
            _spawnSequence.Insert(
                delay,
                transform
                    .DOMove(to, _fallDurationPerCell * dropHeightCells)
                    .SetEase(_fallCurve)
                    .OnComplete(() => fallTweenCompleted = true)
            );
            
            // Restore scale
            _spawnSequence.Append(
                transform.DOScale(Vector3.one, _fallSqueezeRestoreDuration).SetEase(_fallSqueezeRestoreEase)
            );

            _spawnSequence.Play();

            return UniTask.WaitUntil(() => fallTweenCompleted);
        }
    }
}