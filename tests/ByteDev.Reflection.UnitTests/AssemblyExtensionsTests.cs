using System;
using System.Linq;
using System.Reflection;
using ByteDev.Collections;
using NUnit.Framework;

namespace ByteDev.Reflection.UnitTests
{
    [TestFixture]
    public class AssemblyExtensionsTests
    {
        private Assembly _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = Assembly.GetAssembly(typeof(AssemblyExtensionsTests));
        }

        [TestFixture]
        public class GetVersion : AssemblyExtensionsTests
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
        public class GetFileVersion : AssemblyExtensionsTests
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

        [TestFixture]
        public class GetSubClasses : AssemblyExtensionsTests
        {
            [Test]
            public void WhenIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => (null as Assembly).GetSubClasses<Car>());
            }

            [Test]
            public void WhenTypeHasSubClasses_ThenReturnsSubClasses()
            {
                var result = _sut.GetSubClasses<Car>().ToList();

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result.First().Name, Is.EqualTo(typeof(Ford).Name));
                Assert.That(result.Second().Name, Is.EqualTo(typeof(Fiat).Name));
            }

            [Test]
            public void WhenTypeHasNoSubClasses_ThenReturnsEmpty()
            {
                var result = _sut.GetSubClasses<Ford>();

                Assert.That(result, Is.Empty);
            }
        }

        [TestFixture]
        public class GetAssemblyAttribute : AssemblyExtensionsTests
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