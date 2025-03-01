using System.Collections.Generic;
using UnityEngine;

namespace ApplicationLayer.Services.Pooling
{
    public class GameObjectPool : IGameObjectPool
    {
        private Transform _transform;
        private readonly GameObject _prefab;
        private readonly int _initialAmount;

        private readonly List<GameObject> _inactiveInstances;

        public GameObjectPool(GameObjectPoolData data)
        {
            _transform = data.RootTransform;
            _prefab = data.Prefab;
            _initialAmount = data.InitialAmount;
            
            _inactiveInstances = new List<GameObject>(_initialAmount);
        }
        
        public void InstantiateInitialElements(Transform poolsParent)
        {
            _transform = poolsParent;
            for (var i = 0; i < _initialAmount; i++)
            {
                InstantiateElement();
            }
        }

        public GameObject GetInstance(Vector3 newPosition)
        {
            if (_inactiveInstances.Count == 0)
            {
                InstantiateElement();
            }

            var instance = _inactiveInstances[0];
            _inactiveInstances.RemoveAt(0);
            
            instance.transform.position = newPosition;
            instance.SetActive(true);
            
            return instance;
        }

        public void BackToPool(GameObject instance)
        {
            if (instance is null) { return; }
            
            if (instance != null)
            {
                instance.SetActive(false);
            }
            
            if (!_inactiveInstances.Contains(instance))
            {
                _inactiveInstances.Add(instance);
            }
        }

        private void InstantiateElement()
        {
            var instance = Object.Instantiate(_prefab, _transform.position, Quaternion.identity, _transform);
            instance.SetActive(false);
            _inactiveInstances.Add(instance);
        }

        public void ResetCollections()
        {
            _inactiveInstances.Clear();
        }
    }
}