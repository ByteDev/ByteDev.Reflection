using System;
using System.Reflection;

namespace ByteDev.Reflection
{
    internal static class TypeExtensions
    {
        internal static PropertyInfo GetPropertyInfoOrThrow(this Type source, string propertyName)
        {
            var propertyInfo = source.GetProperty(propertyName);

            if (propertyInfo == null)
            {
                ThrowTypeDoesNotHaveNamedProperty(source, propertyName);
            }
            return propertyInfo;
        }

        internal static PropertyInfo GetStaticPropertyInfoOrThrow(this Type source, string propertyName)
        {
            var propertyInfo = source.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Static);

            if (propertyInfo == null)
            {
                ThrowTypeDoesNotHaveNamedProperty(source, propertyName);
            }
            return propertyInfo;
        }

        private static void ThrowTypeDoesNotHaveNamedProperty(Type source, string propertyName)
        {
            throw new InvalidOperationException($"Type '{source.Name}' has no property called '{propertyName}'.");
        }
    }
}