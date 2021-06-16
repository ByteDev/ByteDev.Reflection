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

            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name is null or empty.", nameof(propertyName));

            foreach (var name in propertyName.Split('.'))
            {
                var pi = source.GetType().GetPropertyOrThrow(name, ignoreCase);

                source = pi.GetValue(source, null);
            }

            return source;
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

        /// <summary>
        /// Set an object's readonly property value using reflection.
        /// </summary>
        /// <param name="source">The object to set the property on.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="value">Property value.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="propertyName" /> is null or empty.</exception>
        /// <exception cref="T:System.InvalidOperationException">Property does not exist.</exception>
        public static void SetPropertyReadOnlyValue(this object source, string propertyName, object value)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name is null or empty.", nameof(propertyName));

            var fieldInfo = source.GetType().GetBackingField(propertyName);

            if (fieldInfo == null)
                ExceptionThrower.ThrowPropertyDoesNotExist(source.GetType(), propertyName);

            fieldInfo.SetValue(source, value);
        }

        /// <summary>
        /// Set an object's property value using reflection. If <paramref name="value" /> is
        /// not of the same type as the property then a type conversion is attempted.
        /// </summary>
        /// <param name="source">The object to set the property on.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="value">Property value.</param>
        /// <param name="ignoreCase">Ignore the case of the property name.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="propertyName" /> is null or empty.</exception>
        /// <exception cref="T:System.InvalidOperationException">Property does not exist.</exception>
        public static void SetPropertyValue(this object source, string propertyName, object value, bool ignoreCase = false)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var pi = source.GetType().GetPropertyOrThrow(propertyName, ignoreCase);

            SetPropertyValue(source, pi, value);
        }

        /// <summary>
        /// Set an object's property value using reflection. If <paramref name="value" /> is
        /// not of the same type as the property then a type conversion is attempted.
        /// </summary>
        /// <param name="source">The object to set the property on.</param>
        /// <param name="pi">Property name.</param>
        /// <param name="value">Property value.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="pi" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">Property's type cannot be set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">Property is not writable.</exception>
        public static void SetPropertyValue(this object source, PropertyInfo pi, object value)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (pi == null)
                throw new ArgumentNullException(nameof(pi));

            if (!pi.CanWrite)
                ExceptionThrower.ThrowPropertyIsNotWriteable(source.GetType(), pi.Name);

            if (value == null)
            {
                if (pi.PropertyType.IsNullable())
                    pi.SetValue(source, null);
                else
                    throw new InvalidOperationException($"Property's type '{pi.PropertyType}' cannot be set to null.");
            }

            else if (pi.PropertyType == value.GetType())
                pi.SetValue(source, value);

            else if (pi.PropertyType == typeof(string))
                pi.SetValue(source, value.ToString());

            else if (pi.PropertyType.IsEnum)
                pi.SetValue(source, Enum.Parse(pi.PropertyType, value.ToString()));

            else
                pi.SetValue(source, Convert.ChangeType(value, pi.PropertyType));
        }
    }
}