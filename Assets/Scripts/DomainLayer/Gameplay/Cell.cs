using UnityEngine;

namespace DomainLayer.Gameplay
{
    public class Cell
    {
        public static Vector2 WorldSize = new(1f, 1f);
        public static Vector2 PieceOffset = new(0.5f, 0.5f);
        
        public readonly Vector2Int Coordinates;
        public readonly Vector3 WorldCoordinates;
        public Piece Piece { get; set; }

        public bool IsEmpty => Piece is null;

        public Cell(Vector2Int coordinates, Vector3 worldCoordinates)
        {
            Coordinates = coordinates;
            WorldCoordinates = worldCoordinates;
        }

        public override string ToString()
        {
            return $"Cell ({Coordinates.x},{Coordinates.y}) - Piece = {Piece?.PieceType}";
        }

        public Cell Clone()
        {
            return new Cell(Coordinates, WorldCoordinates)
            {
                Piece = Piece
            };
        }
    }
}