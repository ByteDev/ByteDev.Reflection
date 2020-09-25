using System;
using System.IO;
using System.Reflection;
using ByteDev.Io;
using NUnit.Framework;

namespace ByteDev.Reflection.IntTests
{
    /// <summary>
    /// Sets up and destroys a working directory at the start and end
    /// of an IO related test.
    /// </summary>
    public abstract class IoTestBase
    {
        private const string BasePath = @"C:\Temp";

        protected DirectoryInfo WorkingDir { get; }

        protected IoTestBase()
        {
            var name = Assembly.GetExecutingAssembly().GetName().Name;      // e.g. ByteDev.Reflection.IntTests

            if (string.IsNullOrEmpty(name))
                name = CreateRandomDirName();

            WorkingDir = new DirectoryInfo(Path.Combine(BasePath, name));
        }

        [SetUp]
        public void SetUp()
        {
            WorkingDir.CreateDirectory();
        }

        [TearDown]
        public void TearDown()
        {
            WorkingDir.DeleteIfExists();
        }

        protected string GetWorkingPath(string file)
        {
            return Path.Combine(WorkingDir.FullName, file);
        }

        private static string CreateRandomDirName()
        {
            return "TestWorkingDir-" + Guid.NewGuid().ToString().Replace("-", string.Empty);
        }
    }
}