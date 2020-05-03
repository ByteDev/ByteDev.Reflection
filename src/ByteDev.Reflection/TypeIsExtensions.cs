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