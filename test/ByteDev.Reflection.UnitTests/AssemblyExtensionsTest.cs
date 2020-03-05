using System;
using System.Reflection;
using NUnit.Framework;

namespace ByteDev.Reflection.UnitTests
{
    [TestFixture]
    public class AssemblyExtensionsTest
    {
        private Assembly _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = Assembly.GetAssembly(typeof(AssemblyExtensionsTest));
        }

        [TestFixture]
        public class GetSubClasses : AssemblyExtensionsTest
        {
            [Test]
            public void WhenIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => (null as Assembly).GetSubClasses<Car>());
            }

            [Test]
            public void WhenTypeHasSubClasses_ThenReturnsSubClasses()
            {
                var result = _sut.GetSubClasses<Car>();

                Assert.That(result.Count, Is.EqualTo(2));
            }

            [Test]
            public void WhenTypeHasNoSubClasses_ThenReturnsEmpty()
            {
                var result = _sut.GetSubClasses<Ford>();

                Assert.That(result.Count, Is.EqualTo(0));
            }
        }

        [TestFixture]
        public class GetAssemblyAttribute : AssemblyExtensionsTest
        {
            [Test]
            public void WhenIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => (null as Assembly).GetAssemblyAttribute<AssemblyProductAttribute>());
            }

            [Test]
            public void WhenAssemblyAttributeExists_ThenReturnCorrectAttribute()
            {
                var result = _sut.GetAssemblyAttribute<AssemblyProductAttribute>();

                Assert.That(result.Product, Is.EqualTo("ByteDev.Reflection.UnitTests"));
            }
        }

        [TestFixture]
        public class GetVersion : AssemblyExtensionsTest
        {
            [Test]
            public void WhenIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => (null as Assembly).GetVersion());
            }

            [Test]
            public void WhenAssemblyHasVersion_ThenReturnVersion()
            {
                var result = _sut.GetVersion();

                Assert.That(result.ToString(), Is.EqualTo("1.0.0.0"));
            }
        }

        [TestFixture]
        public class GetFileVersion : AssemblyExtensionsTest
        {
            [Test]
            public void WhenIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => (null as Assembly).GetFileVersion());
            }

            [Test]
            public void WhenAssemblyHasVersion_ThenReturnVersion()
            {
                var result = _sut.GetFileVersion();

                Assert.That(result.ToString(), Is.EqualTo("1.0.0.0"));
            }
        }

        public class Car
        {
        }

        public class Ford : Car
        {
        }

        public class Fiat : Car
        {
        }
    }
}