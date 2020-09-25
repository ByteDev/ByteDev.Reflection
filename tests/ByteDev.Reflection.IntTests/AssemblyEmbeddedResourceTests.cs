using System.IO;
using ByteDev.Reflection.IntTests.TestFiles;
using ByteDev.Testing.Builders;
using ByteDev.Testing.NUnit;
using NUnit.Framework;

namespace ByteDev.Reflection.IntTests
{
    [TestFixture]
    public class AssemblyEmbeddedResourceTests : IoTestBase
    {
        [TestFixture]
        public class CreateFromAssemblyContaining : AssemblyEmbeddedResourceTests
        {
            [Test]
            public void WhenEmbeddedFileExists_ThenSetProperties()
            {
                AssemblyEmbeddedResource sut = AssemblyEmbeddedResource.CreateFromAssemblyContaining<AssemblyEmbeddedResourceTests>(TestFileNames.ExistingEmbeddedFile);

                Assert.That(sut.Assembly, Is.EqualTo(typeof(AssemblyEmbeddedResourceTests).Assembly));
                Assert.That(sut.FileName, Is.EqualTo(TestFileNames.ExistingEmbeddedFile));
                Assert.That(sut.ResourceName, Is.EqualTo($"ByteDev.Reflection.IntTests.TestFiles.{TestFileNames.ExistingEmbeddedFile}"));
            }

            [Test]
            public void WhenEmbeddedFileDoesNotExist_ThenThrowException()
            {
                Assert.Throws<FileNotFoundException>(() => AssemblyEmbeddedResource.CreateFromAssemblyContaining<AssemblyEmbeddedResourceTests>(TestFileNames.NotExistingEmbeddedFile));
            }

            [Test]
            public void WhenAssemblyHasNoEmbeddedFiles_ThenThrowException()
            {
                Assert.Throws<FileNotFoundException>(() => AssemblyEmbeddedResource.CreateFromAssemblyContaining<AssemblyEmbeddedResource>(TestFileNames.ExistingEmbeddedFile));
            }

            [Test]
            public void WhenFileIsContentFile_ThenThrowException()
            {
                Assert.Throws<FileNotFoundException>(() => AssemblyEmbeddedResource.CreateFromAssemblyContaining<AssemblyEmbeddedResource>(TestFileNames.ExistingContentFile));
            }
        }

        [TestFixture]
        public class Save : AssemblyEmbeddedResourceTests
        {
            [Test]
            public void WhenNoFileExistsOnDisk_ThenSaveToDisk()
            {
                var sut = AssemblyEmbeddedResource.CreateFromAssemblyContaining<AssemblyEmbeddedResourceTests>(TestFileNames.ExistingEmbeddedFile);

                var fileInfo = sut.Save(GetWorkingPath(sut.FileName));

                AssertFile.ContentEquals(fileInfo, "Embedded Resource 1");
            }

            [Test]
            public void WhenFileAlreadyExists_ThenThrowException()
            {
                var saveFilePath = GetWorkingPath(TestFileNames.ExistingEmbeddedFile);

                FileBuilder.InFileSystem.WithPath(saveFilePath).Build();

                var sut = AssemblyEmbeddedResource.CreateFromAssemblyContaining<AssemblyEmbeddedResourceTests>(TestFileNames.ExistingEmbeddedFile);

                Assert.Throws<IOException>(() => sut.Save(saveFilePath));
            }
        }
    }
}