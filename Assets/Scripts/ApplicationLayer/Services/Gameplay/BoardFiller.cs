using System;
using System.Linq;
using ApplicationLayer.Services.Random;
using DomainLayer.Gameplay;

namespace ApplicationLayer.Services.Gameplay
{
    public class BoardFiller : IBoardFiller
    {
        private readonly PieceSpawnConfig[] _availablePiecesSpawnConfigs;
        private readonly IRandomFacade _randomFacade;

        public BoardFiller(PieceSpawnConfig[] availablePiecesSpawnConfigs, IRandomFacade randomFacade)
        {
            _availablePiecesSpawnConfigs = availablePiecesSpawnConfigs;
            _randomFacade = randomFacade;
        }

        public void Fill(Board board)
        {
            for (var row = 0; row < board.Rows; ++row)
            {
                for (var col = 0; col < board.Columns; ++col)
                {
                    board.Cells[row, col].Piece = GetValidPiece();
                }
            }
        }

        private Piece GetValidPiece()
        {
            var validAvailablePieces = _availablePiecesSpawnConfigs.Where(c => c.Weight > 0).ToList();;
            var randomPiece = GetRandomPiece(validAvailablePieces.ToArray());
            return randomPiece;
        }

        private Piece GetRandomPiece(PieceSpawnConfig[] availablePieces)
        {
            var totalWeight = availablePieces.Sum(pieceSpawnConfig => pieceSpawnConfig.Weight);
            var randomWeight = _randomFacade.Next(totalWeight);

            var deltaWeight = 0;
            foreach (var pieceSpawnConfig in availablePieces)
            {
                if (!Array.Exists(availablePieces, pConfig => pConfig.PieceType == pieceSpawnConfig.PieceType)) { continue; }
                
                deltaWeight += pieceSpawnConfig.Weight;
                if (deltaWeight > randomWeight)
                {
                    return new Piece(pieceSpawnConfig.PieceType);
                }
            }
            
            return default;
        }
    }
}