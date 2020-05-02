using System;
using System.Linq;
using ByteDev.Collections;
using NUnit.Framework;

namespace ByteDev.Reflection.UnitTests
{
    [TestFixture]
    public class TypeExtensionsTests
    {
        [TestFixture]
        public class GetPropertyOrThrow
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetPropertyOrThrow(null, "PublicReadOnly"));
            }

            [Test]
            public void WhenPropertyNameIsNull_ThenThrowException()
            {
                var sut = typeof(Car);

                Assert.Throws<ArgumentException>(() => sut.GetPropertyOrThrow(null));
            }

            [Test]
            public void WhenPropertyNameIsEmpty_ThenThrowException()
            {
                var sut = typeof(Car);

                Assert.Throws<ArgumentException>(() => sut.GetPropertyOrThrow(string.Empty));
            }

            [Test]
            public void WhenPropertyDoesNotExist_ThenThrowException()
            {
                var sut = typeof(Car);

                Assert.Throws<InvalidOperationException>(() => sut.GetPropertyOrThrow("DoesNotExist"));
            }

            [Test]
            public void WhenPublicPropertyExists_ThenReturnProperty()
            {
                const string propertyName = "PublicReadOnly";

                var sut = typeof(Car);

                var result = sut.GetPropertyOrThrow(propertyName);

                Assert.That(result.Name, Is.EqualTo(propertyName));
            }

            [Test]
            public void WhenPublicPropertyExists_AndIgnoreCase_ThenReturnProperty()
            {
                var sut = typeof(Car);

                var result = sut.GetPropertyOrThrow("publicreadonly", true);

                Assert.That(result.Name, Is.EqualTo("PublicReadOnly"));
            }

            [Test]
            public void WhenPrivatePropertyExists_ThenReturnProperty()
            {
                const string propertyName = "PrivateReadOnly";

                var sut = typeof(Car);

                var result = sut.GetPropertyOrThrow(propertyName);

                Assert.That(result.Name, Is.EqualTo(propertyName));
            }

            [Test]
            public void WhenPrivatePropertyExists_AndIgnoreCase_ThenReturnProperty()
            {
                var sut = typeof(Car);

                var result = sut.GetPropertyOrThrow("privatereadonly", true);

                Assert.That(result.Name, Is.EqualTo("PrivateReadOnly"));
            }
        }







        [TestFixture]
        public class GetConstants
        {
            [Test]
            public void WhenTypeHasNoConsts_ThenReturnNoFields()
            {
                var result = typeof(DummyWithNoConsts).GetConstants();

                Assert.That(result.Count(), Is.EqualTo(0));
            }

            [Test]
            public void WhenTypeHasConsts_ThenReturnPublicConstFields()
            {
                var result = typeof(DummyWithConsts).GetConstants();

                Assert.That(result.Count(), Is.EqualTo(2));
            }
        }

        [TestFixture]
        public class GetConstantsValues
        {
            [Test]
            public void WhenTypeHasNoConsts_ThenReturnNoFields()
            {
                var result = typeof(DummyWithNoConsts).GetConstantsValues<string>();

                Assert.That(result.Count(), Is.EqualTo(0));
            }

            [Test]
            public void WhenTypeHasConsts_ThenReturnPublicConstValues()
            {
                var result = typeof(DummyWithConsts).GetConstantsValues<string>().ToList();

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result.First(), Is.EqualTo("value1"));
                Assert.That(result.Second(), Is.EqualTo("value2"));
            }
        }


        [TestFixture]
        public class HasAttribute
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
        public class GetStaticPropertyValue
        {
            [Test]
            public void WhenClassHasStaticProperty_ThenReturnValue()
            {
                var sut = typeof(ClassWithStaticProperty);

                ClassWithStaticProperty.Name = "John";

                var result = sut.GetStaticPropertyValue<string>("Name");

                Assert.That(result, Is.EqualTo(ClassWithStaticProperty.Name));
            }

            [Test]
            public void WhenClassHasNoStaticProperty_ThenThrowException()
            {
                var sut = typeof(ClassWithStaticProperty);

                Assert.Throws<InvalidOperationException>(() => sut.GetStaticPropertyValue<string>("Surname"));
            }
        }
    }
}