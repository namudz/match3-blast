using DomainLayer.Gameplay;
using UnityEngine;

namespace ApplicationLayer.Services.Pooling
{
    [CreateAssetMenu(fileName = "GameObjectPoolData", menuName = "ScriptableObjects/Piece Pool Config", order = 0)]
    public class PiecePoolDataConfig : GameObjectPoolDataConfig
    {
        public Piece.Type PieceType;
    }
}