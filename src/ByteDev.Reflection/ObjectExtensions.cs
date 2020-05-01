using System;
using System.Reflection;

namespace ByteDev.Reflection
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Object" />.
    /// </summary>
    public static class ObjectExtensions
    {    
        /// <summary>
        /// Checks whether <paramref name="source" /> has attribute <typeparamref name="TAttribute" />.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute to check for.</typeparam>
        /// <param name="source">The object to check whether has the attribute.</param>
        /// <returns>True if <paramref name="source" /> has the attribute <typeparamref name="TAttribute" />; otherwise returns false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static bool HasAttribute<TAttribute>(this object source) where TAttribute : Attribute
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.GetType().GetAttribute<TAttribute>() != null;
        }


        public static void SetProperty(this object source, string propertyName, object propertyValue, bool ignoreNameCase = false)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name was null or empty.", nameof(propertyName));

            var pi = source.GetProperty(propertyName, ignoreNameCase);

            if (pi != null && pi.CanWrite)
            {
                pi.SetValue(source, propertyValue, null);
            }
        }

        public static void SetPropertyOrThrow(this object source, string propertyName, object propertyValue, bool ignoreNameCase = false)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name was null or empty.", nameof(propertyName));

            var pi = source.GetProperty(propertyName, ignoreNameCase);

            if (pi == null)
                ExceptionThrower.ThrowPropertyDoesNotExist(source.GetType(), propertyName);

            if (pi.CanWrite)
                ExceptionThrower.ThrowPropertyIsNotWriteable(source.GetType(), propertyName);

            pi.SetValue(source, propertyValue, null);
        }

        private static PropertyInfo GetProperty(this object source, string propertyName, bool ignoreNameCase)
        {
            if (ignoreNameCase)
            {
                return source.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase);
            }

            return source.GetType().GetProperty(propertyName);
        }

    }
}