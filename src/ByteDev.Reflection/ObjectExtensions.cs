using System;

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

        /// <summary>
        /// Set an object's property using reflection.
        /// </summary>
        /// <param name="source">The object to set the property on.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyValue">Property value.</param>
        /// <param name="ignoreCase">Ignore the case of the property.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="propertyName" /> is null or empty.</exception>
        /// <exception cref="T:System.InvalidOperationException">Property does not exist.</exception>
        /// <exception cref="T:System.InvalidOperationException">Property is not writable.</exception>
        public static void SetProperty(this object source, string propertyName, object propertyValue, bool ignoreCase = false)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name is null or empty.", nameof(propertyName));

            var pi = source.GetType().GetPropertyOrThrow(propertyName, ignoreCase);

            if (!pi.CanWrite)
                ExceptionThrower.ThrowPropertyIsNotWriteable(source.GetType(), propertyName);

            pi.SetValue(source, propertyValue, null);
        }

        /// <summary>
        /// Retrieves a property value using reflection.
        /// </summary>
        /// <typeparam name="TValue">Type of value to return.</typeparam>
        /// <param name="source">Object that contains the property.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="ignoreCase">Ignore the case of the property.</param>
        /// <returns>Property value; otherwise throws exception.</returns>
        /// <exception cref="T:System.InvalidOperationException">Property does not exist.</exception>
        /// <exception cref="T:System.InvalidCastException">Property type is not of type <typeparamref name="TValue" />.</exception>
        public static TValue GetPropertyValue<TValue>(this object source, string propertyName, bool ignoreCase = false)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var value = GetPropertyValue(source, propertyName, ignoreCase);

            return (TValue)value;
        }

        /// <summary>
        /// Retrieves a property value as a specified type using reflection.
        /// </summary>
        /// <param name="source">Object that contains the property.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="ignoreCase">Ignore the case of the property.</param>
        /// <returns>Property value; otherwise throws exception.</returns>
        /// <exception cref="T:System.InvalidOperationException">Property does not exist.</exception>
        public static object GetPropertyValue(this object source, string propertyName, bool ignoreCase = false)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            foreach (var name in propertyName.Split('.'))
            {
                var pi = source.GetType().GetPropertyOrThrow(name, ignoreCase);

                source = pi.GetValue(source, null);
            }

            return source;
        }
    }
}