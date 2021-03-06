﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ByteDev.Reflection
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Reflection.Assembly" />.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Retrieves the name of the file from the assembly manifest.
        /// </summary>
        /// <param name="source">Assembly to perform the operation on.</param>
        /// <param name="fileName">File name to search for.</param>
        /// <returns>Resource name from the assembly manifest.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.IO.FileNotFoundException">Embedded file <paramref name="fileName" /> could not be found in the assembly.</exception>
        public static string GetManifestResourceName(this Assembly source, string fileName)
        {
            if(source == null)
                throw new ArgumentNullException(nameof(source));

            string name = source.GetManifestResourceNames().SingleOrDefault(n => n.EndsWith(fileName, StringComparison.InvariantCultureIgnoreCase));

            if (string.IsNullOrEmpty(name))
                throw new FileNotFoundException($"Embedded file '{fileName}' could not be found in assembly '{source.FullName}'.", fileName);

            return name;
        }

        /// <summary>
        /// Returns version information for the <paramref name="source" />.
        /// </summary>
        /// <param name="source">The assembly to get version information on.</param>
        /// <returns><see cref="T:System.Version" /> for <paramref name="source" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static Version GetVersion(this Assembly source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.GetName().Version;
        }

        /// <summary>
        /// Returns file version information for the <paramref name="source" />.
        /// </summary>
        /// <param name="source">The assembly to get file version information on.</param>
        /// <returns><see cref="T:System.Version" /> for <paramref name="source" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static Version GetFileVersion(this Assembly source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var fileVersionAttributes = (AssemblyFileVersionAttribute[])source.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true);

            return fileVersionAttributes.Length > 0 ? new Version(fileVersionAttributes[0].Version) : null;
        }

        /// <summary>
        /// Return a list of all the types that are subclasses of a particular class within <paramref name="source" />.
        /// </summary>
        /// <typeparam name="TClass">The class type to return all subclasses on.</typeparam>
        /// <param name="source">The assembly where <typeparamref name="TClass" /> exists.</param>
        /// <returns>List of types that are subclasses of <typeparamref name="TClass" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static IEnumerable<Type> GetSubClasses<TClass>(this Assembly source) where TClass : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            
            return source.GetTypes().Where(type => type.IsSubclassOf(typeof(TClass)));
        }

        /// <summary>
        /// Returns a <see cref="T:System.Attribute" /> from within <paramref name="source" />.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute class to search for.</typeparam>
        /// <param name="source">The assembly where <typeparamref name="TAttribute" /> exists.</param>
        /// <returns>Instance of <typeparamref name="TAttribute" /> if exists; otherwise returns null.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static TAttribute GetAssemblyAttribute<TAttribute>(this Assembly source) where TAttribute : Attribute
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            
            object[] attributes = source.GetCustomAttributes(typeof(TAttribute), false);

            if (attributes.Length == 0)
                return null;
            
            return attributes.OfType<TAttribute>().SingleOrDefault();
        }
    }
}