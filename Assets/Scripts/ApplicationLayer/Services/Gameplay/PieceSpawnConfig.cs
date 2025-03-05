using DomainLayer.Gameplay;
using UnityEngine;

namespace ApplicationLayer.Services.Gameplay
{
    [CreateAssetMenu(fileName = "PieceSpawnConfig", menuName = "ScriptableObjects/Piece Spawn Config", order = 0)]
    public class PieceSpawnConfig : ScriptableObject
    {
        public Piece.Type PieceType;
        public int Weight;
    }
}