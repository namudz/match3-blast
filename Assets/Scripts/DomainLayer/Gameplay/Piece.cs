using System;

namespace DomainLayer.Gameplay
{
    public class Piece
    {
        [Serializable]
        public enum Type
        {
            Blue,
            Green,
            Orange,
            Red,
            Yellow
        }

        public readonly Type PieceType;
        
        public Piece(Type pieceType)
        {
            PieceType = pieceType;
        }
    }
}