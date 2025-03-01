using ApplicationLayer.Services.SignalDispatcher;
using DomainLayer.Gameplay;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ViewLayer.Gameplay
{
    public class BoardRenderer : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private TileBase _cellTile1;
        [SerializeField] private TileBase _cellTile2;

        private ISignalDispatcher _signalDispatcher;
        private Board _board;

        public void Inject(ISignalDispatcher signalDispatcher)
        {
            _signalDispatcher = signalDispatcher;
        }

        public void RenderBoard(Board board)
        {
            _board = board;
            
            for (var row = 0; row < board.Rows; ++row)
            {
                for (var col = 0; col < board.Columns; ++col)
                {
                    RenderCell(row, col);
                }
            }
            return;

            void RenderCell(int row, int col)
            {
                var x = col * _board.Rows + row;
                Debug.Log(x);
                _tilemap.SetTile(
                    new Vector3Int(col, row, 0), 
                    x % 2 == 0 ? _cellTile1 : _cellTile2 
                );
            }
        }
    }
}