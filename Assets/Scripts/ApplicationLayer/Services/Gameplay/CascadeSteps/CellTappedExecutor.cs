using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLayer.Services.Gameplay.DTOs;
using DomainLayer.Gameplay;

namespace ApplicationLayer.Services.Gameplay.CascadeSteps
{
    public class CellTappedExecutor : ICellTappedExecutor
    {
        private const int MinAdjacentCellsForMatch = 2;
        
        public event Action<CascadeMatchStep> OnCellTapped;

        public void TryExecuteTap(Board board, Cell cell)
        {
            var adjacentCells = BoardVisitor.FindAllAdjacentCellsWithSamePiece(board, cell);
            if (adjacentCells.Count < MinAdjacentCellsForMatch) { return; }

            // Copy for the view
            var adjacentCellsToView = new List<Cell>(adjacentCells.Select(adjacentCell => adjacentCell.Clone()));
            
            // Clean cells pieces
            foreach (var adjacentCell in adjacentCells)
            {
                adjacentCell.Piece = null;
            }

            OnCellTapped?.Invoke(new CascadeMatchStep(adjacentCellsToView));
        }
    }
}