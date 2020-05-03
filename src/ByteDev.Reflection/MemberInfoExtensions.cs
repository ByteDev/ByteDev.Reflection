using System;
using System.Linq;
using System.Reflection;

namespace ByteDev.Reflection
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Reflection.MemberInfo" />.
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Determines whether <paramref name="source" /> has attribute applied of type <typeparamref name="TAttribute" />.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute to check for.</typeparam>
        /// <param name="source">The member or method to check whether has the attribute.</param>
        /// <returns>True if <paramref name="source" /> has the attribute; otherwise returns false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static bool HasAttribute<TAttribute>(this MemberInfo source) where TAttribute : Attribute
        {
            return GetAttribute<TAttribute>(source) != null;
        }

        /// <summary>
        /// Retrieves an applied attribute.
        /// </summary>
        /// <typeparam name="TAttribute">Attribute type to return.</typeparam>
        /// <param name="source">The object to perform the operation on.</param>
        /// <returns>The attribute if found; otherwise null.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">Multiple attributes exist.</exception>
        public static TAttribute GetAttribute<TAttribute>(this MemberInfo source) where TAttribute : Attribute
        {
            if(source == null)
                throw new ArgumentNullException(nameof(source));

            var attributes = source.GetCustomAttributes(typeof(TAttribute), true);

            if (attributes.Length == 1)
            {
                return (TAttribute)attributes.Single();
            }

            if (attributes.Length > 1)
            {
                throw new InvalidOperationException($"Multiple attributes of type: '{typeof(TAttribute)}' exist on '{source}'.");
            }

            return null;
        }
    }
}