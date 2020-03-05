using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ByteDev.Reflection
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Type" />.
    /// </summary>
    public static class ReflectionFieldInfoExtensions
    {
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
    }
}