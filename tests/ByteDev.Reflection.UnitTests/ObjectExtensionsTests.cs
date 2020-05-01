using System;
using NUnit.Framework;

namespace ByteDev.Reflection.UnitTests
{
    [TestFixture]
    public class ObjectExtensionsTests
    {
        [TestFixture]
        public class HasAttribute : ReflectionAttributeExtensionsTests
        {
            [Test]
            public void WhenTypeIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => (null as object).HasAttribute<UsedAttribute>());
            }

            [Test]
            public void WhenObjectHasAttribute_ThenReturnTrue()
            {
                object sut = new DummyWithClassAttribute();

                var result = sut.HasAttribute<UsedAttribute>();

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenObjectHasNoAttribute_ThenReturnFalse()
            {
                object sut = new DummyWithMethods();

                var result = sut.HasAttribute<UsedAttribute>();

                Assert.That(result, Is.False);
            }
        }
    }
}