using System;
using System.Reflection;
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
                var person = new DummyWithProperty { PublicString = "John" };

                _sut.DummyWithProperty = person;

                var result = _sut.GetPropertyValue<string>("DummyWithProperty.PublicString");

                Assert.That(result, Is.EqualTo(person.PublicString));
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
                var person = new DummyWithProperty { PublicString = "John" };

                _sut.DummyWithProperty = person;

                var result = _sut.GetPropertyValue("DummyWithProperty.PublicString");

                Assert.That(result, Is.EqualTo(person.PublicString));
            }
        }

        [TestFixture]
        public class SetProperty
        {
            private const string Value = "Something";

            private DummyWithProperties _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = new DummyWithProperties();
            }

            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => ObjectExtensions.SetProperty(null, "PrivateWritable", Value));
            }

            [Test]
            public void WhenPropertyNameIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.SetProperty(null, Value));
            }

            [Test]
            public void WhenPropertyNameIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.SetProperty(string.Empty, Value));
            }

            [Test]
            public void WhenPropertyDoesNotExist_ThenThrowException()
            {
                var ex = Assert.Throws<InvalidOperationException>(() => _sut.SetProperty("DoesntExist", Value));

                Assert.That(ex.Message, Is.EqualTo("Type 'DummyWithProperties' has no property called 'DoesntExist'."));
            }

            [Test]
            public void WhenPropertyIsNotWritable_ThenThrowException()
            {
                var ex = Assert.Throws<InvalidOperationException>(() => _sut.SetProperty("PublicReadOnly", Value));

                Assert.That(ex.Message, Is.EqualTo("Type: 'DummyWithProperties' property: 'PublicReadOnly' cannot be written to."));
            }

            [TestCase("PublicWritable")]
            [TestCase("PrivateWritable")]
            public void WhenPropertyIsWritable_ThenSetProperty(string propertyName)
            {
                _sut.SetProperty(propertyName, Value);

                var actual = _sut.GetPropertyValue<string>(propertyName);

                Assert.That(actual, Is.EqualTo(Value));
            }

            [TestCase("PublicWritable")]
            [TestCase("PrivateWritable")]
            public void WhenPropertyIsWritable_AndIgnoreCase_ThenSetProperty(string propertyName)
            {
                _sut.SetProperty(propertyName.ToLower(), Value, true);

                var actual = _sut.GetPropertyValue<string>(propertyName);

                Assert.That(actual, Is.EqualTo(Value));
            }

            [Test]
            public void WhenPropertyIsInt_ThenSetProperty()
            {
                _sut.SetProperty("PublicWritableInt", 1);

                Assert.That(_sut.PublicWritableInt, Is.EqualTo(1));
            }
        }

        [TestFixture]
        public class SetReadOnlyProperty
        {
            private const string Value = "Something";

            private DummyWithProperties _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = new DummyWithProperties();
            }

            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => ObjectExtensions.SetReadOnlyProperty(null, "PrivateReadOnly", Value));
            }

            [Test]
            public void WhenPropertyNameIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.SetReadOnlyProperty(null, Value));
            }

            [Test]
            public void WhenPropertyNameIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.SetReadOnlyProperty(string.Empty, Value));
            }

            [Test]
            public void WhenPropertyDoesNotExist_ThenThrowException()
            {
                var ex = Assert.Throws<InvalidOperationException>(() => _sut.SetReadOnlyProperty("DoesntExist", Value));

                Assert.That(ex.Message, Is.EqualTo("Type 'DummyWithProperties' has no property called 'DoesntExist'."));
            }

            [Test]
            public void WhenPropertyIsReadOnly_ThenSetProperty()
            {
                _sut.SetReadOnlyProperty("PrivateReadOnly", Value);

                var actual = _sut.GetPropertyValue<string>("PrivateReadOnly");

                Assert.That(actual, Is.EqualTo(Value));
            }
        }

        [TestFixture]
        public class GetPropertiesAsDictionary
        {
            [Test]
            public void WhenSourceIsNull_ThenReturnEmpty()
            {
                var result = (null as TestPerson).GetPropertiesAsDictionary(BindingFlags.Instance | BindingFlags.Public);

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenSourcePropertiesSet_ThenReturnDictionary()
            {
                var sut = new TestPerson {Name = "John", Age = 50};

                var result = sut.GetPropertiesAsDictionary(BindingFlags.Instance | BindingFlags.Public);

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result["Name"], Is.EqualTo("John"));
                Assert.That(result["Age"], Is.EqualTo(50));
            }
        }
    }
}