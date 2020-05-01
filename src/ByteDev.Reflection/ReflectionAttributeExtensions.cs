using System;
using System.Reflection;

namespace ByteDev.Reflection
{
    /// <summary>
    /// Attribute reflection related extension methods.
    /// </summary>
    public static class ReflectionAttributeExtensions
    {
        /// <summary>
        /// Checks whether <paramref name="source" /> has attribute <typeparamref name="TAttribute" />.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute to check for.</typeparam>
        /// <param name="source">The type to check whether has the attribute.</param>
        /// <returns>True if <paramref name="source" /> has the attribute <typeparamref name="TAttribute" />; otherwise returns false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static bool HasAttribute<TAttribute>(this Type source) where TAttribute : Attribute
        {
            if(source == null)
                throw new ArgumentNullException(nameof(source));

            return source.GetAttribute<TAttribute>() != null;
        }



        /// <summary>
        /// Checks whether <paramref name="source" /> has attribute <typeparamref name="TAttribute" />.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute to check for.</typeparam>
        /// <param name="source">The member or method to check whether has the attribute.</param>
        /// <returns>True if <paramref name="source" /> has the attribute <typeparamref name="TAttribute" />; otherwise returns false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static bool HasAttribute<TAttribute>(this MemberInfo source) where TAttribute : Attribute
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.GetAttribute<TAttribute>() != null;
        }
    }
}
