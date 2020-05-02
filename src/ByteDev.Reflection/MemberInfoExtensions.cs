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