using System;
using ApplicationLayer.Services.Gameplay.DTOs;
using DomainLayer.Gameplay;

namespace ApplicationLayer.Services.Gameplay.CascadeSteps
{
    public interface ICellTappedExecutor
    {
        event Action<CascadeMatchStep> OnCellTapped;
        void TryExecuteTap(Board board, Cell cell);
    }
}