using ApplicationLayer.Services.Pooling;
using ApplicationLayer.Services.SignalDispatcher;
using DomainLayer.Gameplay;
using UnityEngine;
using UnityEngine.Tilemaps;
using ViewLayer.Gameplay.Pieces;

namespace ViewLayer.Gameplay
{
    public class BoardRenderer : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private TileBase _cellTile1;
        [SerializeField] private TileBase _cellTile2;

        private IPiecesPoolsFacade _piecesPoolsFacade;
        private ISignalDispatcher _signalDispatcher;
        
        private Board _board;
        private PieceController[,] _instantiatedPieces;

        public void Inject(IPiecesPoolsFacade piecesPoolsFacade, ISignalDispatcher signalDispatcher)
        {
            _piecesPoolsFacade = piecesPoolsFacade;
            _signalDispatcher = signalDispatcher;
        }

        public void RenderBoard(Board board)
        {
            _board = board;
            _instantiatedPieces = new PieceController[board.Rows, board.Columns];
            
            for (var row = 0; row < board.Rows; ++row)
            {
                for (var col = 0; col < board.Columns; ++col)
                {
                    RenderCell(col * _board.Rows + row, row, col);
                    RenderCellPiece(row, col);
                }
            }
            return;

            void RenderCell(int index, int row, int col)
            {
                _tilemap.SetTile(
                    new Vector3Int(col, row, 0), 
                    index % 2 == 0 ? _cellTile1 : _cellTile2 
                );
            }
            
            void RenderCellPiece(int row, int col)
            {
                var cell = board.GetCell(row, col);
                if (cell.IsEmpty) { return; }
                
                _instantiatedPieces[row, col] = _piecesPoolsFacade.GetInstance(
                    cell.Piece.PieceType,
                    cell.WorldCoordinates
                ).GetComponent<PieceController>();
            }
        }
    }
}