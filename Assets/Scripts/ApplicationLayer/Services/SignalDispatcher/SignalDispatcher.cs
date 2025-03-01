using System.Collections.Generic;

namespace ApplicationLayer.Services.SignalDispatcher
{
    public class SignalDispatcher : ISignalDispatcher
    {
        private static class Repository<T> where T : ISignal
        {
            private static readonly IDictionary<ISignalDispatcher, SignalDelegate<T>> _events = new Dictionary<ISignalDispatcher, SignalDelegate<T>>();

            public static void Subscribe(ISignalDispatcher owner, SignalDelegate<T> signalDelegate)
            {
                _events.TryGetValue(owner, out var dispatcher);
                if (dispatcher is null)
                {
                    _events.Add(owner, null);
                }

                _events[owner] += signalDelegate;
            }

            public static void Unsubscribe(ISignalDispatcher owner, SignalDelegate<T> signalDelegate)
            {
                if (!TryGetEventDispatcher(owner, out _))
                {
                    return;
                }

                _events[owner] -= signalDelegate;
                if (_events[owner] is null)
                {
                    _events.Remove(owner);
                }
            }

            public static void UnsubscribeAll(ISignalDispatcher owner)
            {
                if (_events.ContainsKey(owner))
                {
                    _events.Remove(owner);
                }
            }

            public static void Dispatch(ISignalDispatcher owner, T signal)
            {
                if (!TryGetEventDispatcher(owner, out var dispatcher))
                {
                    return;
                }

                dispatcher.Invoke(signal);
            }

            private static bool TryGetEventDispatcher(ISignalDispatcher owner, out SignalDelegate<T> dispatcher)
            {
                _events.TryGetValue(owner, out dispatcher);
                return dispatcher != null;
            }

            public static bool SignalSubscribed(ISignalDispatcher owner)
            {
                return _events.TryGetValue(owner, out var signalDelegate) && signalDelegate != null;
            }
        }

        public void Subscribe<T>(SignalDelegate<T> callback) where T : ISignal
        {
            Repository<T>.Subscribe(this, callback);
        }

        public void Unsubscribe<T>(SignalDelegate<T> callback) where T : ISignal
        {
            Repository<T>.Unsubscribe(this, callback);
        }

        public void UnsubscribeAll<T>() where T : ISignal
        {
            Repository<T>.UnsubscribeAll(this);
        }

        public void Dispatch<T>(T signal) where T : ISignal
        {
            Repository<T>.Dispatch(this, signal);
        }

        public bool IsSignalSubscribed<T>() where T : ISignal
        {
            return Repository<T>.SignalSubscribed(this);
        }
    }
}