using System;
using System.Collections.Generic;
using System.Reflection;

namespace ByteDev.Reflection
{
    /// <summary>
    /// Represents functionality to initialize objects through calling their
    /// constructors using reflection.
    /// </summary>
    public static class ObjectConstruction
    {
        /// <summary>
        /// Initializes a new instance of <typeparamref name="T" /> through it's non-public
        /// parameterless constructor.
        /// </summary>
        /// <typeparam name="T">Type of object to initialize.</typeparam>
        /// <returns>New instance of type <typeparamref name="T" />.</returns>
        public static T ConstructNonPublic<T>()
        {
            return ConstructNonPublic<T>(new Dictionary<Type, object>());
        }

        /// <summary>
        /// Initializes a new instance of <typeparamref name="T" /> through it's non-public
        /// parameter constructor.
        /// </summary>
        /// <typeparam name="T">Type of object to initialize.</typeparam>
        /// <param name="parameters">Dictionary of parameters.</param>
        /// <returns>New instance of type <typeparamref name="T" />.</returns>
        public static T ConstructNonPublic<T>(IDictionary<Type, object> parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var paramTypes = new Type[parameters.Count];
            var paramObjects = new object[parameters.Count];

            var i = 0;

            foreach (var parameter in parameters)
            {
                paramTypes[i] = parameter.Key;
                paramObjects[i] = parameter.Value;
                i++;
            }

            var constructorInfo = typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, paramTypes, null);

            if (constructorInfo == null)
                throw new InvalidOperationException("No matching constructor could be found.");

            var obj = constructorInfo.Invoke(paramObjects);

            return (T)obj;
        }
    }
}