using System.Collections.Generic;
using DomainLayer.Gameplay;
using UnityEngine;

namespace ApplicationLayer.Services.Gameplay.DTOs
{
    public record CascadeRefillStep(ICollection<RefillStep> Steps) : ICascadeStep;
    
    public record RefillStep(Vector3 SpawnWorldCoordinates, Cell To, int deltaCells);
}