using DomainLayer.Gameplay;
using UnityEngine;

namespace ApplicationLayer.Services.Gameplay.Input
{
    public interface IInputHandler
    {
        void SetBoard(Board board);
        void HandleClick(Vector2 worldPosition);
    }
}