using System.Collections.Generic;
using ApplicationLayer.Services.SignalDispatcher;
using DomainLayer.Gameplay;
using UnityEngine;

namespace ApplicationLayer.Services.Pooling
{
    public interface IPiecesPoolsFacade
    {
        GameObject GetInstance(Piece.Type pieceType, Vector3 newPosition);

        void BackToPool(Piece.Type pieceType, GameObject instance);
    }
    
    public class PiecesPoolsFacade : MonoBehaviour, IPiecesPoolsFacade
    {
        [Header("Dependencies - Pools")]
        [SerializeField] private PiecePoolDataConfig[] _piecesPoolsConfigs;
        [SerializeField] private Transform _piecesPoolContainer;

        private readonly Dictionary<Piece.Type, IGameObjectPool> _pools = new();

        public void Initialize()
        {
            CreatePools();
            InitializePools();
        }

        private void CreatePools()
        {
            foreach (var piecePoolConfig in _piecesPoolsConfigs)
            {
                switch (piecePoolConfig.PieceType)
                {
                    case Piece.Type.Blue:
                        var bluePool = new GameObjectPool(piecePoolConfig.PoolData);
                        _pools.Add(piecePoolConfig.PieceType, bluePool);
                        break;

                    case Piece.Type.Green:
                        var greenPool = new GameObjectPool(piecePoolConfig.PoolData);
                        _pools.Add(piecePoolConfig.PieceType, greenPool);
                        break;

                    case Piece.Type.Pink:
                        var orangePool = new GameObjectPool(piecePoolConfig.PoolData);
                        _pools.Add(piecePoolConfig.PieceType, orangePool);
                        break;

                    case Piece.Type.Red:
                        var redPool = new GameObjectPool(piecePoolConfig.PoolData);
                        _pools.Add(piecePoolConfig.PieceType, redPool);
                        break;

                    case Piece.Type.Yellow:
                        var yellowPool = new GameObjectPool(piecePoolConfig.PoolData);
                        _pools.Add(piecePoolConfig.PieceType, yellowPool);
                        break;
                }
            }
        }
        
        private void InitializePools()
        {
            foreach (var pool in _pools)
            {
                pool.Value.InstantiateInitialElements(_piecesPoolContainer);
            }
        }

        public GameObject GetInstance(Piece.Type pieceType, Vector3 newPosition)
        {
            return _pools[pieceType].GetInstance(newPosition);
        }

        public void BackToPool(Piece.Type pieceType, GameObject instance)
        {
            _pools[pieceType].BackToPool(instance);
        }
        
        private void HandleLevelDestroyed(ISignal _)
        {
            foreach (var pool in _pools)
            {
                pool.Value.ResetCollections();
            }
        }
    }
}