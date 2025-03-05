using System.Collections.Generic;
using DomainLayer.Gameplay;

namespace ApplicationLayer.Services.Gameplay.DTOs
{
    public record CascadeMatchStep(List<Cell> MatchCells) : ICascadeStep;
}