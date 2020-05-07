using System;
using NUnit.Framework;

namespace ByteDev.Reflection.UnitTests
{
    [TestFixture]
    public class TypeIsExtensionsTests
    {
        [TestFixture]
        public class IsTestClass
        {
            [Test]
            public void WhenTypeIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => TypeIsExtensions.IsTestClass(null));
            }

            [Test]
            public void WhenTypeIsTestClass_ThenReturnTrue()
            {
                var sut = typeof(TypeIsExtensionsTests);

                var result = sut.IsTestClass();

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenTypeIsNotTestClass_ThenReturnFalse()
            {
                var sut = typeof(Person);

                var result = sut.IsTestClass();

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenTypeIsNotClass_ThenReturnFalse()
            {
                var sut = typeof(PlaceTests);

                var result = sut.IsTestClass();

                Assert.That(result, Is.False);
            }

            internal class Person
            {
            }

            internal struct PlaceTests
            {
            }
        }

        [TestFixture]
        public class IsInNamespace
        {
            private Type _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = typeof(TypeExtensionsTests);
            }

            [Test]
            public void WhenTypeIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => TypeIsExtensions.IsInNamespace(null, "Some.Where"));
            }

            [Test]
            public void WhenNamespaceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.IsInNamespace(null));
            }

            [Test]
            public void WhenNamespaceIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.IsInNamespace(string.Empty));
            }

            [Test]
            public void WhenTypeIsInExactNamespace_ThenReturnTrue()
            {
                var result = _sut.IsInNamespace("ByteDev.Reflection.UnitTests");

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenTypeIsInDiffCaseNamespace_ThenReturnFalse()
            {
                var result = _sut.IsInNamespace("ByteDev.Reflection.UNITTESTS");

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenNamespaceIsLessSpecific_ThenReturnTrue()
            {
                var result = _sut.IsInNamespace("ByteDev.Reflection");

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenNamespaceIsMoreSpecific_ThenReturnFalse()
            {
                var result = _sut.IsInNamespace("ByteDev.Reflection.UnitTests.TestTypes");

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenTypeHasNoNamespace_ThenReturnFalse()
            {
                var sut = typeof(NoNamespaceType);

                var result = sut.IsInNamespace("ByteDev.Reflection.UnitTests");

                Assert.That(result, Is.False);
            }
        }
    }
}

internal class NoNamespaceType
{
}