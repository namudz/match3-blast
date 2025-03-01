namespace ApplicationLayer.Services.SignalDispatcher
{
    public delegate void SignalDelegate<in T>(T signal) where T : ISignal;
    
    public interface ISignal
    {
    }
}