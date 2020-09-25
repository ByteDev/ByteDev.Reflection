using System;
using System.IO;
using System.Reflection;

namespace ByteDev.Reflection
{
    /// <summary>
    /// Represents an assembly's embedded resource.
    /// </summary>
    public class AssemblyEmbeddedResource
    {
        /// <summary>
        /// Assembly the resource belongs to.
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// Resource name.
        /// </summary>
        public string ResourceName { get; }

        /// <summary>
        /// File name.
        /// </summary>
        public string FileName { get; }

        private AssemblyEmbeddedResource(Assembly assembly, string resourceName, string fileName)
        {
            Assembly = assembly;
            ResourceName = resourceName;
            FileName = fileName;
        }

        /// <summary>
        /// Returns a new instance of <see cref="T:ByteDev.Reflection.AssemblyEmbeddedResource" /> from the assembly containing type
        /// <typeparamref name="T" /> for <paramref name="fileName" />.
        /// </summary>
        /// <typeparam name="T">Containing type.</typeparam>
        /// <param name="fileName">File name to search for.</param>
        /// <returns>New instance of <see cref="T:ByteDev.Reflection.AssemblyEmbeddedResource" />.</returns>
        public static AssemblyEmbeddedResource CreateFromAssemblyContaining<T>(string fileName)
        {
            return CreateFromAssembly(typeof(T).Assembly, fileName);
        }

        /// <summary>
        /// Returns a new instance of <see cref="T:ByteDev.Reflection.AssemblyEmbeddedResource" /> from the assembly containing type
        /// <paramref name="type" /> for <paramref name="fileName" />.
        /// </summary>
        /// <param name="type">Containing type.</param>
        /// <param name="fileName">File name to search for.</param>
        /// <returns>New instance of <see cref="T:ByteDev.Reflection.AssemblyEmbeddedResource" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="type" /> is null.</exception>
        public static AssemblyEmbeddedResource CreateFromAssemblyContaining(Type type, string fileName)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return CreateFromAssembly(type.Assembly, fileName);
        }

        /// <summary>
        /// Returns a new instance of <see cref="T:ByteDev.Reflection.AssemblyEmbeddedResource" /> from <paramref name="assembly" /> for <paramref name="fileName" />.
        /// </summary>
        /// <param name="assembly">Assembly to retrieve the embedded resource from.</param>
        /// <param name="fileName">The file name of the embedded resource.</param>
        /// <returns>New instance of <see cref="T:ByteDev.Reflection.AssemblyEmbeddedResource" /></returns>
        public static AssemblyEmbeddedResource CreateFromAssembly(Assembly assembly, string fileName)
        {
            var resourceName = assembly.GetManifestResourceName(fileName);

            return new AssemblyEmbeddedResource(assembly, resourceName, fileName);
        }

        /// <summary>
        /// Saves the current instance to disk.
        /// </summary>
        /// <param name="filePath">File path of where to save the file to disk.</param>
        /// <returns>FileInfo of the file saved to disk.</returns>
        public FileInfo Save(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
            {
                using (var stream = Assembly.GetManifestResourceStream(ResourceName))
                {
                    if (stream == null)
                    {
                        return new FileInfo(filePath);
                    }

                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }
            }

            return new FileInfo(filePath);
        }
    }
}