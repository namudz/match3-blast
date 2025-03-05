using System.Collections.Generic;
using DomainLayer.Gameplay;

namespace ApplicationLayer.Services.Gameplay.DTOs
{
    public record CascadeGravityStep(ICollection<GravityStep> Steps) : ICascadeStep;
    
    public record GravityStep(Cell From, Cell To, int deltaCells);
}