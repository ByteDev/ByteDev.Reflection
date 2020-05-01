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