using System;
using System.Reflection;

namespace ByteDev.Reflection
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Type" />.
    /// </summary>
    public static class TypeIsExtensions
    {
        /// <summary>
        /// Indicates if a type is in a particular namespace hierarchy.
        /// </summary>
        /// <param name="source">The type to perform the operation on.</param>
        /// <param name="namespace">The namespace.</param>
        /// <returns>True if the type is in the namespace hierarchy; otherwise false.</returns>
        public static bool IsInNamespace(this Type source, string @namespace)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(@namespace))
                throw new ArgumentException("Namespace was null or empty.", nameof(@namespace));

            var typeNamespace = source.Namespace ?? string.Empty;

            if (@namespace == typeNamespace)
                return true;

            if (@namespace.Length > typeNamespace.Length)
                return false;

            var typeSubNamespace = typeNamespace.Substring(0, @namespace.Length);

            if (typeSubNamespace == @namespace)
            {
                return typeNamespace[@namespace.Length] == '.';
            }

            return false;
        }

        /// <summary>
        /// Indicates if a type can be set to null.
        /// </summary>
        /// <param name="source">The type to perform the operation on.</param>
        /// <returns>True the type can be set to null; otherwise false.</returns>
        public static bool IsNullable(this Type source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!source.IsValueType)
                return true;

            return Nullable.GetUnderlyingType(source) != null;
        }

        /// <summary>
        /// Indicates if a type is a test class. Determined by the class's
        /// name suffix.
        /// </summary>
        /// <param name="source">The type to perform the operation on.</param>
        /// <returns>True if the class is a test class; otherwise false.</returns>
        public static bool IsTestClass(this Type source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var typeInfo = source.GetTypeInfo();

            return typeInfo.IsClass && (typeInfo.Name.EndsWith("Tests") || typeInfo.Name.EndsWith("Test"));
        }
    }
}