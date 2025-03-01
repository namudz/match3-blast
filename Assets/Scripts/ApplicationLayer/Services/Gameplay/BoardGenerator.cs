using System;
using DomainLayer.Gameplay;
using UnityEngine;

namespace ApplicationLayer.Services.Gameplay
{
    public class BoardGenerator : IBoardGenerator
    {
        public Board GenerateBoard(Vector2Int rowsRange, Vector2Int columnsRange)
        {
            var random = new System.Random(Guid.NewGuid().GetHashCode());
            var rows = random.Next(rowsRange.x, rowsRange.y + 1);
            var columns = random.Next(columnsRange.x, columnsRange.y + 1);

            var cells = new Cell[rows, columns];
            for (var row = 0; row < rows; ++row)
            {
                for (var column = 0; column < columns; ++column)
                {
                    cells[row, column] = new Cell(
                        new Vector2Int(row, column),
                        new Vector3((column * Cell.WorldSize.x) + Cell.PieceOffset.x, (row * Cell.WorldSize.y) + Cell.PieceOffset.y, 0f)
                    );
                }
            }

            return new Board(cells);
        }
    }
}