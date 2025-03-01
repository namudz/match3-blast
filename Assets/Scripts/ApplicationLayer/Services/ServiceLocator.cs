using System;
using System.Collections.Generic;

namespace ApplicationLayer.Services
{
    public class ServiceLocator
    {
        public static ServiceLocator Instance => _instance ??= new ServiceLocator();
        private static ServiceLocator _instance;

        private readonly Dictionary<Type, object> _services;

        private ServiceLocator()
        {
            _services = new Dictionary<Type, object>();
        }

        public void RegisterService<T>(T service)
        {
            var type = typeof(T);
            if (!_services.ContainsKey(type))
            {
                _services.Add(type, service);
            }
        }

        public bool UnregisterService<T>()
        {
            var type = typeof(T);
            return _services.ContainsKey(type) && _services.Remove(type);
        }

        public T GetService<T>()
        {
            var type = typeof(T);
            if (!_services.TryGetValue(type, out var service))
            {
                throw new Exception($"Service {type} not found");
            }

            return (T) service;
        }

        public bool ServiceExist<T>()
        {
            return _services.ContainsKey(typeof(T));
        }
    }
}