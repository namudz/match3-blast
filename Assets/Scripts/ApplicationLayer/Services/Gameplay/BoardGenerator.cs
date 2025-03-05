using ApplicationLayer.Services.Random;
using DomainLayer.Gameplay;
using UnityEngine;

namespace ApplicationLayer.Services.Gameplay
{
    public class BoardGenerator : IBoardGenerator
    {
        private readonly IRandomFacade _randomFacade;

        public BoardGenerator(IRandomFacade randomFacade)
        {
            _randomFacade = randomFacade;
        }
        
        public Board GenerateBoard(Vector2Int rowsRange, Vector2Int columnsRange)
        {
            var rows = _randomFacade.Next(rowsRange.x, rowsRange.y + 1);
            var columns = _randomFacade.Next(columnsRange.x, columnsRange.y + 1);

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