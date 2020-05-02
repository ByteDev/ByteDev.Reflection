using System;
using NUnit.Framework;

namespace ByteDev.Reflection.UnitTests
{
    [TestFixture]
    public class ObjectExtensionsTests
    {
        [TestFixture]
        public class HasAttribute
        {
            [Test]
            public void WhenTypeIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => ObjectExtensions.HasAttribute<UsedAttribute>(null));
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

        [TestFixture]
        public class SetProperty
        {
            private const string Value = "Something";

            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => ObjectExtensions.SetProperty(null, "PrivateWritable", Value));
            }

            [Test]
            public void WhenPropertyNameIsNull_ThenThrowException()
            {
                var sut = new Car();

                Assert.Throws<ArgumentException>(() => sut.SetProperty(null, Value));
            }

            [Test]
            public void WhenPropertyNameIsEmpty_ThenThrowException()
            {
                var sut = new Car();

                Assert.Throws<ArgumentException>(() => sut.SetProperty(string.Empty, Value));
            }

            [Test]
            public void WhenPropertyIsNotWritable_ThenThrowException()
            {
                var sut = new Car();

                var ex = Assert.Throws<InvalidOperationException>(() => sut.SetProperty("PublicReadOnly", Value));
                Assert.That(ex.Message, Is.EqualTo("Type: 'Car' property: 'PublicReadOnly' cannot be written to."));
            }

            [Test]
            public void WhenPropertyIsPublicWritable_ThenSetProperty()
            {
                var sut = new Car();

                sut.SetProperty("PublicWritable", Value);

                Assert.That(sut.PublicWritable, Is.EqualTo(Value));
            }

            [Test]
            public void WhenPropertyIsPublicWritable_AndIgnoreCase_ThenSetProperty()
            {
                var sut = new Car();

                sut.SetProperty("publicwritable", Value, true);

                Assert.That(sut.PublicWritable, Is.EqualTo(Value));
            }

            [Test]
            public void WhenPropertyIsPrivateWritable_ThenSetProperty()
            {
                var sut = new Car();

                sut.SetProperty("PrivateWritable", Value);

                Assert.That(sut.PrivateWritable, Is.EqualTo(Value));
            }

            [Test]
            public void WhenPropertyIsPrivateWritable_AndIgnoreCase_ThenSetProperty()
            {
                var sut = new Car();

                sut.SetProperty("privatewritable", Value, true);

                Assert.That(sut.PrivateWritable, Is.EqualTo(Value));
            }
        }

        [TestFixture]
        public class GetPropertyValue
        {
            [Test]
            public void WhenClassHasProperty_ThenReturnValue()
            {
                var sut = new ClassWithProperty { Name = "John" };

                var result = sut.GetPropertyValue<string>("Name");

                Assert.That(result, Is.EqualTo(sut.Name));
            }

            [Test]
            public void WhenClassHasComplexProperty_ThenReturnValue()
            {
                const int expected = 50;

                var sut = new ClassWithProperty { AnotherClassWithProperty = new AnotherClassWithProperty { Age = expected } };

                var result = sut.GetPropertyValue<int>("AnotherClassWithProperty.Age");

                Assert.That(result, Is.EqualTo(expected));
            }

            [Test]
            public void WhenClassHasPropertyOfDifferentType_ThenThrowException()
            {
                var sut = new ClassWithProperty { Name = "John" };

                Assert.Throws<InvalidCastException>(() => sut.GetPropertyValue<int>("Name"));
            }

            [Test]
            public void WhenClassHasNoProperty_ThenThrowException()
            {
                var sut = typeof(ClassWithProperty);

                Assert.Throws<InvalidOperationException>(() => sut.GetStaticPropertyValue<string>("Surname"));
            }
        }
    }
}