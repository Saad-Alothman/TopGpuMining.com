using System;
using System.Collections.Generic;
using CreaDev.Framework.Core.Models;

namespace CreaDev.Framework.Core
{
    public static class PermissionSettingsLocator
    {
        private static Dictionary<Type, PermissionSettings> _permissionSettings = new Dictionary<Type, PermissionSettings>();

        public static void Add<TType>(PermissionSettings permissionSettings)
        {
            _permissionSettings.Add(typeof(TType), permissionSettings);
        }
        public static PermissionSettings Get<TType>(bool throwOnNotFound = true)
        {
            if (throwOnNotFound)
                Guard.AgainstFalse(_permissionSettings.ContainsKey(typeof(TType)), $"ther is no service of type {typeof(TType)} registerd");

            return _permissionSettings[typeof(TType)];
        }


        public static PermissionSettings Get<TType, TFallBackType>(bool throwOnNotFound = true)
        {
            if (_permissionSettings.ContainsKey(typeof (TType)))
                return Get<TType>();

                return Get<TFallBackType>(throwOnNotFound);
        }
    }
}