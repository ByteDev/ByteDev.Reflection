using System;
using System.Linq;
using System.Reflection;
using ByteDev.Reflection.UnitTests.TestTypes;
using NUnit.Framework;

namespace ByteDev.Reflection.UnitTests
{
    [TestFixture]
    public class MemberInfoExtensionsTests
    {
        [TestFixture]
        public class HasAttribute
        {
            [Test]
            public void WhenTypeIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.HasAttribute<UsedMethodAttribute>(null));
            }

            [Test]
            public void WhenMethodHasAttribute_ThenReturnTrue()
            {
                var sut = typeof(DummyWithMethods).GetMethod("MethodWithAttribute");

                var result = sut.HasAttribute<UsedMethodAttribute>();

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenMethodDoesNotHaveAttribute_ThenReturnFalse()
            {
                var sut = typeof(DummyWithMethods).GetMethod("MethodWithNoAttribute");

                var result = sut.HasAttribute<UsedMethodAttribute>();

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenTypeHasAttribute_ThenReturnTrue()
            {
                var sut = typeof(DummyWithClassAttribute);

                var result = sut.HasAttribute<UsedClassAttribute>();

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenTypeDoesNotHaveAttribute_ThenReturnFalse()
            {
                var sut = typeof(DummyWithClassAttribute);

                var result = sut.HasAttribute<NotUsedAttribute>();

                Assert.That(result, Is.False);
            }
        }

        [TestFixture]
        public class GetAttribute
        {
            [Test]
            public void WhenIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.GetAttribute<UsedMethodAttribute>(null));
            }

            [Test]
            public void WhenMethodHasAttribute_ThenReturnAttribute()
            {
                MemberInfo sut = typeof(DummyWithMethods).GetMember("MethodWithAttribute").Single();

                var result = sut.GetAttribute<UsedMethodAttribute>();

                Assert.That(result.Name, Is.EqualTo("John Smith"));
            }

            [Test]
            public void WhenMethodDoesNotHaveAttribute_ThenReturnNull()
            {
                MemberInfo sut = typeof(DummyWithMethods).GetMember("MethodWithNoAttribute").Single();

                var result = sut.GetAttribute<UsedMethodAttribute>();

                Assert.That(result, Is.Null);
            }

            [Test]
            public void WhenMemberHasMultipleOfAttribute_ThenThrowException()
            {
                MemberInfo sut = typeof(DummyWithMethods).GetMember("MethodWithMultiAttribute").Single();

                Assert.Throws<InvalidOperationException>(() => sut.GetAttribute<UsedMethodAttribute>());
            }
        }
    }
}