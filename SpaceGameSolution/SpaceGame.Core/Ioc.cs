using System;
using System.Collections.Generic;

namespace SpaceGame.Core
{
    public static class Ioc
    {
        private static readonly Dictionary<string, Func<object[], object>> _resolvers = new();

        public static void Register(string key, Func<object[], object> resolver)
        {
            _resolvers[key] = resolver;
        }

        public static object Resolve(string key, params object[] args)
        {
            if (_resolvers.TryGetValue(key, out var resolver))
                return resolver(args);
            throw new InvalidOperationException($"Зависимость '{key}' не зарегистрирована");
        }
    }
}