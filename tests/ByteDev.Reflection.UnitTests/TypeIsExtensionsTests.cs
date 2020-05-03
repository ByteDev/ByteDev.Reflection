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
        }

        internal class Person
        {
        }

        internal struct PlaceTests
        {
        }
    }
}