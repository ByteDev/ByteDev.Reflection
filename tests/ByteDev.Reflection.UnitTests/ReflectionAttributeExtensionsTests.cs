using System;
using System.Reflection;
using NUnit.Framework;

namespace ByteDev.Reflection.UnitTests
{
    [TestFixture]
    public class ReflectionAttributeExtensionsTests
    {
        [TestFixture]
        public class HasAttribute_Type : ReflectionAttributeExtensionsTests
        {
            [Test]
            public void WhenTypeIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => (null as Type).HasAttribute<UsedAttribute>());
            }

            [Test]
            public void WhenTypeHasAttribute_ThenReturnTrue()
            {
                Type sut = typeof(DummyWithClassAttribute);

                var result = sut.HasAttribute<UsedAttribute>();

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenTypeDoesNotHaveAttribute_ThenReturnFalse()
            {
                Type sut = typeof(DummyWithClassAttribute);

                var result = sut.HasAttribute<NotUsedAttribute>();

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenTypesMethodsHasAttribute_ThenReturnFalse()
            {
                Type sut = typeof(DummyWithMethods);

                var result = sut.HasAttribute<UsedAttribute>();

                Assert.That(result, Is.False);
            }
        }

        [TestFixture]
        public class HasAttribute_MemberInfo : ReflectionAttributeExtensionsTests
        {
            [Test]
            public void WhenTypeIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => (null as MemberInfo).HasAttribute<UsedAttribute>());
            }

            [Test]
            public void WhenMethodHasAttribute_ThenReturnTrue()
            {
                MethodInfo sut = typeof(DummyWithMethods).GetMethod("MethodWithAttribute");

                var result = sut.HasAttribute<UsedAttribute>();

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenMethodDoesNotHaveAttribute_ThenReturnFalse()
            {
                MethodInfo sut = typeof(DummyWithMethods).GetMethod("MethodWithNoAttribute");

                var result = sut.HasAttribute<UsedAttribute>();

                Assert.That(result, Is.False);
            }
        }

        public class UsedAttribute : Attribute
        {
        }

        public class NotUsedAttribute : Attribute
        {
        }

        public class DummyWithMethods
        {
            [Used]
            public void MethodWithAttribute()
            {
            }

            public void MethodWithNoAttribute()
            {
            }
        }

        [Used]
        public class DummyWithClassAttribute
        {
        }
    }
}
