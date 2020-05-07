using System;
using System.Reflection;

namespace ByteDev.Reflection
{
    public static class GenericExtensions
    {
        /// <summary>
        /// Invokes a method on a type using reflection.
        /// </summary>
        /// <typeparam name="TSource">The type of the object to invoke the method on.</typeparam>
        /// <param name="source">The object to invoke the method on.</param>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="args">Any arguments to pass to the invoked method.</param>
        /// <returns>The value returned from the invoked method. If the return type is void then null will be returned.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="methodName" /> does not exist.</exception>
        public static object InvokeMethod<TSource>(this TSource source, string methodName, params object[] args)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var type = typeof(TSource);
            var method = type.GetTypeInfo().GetDeclaredMethod(methodName);

            if (method == null)
                throw new ArgumentException($"Type: '{type.Name}' does not contain method '{methodName}'.", nameof(methodName));

            return method.Invoke(source, args);
        }
    }
}