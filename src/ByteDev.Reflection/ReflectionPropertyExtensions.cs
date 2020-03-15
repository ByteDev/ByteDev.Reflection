using System;
using System.Reflection;

namespace ByteDev.Reflection
{
    /// <summary>
    /// Reflection property extension methods. 
    /// </summary>
    public static class ReflectionPropertyExtensions
    {
        /// <summary>
        /// Retrieves a property value using reflection.
        /// </summary>
        /// <typeparam name="TValue">Type of value to return.</typeparam>
        /// <param name="source">Object that contains the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Property value for the corresponding property name.</returns>
        /// <exception cref="T:System.InvalidOperationException"> when property with name <paramref name="propertyName" /> does not exist.</exception>
        /// <exception cref="T:System.InvalidCastException"> when property with name <paramref name="propertyName" /> is not of type <typeparamref name="TValue" />.</exception>
        public static TValue GetPropertyValue<TValue>(this object source, string propertyName)
        {
            var value = GetPropertyValue(source, propertyName);
            return (TValue)value;
        }

        /// <summary>
        /// Retrieves a property value as an object type using reflection.
        /// </summary>
        /// <param name="source">Object that contains the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Property value for the corresponding property name.</returns>
        /// <exception cref="T:System.InvalidOperationException"> when property with name <paramref name="propertyName" /> does not exist.</exception>
        public static object GetPropertyValue(this object source, string propertyName)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            foreach (var name in propertyName.Split('.'))
            {
                var type = source.GetType();
                var propertyInfo = type.GetPropertyInfoOrThrow(name);
                source = propertyInfo.GetValue(source, null);
            }

            return source;
        }

        /// <summary>
        /// Retrieves a static property value using reflection.
        /// </summary>
        /// <typeparam name="TValue">Type of value to return.</typeparam>
        /// <param name="source">Type that contains the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Property value for the corresponding property name.</returns>
        /// <exception cref="T:System.InvalidOperationException"> when property with name <paramref name="propertyName" /> does not exist.</exception>
        public static TValue GetStaticPropertyValue<TValue>(this Type source, string propertyName)
        {
            var propertyInfo = source.GetStaticPropertyInfoOrThrow(propertyName);
            return (TValue)propertyInfo.GetValue(null, null);
        }

        /// <summary>
        /// Retrieves a static property value as an object type using reflection.
        /// </summary>
        /// <typeparam name="TValue">Type of value to return.</typeparam>
        /// <param name="source">Type that contains the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Property value for the corresponding property name.</returns>
        /// <exception cref="T:System.InvalidOperationException"> when property with name <paramref name="propertyName" /> does not exist.</exception>
        public static TValue GetStaticPropertyValue<TValue>(this object source, string propertyName)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var type = source.GetType();
            return GetStaticPropertyValue<TValue>(type, propertyName);
        }

        public static PropertyInfo GetPropertyInfoOrThrow(this Type source, string propertyName)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var propertyInfo = source.GetProperty(propertyName);

            if (propertyInfo == null)
            {
                ExceptionThrower.ThrowPropertyDoesNotExist(source, propertyName);
            }
            return propertyInfo;
        }

        public static PropertyInfo GetStaticPropertyInfoOrThrow(this Type source, string propertyName)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var propertyInfo = source.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            if (propertyInfo == null)
            {
                ExceptionThrower.ThrowPropertyDoesNotExist(source, propertyName);
            }
            return propertyInfo;
        }

        public static void SetProperty(this object source, string propertyName, object propertyValue, bool ignoreNameCase = false)
        {
            if(source == null)
                throw new ArgumentNullException(nameof(source));

            if(string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name was null or empty.", nameof(propertyName));

            var pi = source.GetProperty(propertyName, ignoreNameCase);

            if (pi != null && pi.CanWrite)
            {
                pi.SetValue(source, propertyValue, null);
            }
        }

        public static void SetPropertyOrThrow(this object source, string propertyName, object propertyValue, bool ignoreNameCase = false)
        {
            if(source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name was null or empty.", nameof(propertyName));

            var pi = source.GetProperty(propertyName, ignoreNameCase);

            if(pi == null) 
                ExceptionThrower.ThrowPropertyDoesNotExist(source.GetType(), propertyName);

            if(pi.CanWrite) 
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