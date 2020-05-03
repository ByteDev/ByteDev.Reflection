using System;
using ByteDev.Reflection.UnitTests.TestTypes;
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
                Assert.Throws<ArgumentNullException>(() => ObjectExtensions.HasAttribute<UsedMethodAttribute>(null));
            }

            [Test]
            public void WhenObjectHasAttribute_ThenReturnTrue()
            {
                object sut = new DummyWithClassAttribute();

                var result = sut.HasAttribute<UsedClassAttribute>();

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenObjectHasNoAttribute_ThenReturnFalse()
            {
                object sut = new DummyWithMethods();

                var result = sut.HasAttribute<UsedMethodAttribute>();

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
                var sut = new DummyWithProperties();

                Assert.Throws<ArgumentException>(() => sut.SetProperty(null, Value));
            }

            [Test]
            public void WhenPropertyNameIsEmpty_ThenThrowException()
            {
                var sut = new DummyWithProperties();

                Assert.Throws<ArgumentException>(() => sut.SetProperty(string.Empty, Value));
            }

            [Test]
            public void WhenPropertyIsNotWritable_ThenThrowException()
            {
                var sut = new DummyWithProperties();

                var ex = Assert.Throws<InvalidOperationException>(() => sut.SetProperty("PublicReadOnly", Value));
                Assert.That(ex.Message, Is.EqualTo("Type: 'DummyWithProperties' property: 'PublicReadOnly' cannot be written to."));
            }

            [Test]
            public void WhenPropertyIsPublicWritable_ThenSetProperty()
            {
                var sut = new DummyWithProperties();

                sut.SetProperty("PublicWritable", Value);

                Assert.That(sut.PublicWritable, Is.EqualTo(Value));
            }

            [Test]
            public void WhenPropertyIsPublicWritable_AndIgnoreCase_ThenSetProperty()
            {
                var sut = new DummyWithProperties();

                sut.SetProperty("publicwritable", Value, true);

                Assert.That(sut.PublicWritable, Is.EqualTo(Value));
            }

            [Test]
            public void WhenPropertyIsPrivateWritable_ThenSetProperty()
            {
                var sut = new DummyWithProperties();

                sut.SetProperty("PrivateWritable", Value);

                var actual = sut.GetPropertyValue<string>("PrivateWritable");

                Assert.That(actual, Is.EqualTo(Value));
            }

            [Test]
            public void WhenPropertyIsPrivateWritable_AndIgnoreCase_ThenSetProperty()
            {
                var sut = new DummyWithProperties();

                sut.SetProperty("privatewritable", Value, true);

                var actual = sut.GetPropertyValue<string>("privatewritable", true);

                Assert.That(actual, Is.EqualTo(Value));
            }
        }

        [TestFixture]
        public class GetPropertyValue_Object
        {
            private DummyWithProperties _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = new DummyWithProperties();
            }

            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => ObjectExtensions.GetPropertyValue(null, "PrivateReadOnly"));
            }

            [Test]
            public void WhenPropertyNameIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.GetPropertyValue(null));
            }

            [Test]
            public void WhenPropertyNameIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.GetPropertyValue(string.Empty));
            }

            [Test]
            public void WhenPropertyDoesNotExist_ThenThrowException()
            {
                Assert.Throws<InvalidOperationException>(() => _sut.GetPropertyValue("DoesNotExist"));
            }

            [TestCase("PrivateReadOnly", "Private read default value")]
            [TestCase("ProtectedReadOnly", "Protected read default value")]
            [TestCase("InternalReadOnly", "Internal read default value")]
            [TestCase("PublicReadOnly", "Public read default value")]
            public void WhenPropertyExists_ThenReturnValue(string propertyName, string expectedValue)
            {
                var result = _sut.GetPropertyValue(propertyName);

                Assert.That(result, Is.EqualTo(expectedValue));
            }

            [Test]
            public void WhenPropertyIsComplexType_ThenReturnValue()
            {
                var person = new DummyWithProperty { Name = "John" };

                _sut.DummyWithProperty = person;

                var result = _sut.GetPropertyValue<string>("DummyWithProperty.Name");

                Assert.That(result, Is.EqualTo(person.Name));
            }
        }

        [TestFixture]
        public class GetPropertyValue_Type
        {
            private DummyWithProperties _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = new DummyWithProperties();
            }

            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => ObjectExtensions.GetPropertyValue<string>(null, "PrivateReadOnly"));
            }

            [Test]
            public void WhenPropertyNameIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.GetPropertyValue<string>(null));
            }

            [Test]
            public void WhenPropertyNameIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.GetPropertyValue<string>(string.Empty));
            }

            [Test]
            public void WhenPropertyDoesNotExist_ThenThrowException()
            {
                Assert.Throws<InvalidOperationException>(() => _sut.GetPropertyValue<string>("DoesNotExist"));
            }
            
            [Test]
            public void WhenPropertyOfDifferentType_ThenThrowException()
            {
                Assert.Throws<InvalidCastException>(() => _sut.GetPropertyValue<int>("PrivateReadOnly"));
            }
            
            [TestCase("PrivateReadOnly", "Private read default value")]
            [TestCase("ProtectedReadOnly", "Protected read default value")]
            [TestCase("InternalReadOnly", "Internal read default value")]
            [TestCase("PublicReadOnly", "Public read default value")]
            public void WhenPropertyExists_ThenReturnValueAsType(string propertyName, string expectedValue)
            {
                var result = _sut.GetPropertyValue<string>(propertyName);

                Assert.That(result, Is.EqualTo(expectedValue));
            }

            [Test]
            public void WhenPropertyIsComplexType_ThenReturnValue()
            {
                var person = new DummyWithProperty { Name = "John" };

                _sut.DummyWithProperty = person;

                var result = _sut.GetPropertyValue("DummyWithProperty.Name");

                Assert.That(result, Is.EqualTo(person.Name));
            }
        }
    }
}