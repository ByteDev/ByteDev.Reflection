using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ByteDev.Reflection
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Type" />.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Get a type's property.
        /// </summary>
        /// <param name="source">The type to perform the operation on.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="ignoreCase">Ignore the case of the property.</param>
        /// <returns>PropertyInfo if it exists; otherwise throws exception.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="propertyName" /> is null or empty.</exception>
        /// <exception cref="T:System.InvalidOperationException">Property does not exist.</exception>
        public static PropertyInfo GetPropertyOrThrow(this Type source, string propertyName, bool ignoreCase = false)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            return GetPropertyInfo(source, propertyName, ignoreCase, flags);
        }





        /// <summary>
        /// Retrieves a static property value using reflection.
        /// </summary>
        /// <typeparam name="TValue">Type of value to return.</typeparam>
        /// <param name="source">Type that contains the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="ignoreCase">Ignore the case of the property.</param>
        /// <returns>Property value for the corresponding property name.</returns>
        /// <exception cref="T:System.InvalidOperationException">Property does not exist.</exception>
        public static TValue GetStaticPropertyValue<TValue>(this Type source, string propertyName, bool ignoreCase = false)
        {
            var pi = GetStaticPropertyOrThrow(source, propertyName, ignoreCase);

            return (TValue)pi.GetValue(null, null);
        }

        public static PropertyInfo GetStaticPropertyOrThrow(this Type source, string propertyName, bool ignoreCase = false)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

            return GetPropertyInfo(source, propertyName, ignoreCase, flags);
        }







        /// <summary>
        /// Retrieve all constants on the type.
        /// </summary>
        /// <param name="source">The type to perform the operation on.</param>
        /// <returns>Collection of info on constants.</returns>
        public static IEnumerable<FieldInfo> GetConstants(this Type source)
        {
            var fieldInfos = source.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly);
        }

        /// <summary>
        /// Retrieve all values of constants on the type. Restricted to constants of the same type.
        /// </summary>
        /// <typeparam name="TValue">Type of constant values.</typeparam>
        /// <param name="source">The type to perform the operation on.</param>
        /// <returns>Collection of constant values.</returns>
        public static IEnumerable<TValue> GetConstantsValues<TValue>(this Type source) where TValue : class
        {
            var fieldInfos = GetConstants(source);

            return fieldInfos.Select(fi => fi.GetRawConstantValue() as TValue);
        }

        /// <summary>
        /// Checks whether <paramref name="source" /> has attribute <typeparamref name="TAttribute" />.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute to check for.</typeparam>
        /// <param name="source">The type to check whether has the attribute.</param>
        /// <returns>True if <paramref name="source" /> has the attribute <typeparamref name="TAttribute" />; otherwise returns false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static bool HasAttribute<TAttribute>(this Type source) where TAttribute : Attribute
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.GetAttribute<TAttribute>() != null;
        }



        internal static PropertyInfo GetPropertyInfo(Type source, string propertyName, bool ignoreCase, BindingFlags flags)
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