using System;
using System.Reflection;

namespace ByteDev.Reflection
{
    internal static class PropertyInfoHelper
    {
        public static PropertyInfo GetPropertyInfo(Type source, string propertyName, bool ignoreCase, BindingFlags flags)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name is null or empty.", nameof(propertyName));

            var pi = ignoreCase ?
                source.GetProperty(propertyName, flags | BindingFlags.IgnoreCase) :
                source.GetProperty(propertyName, flags);

            if (pi == null)
                ExceptionThrower.ThrowPropertyDoesNotExist(source, propertyName);

            return pi;
        }
    }
}