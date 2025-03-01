using UnityEngine;

namespace DomainLayer.Gameplay
{
    public class Board
    {
        public readonly Cell[,] Cells;

        public int Rows => Cells.GetLength(0); // (X axis)

        public int Columns => Cells.GetLength(1); // (Y axis)

        public Board(Cell[,] cells)
        {
            if (cells is null)
            {
                Debug.LogError("Cells could not be null");
                return;
            }
            
            Cells = cells;
        }

        public Cell GetCell(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Rows || y >= Columns)
            {
                return null;
            }
            
            return Cells[x, y];
        }

        public override string ToString()
        {
            var result = "";
            for (var r = 0; r < Rows; ++r)
            {
                for (var c = 0; c < Columns; ++c)
                {
                    result += $"{string.Join(" :: ", GetCell(r, c).ToString())}\n";
                }
            }

            return result;
        }
    }
}