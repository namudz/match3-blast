using ApplicationLayer.Services.Gameplay.CascadeSteps;
using DomainLayer.Gameplay;
using UnityEngine;

namespace ApplicationLayer.Services.Gameplay.Input
{
    public class InputHandler : IInputHandler
    {
        private readonly ICellTappedExecutor _cellTappedExecutor;
        private Board _board;

        public InputHandler(ICellTappedExecutor cellTappedExecutor)
        {
            _cellTappedExecutor = cellTappedExecutor;
        }

        public void SetBoard(Board board)
        {
            _board = board;
        }
        
        public void HandleClick(Vector2 worldPosition)
        {
            var cell = GetClosestCell(worldPosition);
            if (cell is null)
            {
                return;
            }

            _cellTappedExecutor.TryExecuteTap(_board, cell);
        }

        private Cell GetClosestCell(Vector2 swipeStartPos)
        {
            foreach (var cell in _board.Cells)
            {
                if (IsPositionWithinCell(cell))
                {
                    return cell;
                }
            }

            return null;

            bool IsPositionWithinCell(Cell cell)
            {
                return (swipeStartPos.x > cell.WorldCoordinates.x - Cell.PieceOffset.x && swipeStartPos.x < cell.WorldCoordinates.x + Cell.PieceOffset.x)
                       && (swipeStartPos.y > cell.WorldCoordinates.y - Cell.PieceOffset.y && swipeStartPos.y < cell.WorldCoordinates.y + Cell.PieceOffset.y);
            }
        }
    }
}