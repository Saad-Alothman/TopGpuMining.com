using System;
using System.Collections.Generic;

namespace CreaDev.Framework.Core
{
   
    public static class ServiceLocator
    {
        private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static void Add<Ttype>(object serice)
        {
            _services.Add(typeof(Ttype), serice);
        }
        public static Ttype Get<Ttype>()
        {
            Guard.AgainstFalse(_services.ContainsKey(typeof(Ttype)), $"ther is no service of type {typeof(Ttype)} registerd");
            return (Ttype)_services[typeof(Ttype)];
        }


    }
}