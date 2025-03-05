using DomainLayer.Gameplay;
using UnityEngine;

namespace ApplicationLayer.Services.Gameplay
{
    public interface IBoardGenerator
    {
        Board GenerateBoard(Vector2Int rowsRange, Vector2Int columnsRange);
    }
}