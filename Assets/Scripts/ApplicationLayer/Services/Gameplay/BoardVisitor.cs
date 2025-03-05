using System.Collections.Generic;
using System.Linq;
using DomainLayer.Gameplay;
using UnityEngine;

namespace ApplicationLayer.Services.Gameplay
{
    public static class BoardVisitor
    {
        public static Cell[] GetAdjacentCells(Board board, Vector2Int cellCoordinates)
        {
            return new Cell[]
            {
                board.GetCell(cellCoordinates.x, cellCoordinates.y - 1),
                board.GetCell(cellCoordinates.x, cellCoordinates.y + 1),
                board.GetCell(cellCoordinates.x - 1, cellCoordinates.y),
                board.GetCell(cellCoordinates.x + 1, cellCoordinates.y)
            };
        }

        public static List<Cell> FindAllAdjacentCellsWithSamePiece(Board board, Cell cell)
        {
            var adjacentCellsWithSamePiece = new List<Cell>{cell};
            var counterCellsToCheck = 1;
            var iCellToCheck = 0;

            do
            {
                var iAdjacentCells = GetAdjacentCells(board, adjacentCellsWithSamePiece[iCellToCheck].Coordinates);
                var iValidAdjacentCells = iAdjacentCells.Where(c => c is {IsEmpty: false} && c.Piece.PieceType == cell.Piece.PieceType && !adjacentCellsWithSamePiece.Contains(c)).ToArray();
                
                adjacentCellsWithSamePiece.AddRange(iValidAdjacentCells);
                ++iCellToCheck;
                counterCellsToCheck += iValidAdjacentCells.Length;

            } while (iCellToCheck < counterCellsToCheck);

            return adjacentCellsWithSamePiece;
        }
    }
}