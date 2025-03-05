using System;
using System.Collections.Generic;
using ApplicationLayer.Services.Gameplay.DTOs;
using DomainLayer.Gameplay;

namespace ApplicationLayer.Services.Gameplay.CascadeSteps
{
    public class BoardGravityExecutor : IBoardGravityExecutor
    {
        public CascadeGravityStep TryApplyGravity(Board board)
        {
            if (board is null) { throw new Exception("Can't apply gravity on a null board!"); }

            var steps = new List<GravityStep>();
            ApplyGravityToAllBoard(board, steps);

            return new CascadeGravityStep(steps);
        }

        private static void ApplyGravityToAllBoard(Board board, in List<GravityStep> steps)
        {
            for (var row = 0; row < board.Rows; ++row)
            {
                for (var col = 0; col < board.Columns; ++col)
                {
                    if (!board.Cells[row, col].IsEmpty) { continue; }

                    steps.AddRange(ApplyGravityOnColumn(board, row, col));
                }
            }
        }

        private static IEnumerable<GravityStep> ApplyGravityOnColumn(Board board, int emptyCellRow, int emptyCellCol)
        {
            var gravityStepsOnColumn = new List<GravityStep>();
            var fallenPieces = 0;
            for (var row = emptyCellRow + 1; row < board.Rows; ++row)
            {
                var fallResult = ApplyGravityOnCell(row);
                if (fallResult is null) { continue; }
                
                gravityStepsOnColumn.Add(fallResult);
                ++fallenPieces;
            }
            return gravityStepsOnColumn;

            GravityStep ApplyGravityOnCell(int rowToCheck)
            {
                var belowCell = board.GetCell(rowToCheck - 1, emptyCellCol);
                if (belowCell is null){ return null; }

                var fallingCell = board.Cells[rowToCheck, emptyCellCol];
                if (fallingCell.IsEmpty) { return null; }

                board.Cells[emptyCellRow + fallenPieces, emptyCellCol].Piece = fallingCell.Piece;
                board.Cells[rowToCheck, emptyCellCol].Piece = null;

                return new GravityStep(
                    board.Cells[rowToCheck, emptyCellCol], 
                    board.Cells[emptyCellRow + fallenPieces, emptyCellCol], 
                    rowToCheck - emptyCellRow
                );
            }

        }
    }
}