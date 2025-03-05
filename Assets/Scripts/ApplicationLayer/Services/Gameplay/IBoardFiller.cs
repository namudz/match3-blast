using ApplicationLayer.Services.Gameplay.DTOs;
using DomainLayer.Gameplay;

namespace ApplicationLayer.Services.Gameplay
{
    public interface IBoardFiller
    {
        void Fill(Board board);
        CascadeRefillStep Refill(Board board);
    }
}