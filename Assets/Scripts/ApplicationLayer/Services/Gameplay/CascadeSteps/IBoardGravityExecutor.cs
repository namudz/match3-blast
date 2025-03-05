using ApplicationLayer.Services.Gameplay.DTOs;
using DomainLayer.Gameplay;

namespace ApplicationLayer.Services.Gameplay.CascadeSteps
{
    public interface IBoardGravityExecutor
    {
        CascadeGravityStep TryApplyGravity(Board board);
    }
}