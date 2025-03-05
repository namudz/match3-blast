using System;
using UnityEngine;

namespace ApplicationLayer.Services.Pooling
{
    [Serializable]
    public class GameObjectPoolData
    {
        public Transform RootTransform { get; set; }
        public GameObject Prefab;
        public int InitialAmount;
    }
}