using System;
using System.Collections.Generic;

namespace CreaDev.Framework.Core
{
   
    public static class ServiceLocator
    {
        private static Dictionary<Type, object> _services = new Dictionary<Type, object>();
        private static Dictionary<Type, object> _servicesByModel = new Dictionary<Type, object>();

        public static void Add<Ttype>(object serice)
        {
            _services.Add(typeof(Ttype), serice);
        }
        public static void Add<Ttype,TModel>(object serice)
        {
            _services.Add(typeof(Ttype), serice);
            _servicesByModel.Add(typeof(TModel), serice);
        }
        public static Ttype Get<Ttype>()
        {
            Guard.AgainstFalse(_services.ContainsKey(typeof(Ttype)), $"ther is no service of type {typeof(Ttype)} registerd");
            return (Ttype)_services[typeof(Ttype)];
        }
        public static Ttype GetByModel<Ttype>()
        {
            Guard.AgainstFalse(_servicesByModel.ContainsKey(typeof(Ttype)), $"ther is no service of type {typeof(Ttype)} registerd");
            return (Ttype)_servicesByModel[typeof(Ttype)];
        }


    }
}