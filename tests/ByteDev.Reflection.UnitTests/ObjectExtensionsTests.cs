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

            [Test]
            public void WhenSourcePropertiesNotSet_ThenSetDefault()
            {
                var sut = new TestPerson();

                var result = sut.GetPropertiesAsDictionary(BindingFlags.Instance | BindingFlags.Public);

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result["Name"], Is.EqualTo(null));
                Assert.That(result["Age"], Is.EqualTo(0));
            }
        }

        [TestFixture]
        public class SetPropertyReadOnlyValue
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
                Assert.Throws<ArgumentNullException>(() => ObjectExtensions.SetPropertyReadOnlyValue(null, "PrivateReadOnly", Value));
            }

            [Test]
            public void WhenPropertyNameIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.SetPropertyReadOnlyValue(null, Value));
            }

            [Test]
            public void WhenPropertyNameIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.SetPropertyReadOnlyValue(string.Empty, Value));
            }

            [Test]
            public void WhenPropertyDoesNotExist_ThenThrowException()
            {
                var ex = Assert.Throws<InvalidOperationException>(() => _sut.SetPropertyReadOnlyValue("DoesntExist", Value));

                Assert.That(ex.Message, Is.EqualTo("Type 'DummyWithProperties' has no property called 'DoesntExist'."));
            }

            [Test]
            public void WhenPropertyIsReadOnly_ThenSetProperty()
            {
                _sut.SetPropertyReadOnlyValue("PrivateReadOnly", Value);

                var actual = _sut.GetPropertyValue<string>("PrivateReadOnly");

                Assert.That(actual, Is.EqualTo(Value));
            }
        }

        [TestFixture]
        public class SetPropertyValue
        {
            private TestClassAllTypes _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = new TestClassAllTypes();
            }

            [Test]
            public void WhenSourceIsNull_AndPropertyName_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => ObjectExtensions.SetPropertyValue(null, "String", "John"));
            }

            [Test]
            public void WhenSourceIsNull_AndPropertyInfo_ThenThrowException()
            {
                var pi = typeof(TestClassAllTypes).GetProperty("String");

                Assert.Throws<ArgumentNullException>(() => ObjectExtensions.SetPropertyValue(null, pi, "John"));
            }

            [Test]
            public void WhenSourceIsNotNull_AndPropertyInfoIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.SetPropertyValue(null as PropertyInfo, "John"));
            }

            [Test]
            public void WhenSourceIsNotNull_AndPropertyNameIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.SetPropertyValue(null as string, "John"));
            }

            [Test]
            public void WhenSourceIsNotNull_AndPropertyNameIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.SetPropertyValue(string.Empty, "John"));
            }

            [Test]
            public void WhenPropertyDoesNotExist_ThenThrowException()
            {
                var ex = Assert.Throws<InvalidOperationException>(() => _sut.SetPropertyValue("DoesntExist", "John"));

                Assert.That(ex.Message, Is.EqualTo("Type 'TestClassAllTypes' has no property called 'DoesntExist'."));
            }

            [Test]
            public void WhenPropertyIsNotWritable_ThenThrowException()
            {
                var ex = Assert.Throws<InvalidOperationException>(() => _sut.SetPropertyValue(nameof(TestClassAllTypes.ReadOnlyString), "John"));

                Assert.That(ex.Message, Is.EqualTo("Type: 'TestClassAllTypes' property: 'ReadOnlyString' cannot be written to."));
            }

            [Test]
            public void WhenValueNullIsAllowed_ThenSetProperty()
            {
                _sut.SetPropertyValue(nameof(TestClassAllTypes.String), null);

                Assert.That(_sut.String, Is.Null);
            }

            [Test]
            public void WhenValueNullIsNotAllowed_ThenThrowException()
            {
                Assert.Throws<InvalidOperationException>(() => _sut.SetPropertyValue(nameof(TestClassAllTypes.Int), null));
            }

            [TestCase("PublicWritable")]
            [TestCase("PrivateWritable")]
            public void WhenPropertyIsWritable_ThenSetProperty(string propertyName)
            {
                var sut = new DummyWithProperties();

                sut.SetPropertyValue(propertyName, "John");

                var actual = sut.GetPropertyValue<string>(propertyName);

                Assert.That(actual, Is.EqualTo("John"));
            }

            [TestCase("PublicWritable")]
            [TestCase("PrivateWritable")]
            public void WhenPropertyIsWritable_AndIgnoreCase_ThenSetProperty(string propertyName)
            {
                var sut = new DummyWithProperties();

                sut.SetPropertyValue(propertyName.ToLower(), "John", true);

                var actual = sut.GetPropertyValue<string>(propertyName);

                Assert.That(actual, Is.EqualTo("John"));
            }
            
            [Test]
            public void WhenTypeString_ThenSetProperty()
            {
                _sut.SetPropertyValue(nameof(TestClassAllTypes.String), "John");

                Assert.That(_sut.String, Is.EqualTo("John"));
            }

            [Test]
            public void WhenTypeBool_ThenSetProperty()
            {
                _sut.SetPropertyValue(nameof(TestClassAllTypes.Bool), true.ToString());

                Assert.That(_sut.Bool, Is.EqualTo(true));
            }

            [Test]
            public void WhenTypeChar_ThenSetProperty()
            {
                _sut.SetPropertyValue(nameof(TestClassAllTypes.Char), 'A'.ToString());

                Assert.That(_sut.Char, Is.EqualTo('A'));
            }
            
            [Test]
            public void WhenTypeLong_ThenSetProperty()
            {
                _sut.SetPropertyValue(nameof(TestClassAllTypes.Long), long.MaxValue.ToString());

                Assert.That(_sut.Long, Is.EqualTo(long.MaxValue));
            }

            [Test]
            public void WhenTypeInt_ThenSetProperty()
            {
                _sut.SetPropertyValue(nameof(TestClassAllTypes.Int), int.MaxValue.ToString());

                Assert.That(_sut.Int, Is.EqualTo(int.MaxValue));
            }

            [Test]
            public void WhenTypeShort_ThenSetProperty()
            {
                _sut.SetPropertyValue(nameof(TestClassAllTypes.Short), short.MaxValue.ToString());

                Assert.That(_sut.Short, Is.EqualTo(short.MaxValue));
            }

            [Test]
            public void WhenTypeByte_ThenSetProperty()
            {
                _sut.SetPropertyValue(nameof(TestClassAllTypes.Byte), byte.MaxValue.ToString());

                Assert.That(_sut.Byte, Is.EqualTo(byte.MaxValue));
            }

            [Test]
            public void WhenTypeDecimal_ThenSetProperty()
            {
                _sut.SetPropertyValue(nameof(TestClassAllTypes.Decimal), decimal.MaxValue.ToString());

                Assert.That(_sut.Decimal, Is.EqualTo(decimal.MaxValue));
            }

            [Test]
            public void WhenTypeDouble_ThenSetProperty()
            {
                _sut.SetPropertyValue(nameof(TestClassAllTypes.Double), double.MaxValue.ToString());

                Assert.That(_sut.Double, Is.EqualTo(double.MaxValue));
            }

            [Test]
            public void WhenTypeFloat_ThenSetProperty()
            {
                _sut.SetPropertyValue(nameof(TestClassAllTypes.Float), float.MaxValue.ToString());

                Assert.That(_sut.Float, Is.EqualTo(float.MaxValue));
            }

            [Test]
            public void WhenTypeULong_ThenSetProperty()
            {
                _sut.SetPropertyValue(nameof(TestClassAllTypes.ULong), ulong.MaxValue.ToString());

                Assert.That(_sut.ULong, Is.EqualTo(ulong.MaxValue));
            }

            [Test]
            public void WhenTypeUInt_ThenSetProperty()
            {
                _sut.SetPropertyValue(nameof(TestClassAllTypes.UInt), uint.MaxValue.ToString());

                Assert.That(_sut.UInt, Is.EqualTo(uint.MaxValue));
            }

            [Test]
            public void WhenTypeUShort_ThenSetProperty()
            {
                _sut.SetPropertyValue(nameof(TestClassAllTypes.UShort), ushort.MaxValue.ToString());

                Assert.That(_sut.UShort, Is.EqualTo(ushort.MaxValue));
            }

            [Test]
            public void WhenTypeSByte_ThenSetProperty()
            {
                _sut.SetPropertyValue(nameof(TestClassAllTypes.SByte), sbyte.MaxValue.ToString());

                Assert.That(_sut.SByte, Is.EqualTo(sbyte.MaxValue));
            }

            [Test]
            public void WhenTypeUserDefinedObject_ThenSetProperty()
            {
                var obj = new TestClassAllTypes();

                _sut.SetPropertyValue(nameof(TestClassAllTypes.String), obj);

                Assert.That(_sut.String, Is.EqualTo(obj.ToString()));
            }
        }
    }
}