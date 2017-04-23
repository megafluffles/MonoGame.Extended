using System;

namespace MonoGame.Extended
{
    public static class ServiceProviderExtensions
    {
        public static T GetService<T>(this IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetService(typeof(T));

            if (service == null)
                throw new InvalidOperationException($"Service {typeof(T).Name} not found");

            return (T)service;
        }
    }
}
