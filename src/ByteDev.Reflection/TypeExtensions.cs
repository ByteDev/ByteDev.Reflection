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
        /// Determines whether type has attribute applied.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute to check for.</typeparam>
        /// <param name="source">The type to perform the operation on.</param>
        /// <returns>True if <paramref name="source" /> has the attribute <typeparamref name="TAttribute" />; otherwise returns false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static bool HasAttribute<TAttribute>(this Type source) where TAttribute : Attribute
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.GetAttribute<TAttribute>() != null;
        }

        /// <summary>
        /// Retrieves a type's property regardless of its access modifier.
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

            return PropertyInfoHelper.GetPropertyInfo(source, propertyName, ignoreCase, flags);
        }

        /// <summary>
        /// Retrieves a type's static property.
        /// </summary>
        /// <param name="source">The type to perform the operation on.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="ignoreCase">Ignore the case of the property.</param>
        /// <returns>PropertyInfo if it exists; otherwise throws exception.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="propertyName" /> is null or empty.</exception>
        /// <exception cref="T:System.InvalidOperationException">Property does not exist.</exception>
        public static PropertyInfo GetStaticPropertyOrThrow(this Type source, string propertyName, bool ignoreCase = false)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

            return PropertyInfoHelper.GetPropertyInfo(source, propertyName, ignoreCase, flags);
        }

        /// <summary>
        /// Retrieves a type's static property value using reflection.
        /// </summary>
        /// <typeparam name="TValue">Type of value to return.</typeparam>
        /// <param name="source">The type to perform the operation on.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="ignoreCase">Ignore the case of the property.</param>
        /// <returns>Property value for the corresponding property name.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="propertyName" /> is null or empty.</exception>
        /// <exception cref="T:System.InvalidOperationException">Property does not exist.</exception>
        public static TValue GetStaticPropertyValue<TValue>(this Type source, string propertyName, bool ignoreCase = false)
        {
            var pi = GetStaticPropertyOrThrow(source, propertyName, ignoreCase);

            return (TValue)pi.GetValue(null, null);
        }

        /// <summary>
        /// Retrieves all constants on the type.
        /// </summary>
        /// <param name="source">The type to perform the operation on.</param>
        /// <param name="publicOnly">True returns only public constants; otherwise returns all constants.</param>
        /// <returns>Collection of FieldInfo on constants.</returns>
        public static IEnumerable<FieldInfo> GetConstants(this Type source, bool publicOnly = false)
        {
            if(source == null)
                throw new ArgumentNullException(nameof(source));

            var flags = publicOnly ? 
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy :
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic;

            var fieldInfos = source.GetFields(flags);

            return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly);
        }

        /// <summary>
        /// Retrieves all constant values on the type. Restricted to constants of the same type.
        /// </summary>
        /// <typeparam name="TValue">Type of constant values.</typeparam>
        /// <param name="source">The type to perform the operation on.</param>
        /// <param name="publicOnly">True returns only public constants; otherwise returns all constants.</param>
        /// <returns>Collection of constant values.</returns>
        public static IEnumerable<TValue> GetConstantsValues<TValue>(this Type source, bool publicOnly = false) where TValue : class
        {
            var fieldInfos = GetConstants(source, publicOnly);

            return fieldInfos.Select(fi => fi.GetRawConstantValue() as TValue);
        }

        /// <summary>
        /// Indicates if a class is a test class. Determined by the class's
        /// name suffix.
        /// </summary>
        /// <param name="source">The type to perform the operation on.</param>
        /// <returns>True if the class is a test class; otherwise returns false.</returns>
        public static bool IsTestClass(this Type source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var typeInfo = source.GetTypeInfo();

            return typeInfo.IsClass && (typeInfo.Name.EndsWith("Tests") || typeInfo.Name.EndsWith("Test"));
        }
    }
}