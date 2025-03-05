using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Services.Gameplay.DTOs;
using ApplicationLayer.Services.Pooling;
using ApplicationLayer.Services.SignalDispatcher;
using Cysharp.Threading.Tasks;
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

        public async Task DestroyMatchPieces(List<Cell> matchCells)
        {
            await TryDestroyMatchCells(matchCells);
        }

        private async UniTask TryDestroyMatchCells(List<Cell> matchCells)
        {
            if (!matchCells.Any()) { return; }

            var destroyTasks = new List<UniTask>();
            foreach (var cell in matchCells)
            {
                var piece = _instantiatedPieces[cell.Coordinates.x, cell.Coordinates.y];

                destroyTasks.Add(piece.DestroyPiece());
            }

            await UniTask.WhenAll(destroyTasks);

            foreach (var cell in matchCells)
            {
                if (_instantiatedPieces[cell.Coordinates.x, cell.Coordinates.y] is null) { continue; }
                
                _piecesPoolsFacade.BackToPool(cell.Piece.PieceType, _instantiatedPieces[cell.Coordinates.x, cell.Coordinates.y].gameObject);
                _instantiatedPieces[cell.Coordinates.x, cell.Coordinates.y] = null;
            }
        }

        public async UniTask ApplyGravity(IEnumerable<GravityStep> gravitySteps)
        {
            var fallTasks = new List<UniTask>();
            foreach (var gravityStep in gravitySteps)
            {
                var fallingPiece = _instantiatedPieces[gravityStep.From.Coordinates.x, gravityStep.From.Coordinates.y];

                if (fallingPiece is null){ continue; }
            
                fallTasks.Add(fallingPiece.FallTo(gravityStep.To.WorldCoordinates, gravityStep.deltaCells));
                
                (_instantiatedPieces[gravityStep.From.Coordinates.x, gravityStep.From.Coordinates.y], _instantiatedPieces[gravityStep.To.Coordinates.x, gravityStep.To.Coordinates.y]) = (_instantiatedPieces[gravityStep.To.Coordinates.x, gravityStep.To.Coordinates.y], _instantiatedPieces[gravityStep.From.Coordinates.x, gravityStep.From.Coordinates.y]);
            }
            
            await UniTask.WhenAll(fallTasks);
        }

        public async Task Refill(ICollection<RefillStep> refillSteps)
        {
            if (!refillSteps.Any()) { return; }
            
            var refillTasks = new List<UniTask>();

            var refillStepsByColumn =refillSteps.GroupBy(x => x.To.Coordinates.y);
            
            foreach (var groupedRefill in refillStepsByColumn)
            {
                var columnRefillSteps = groupedRefill.ToList();
                var maxDeltaCells = columnRefillSteps.OrderByDescending(r => r.deltaCells).First().deltaCells;
                foreach (var (spawnCoordinates, cell, deltaCells) in columnRefillSteps)
                {
                    _instantiatedPieces[cell.Coordinates.x, cell.Coordinates.y] = _piecesPoolsFacade.GetInstance(
                        cell.Piece.PieceType,
                        spawnCoordinates
                    ).GetComponent<PieceController>();

                    refillTasks.Add( 
                        _instantiatedPieces[cell.Coordinates.x, cell.Coordinates.y].SpawnAndFallTo(cell.WorldCoordinates, deltaCells, (maxDeltaCells - deltaCells) * .09f)
                    );
                }
            }
            
            await UniTask.WhenAll(refillTasks);
        }
    }
}