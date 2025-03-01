using UnityEngine;

namespace ApplicationLayer.Services.Pooling
{
    public interface IGameObjectPool
    {
        void InstantiateInitialElements(Transform parent);
        GameObject GetInstance(Vector3 newPosition);
        void BackToPool(GameObject instance);
        void ResetCollections();
    }
}