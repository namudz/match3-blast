using System.Collections.Generic;
using ApplicationLayer.Services.Gameplay.DTOs;
using ApplicationLayer.Services.SignalDispatcher;

namespace ApplicationLayer.Services.Gameplay.Signals
{
    public record CascadeStartedSignal(List<ICascadeStep> CascadeSteps) : ISignal;
}