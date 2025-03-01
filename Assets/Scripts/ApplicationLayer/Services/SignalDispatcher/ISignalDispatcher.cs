namespace ApplicationLayer.Services.SignalDispatcher
{
    public interface ISignalDispatcher
    {
        void Subscribe<T>(SignalDelegate<T> callback) where T : ISignal;
        void Unsubscribe<T>(SignalDelegate<T> callback) where T : ISignal;
        void UnsubscribeAll<T>() where T : ISignal;
        void Dispatch<T>(T signal) where T : ISignal;
        bool IsSignalSubscribed<T>() where T: ISignal;
    }
}