using System;
using System.Collections.Generic;
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

            if (String.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name is null or empty.", nameof(propertyName));

            foreach (var name in propertyName.Split('.'))
            {
                var pi = source.GetType().GetPropertyOrThrow(name, ignoreCase);

                source = pi.GetValue(source, null);
            }

            return source;
        }

        /// <summary>
        /// Set an object's property using reflection. Properties that are read only
        /// cannot be successfully set by this method.
        /// </summary>
        /// <param name="source">The object to set the property on.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="value">Property value.</param>
        /// <param name="ignoreCase">Ignore the case of the property.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="propertyName" /> is null or empty.</exception>
        /// <exception cref="T:System.InvalidOperationException">Property does not exist.</exception>
        /// <exception cref="T:System.InvalidOperationException">Property is not writable.</exception>
        public static void SetProperty(this object source, string propertyName, object value, bool ignoreCase = false)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (String.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name is null or empty.", nameof(propertyName));

            var pi = source.GetType().GetPropertyOrThrow(propertyName, ignoreCase);

            if (!pi.CanWrite)
                ExceptionThrower.ThrowPropertyIsNotWriteable(source.GetType(), propertyName);

            pi.SetValue(source, value);
        }

        /// <summary>
        /// Set an object's readonly property using reflection.
        /// </summary>
        /// <param name="source">The object to set the property on.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="value">Property value.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="propertyName" /> is null or empty.</exception>
        /// <exception cref="T:System.InvalidOperationException">Property does not exist.</exception>
        public static void SetReadOnlyProperty(this object source, string propertyName, object value)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (String.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name is null or empty.", nameof(propertyName));

            var fieldInfo = GetBackingField(source.GetType(), propertyName);

            if (fieldInfo == null)
                ExceptionThrower.ThrowPropertyDoesNotExist(source.GetType(), propertyName);

            fieldInfo.SetValue(source, value);
        }

        /// <summary>
        /// Returns an object's properties as a dictionary of property name values.
        /// </summary>
        /// <param name="source">The collection to perform the operation on.</param>
        /// <param name="bindingFlags">Binding flags used to select the properties.</param>
        /// <returns>Dictionary of property name values.</returns>
        public static IDictionary<string, object> GetPropertiesAsDictionary(this object source, BindingFlags bindingFlags)
        {
            var dict = new Dictionary<string, object>();

            if (source == null)
                return dict;

            var properties = source.GetType().GetProperties(bindingFlags);

            foreach (var property in properties)
            {
                dict.Add(property.Name, property.GetValue(source));
            }

            return dict;
        }

        private static FieldInfo GetBackingField(Type type, string propertyName)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            return type.GetField("<" + propertyName + ">k__BackingField", flags);
        }
    }
}