using System;
using System.Linq;
using ByteDev.Collections;
using ByteDev.Reflection.UnitTests.TestTypes;
using NUnit.Framework;

namespace ByteDev.Reflection.UnitTests
{
    [TestFixture]
    public class TypeExtensionsTests
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
                    Assert.Throws<ArgumentNullException>(() => TypeExtensions.IsTestClass(null));
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

        [TestFixture]
        public class HasAttribute
        {
            [Test]
            public void WhenTypeIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => TypeExtensions.HasAttribute<UsedMethodAttribute>(null));
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

            [Test]
            public void WhenTypesMethodsHasAttribute_ThenReturnFalse()
            {
                var sut = typeof(DummyWithMethods);

                var result = sut.HasAttribute<UsedMethodAttribute>();

                Assert.That(result, Is.False);
            }
        }

        [TestFixture]
        public class GetPropertyOrThrow
        {
            private Type _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = typeof(DummyWithProperties);
            }

            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetPropertyOrThrow(null, "PublicReadOnly"));
            }

            [Test]
            public void WhenPropertyNameIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.GetPropertyOrThrow(null));
            }

            [Test]
            public void WhenPropertyNameIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.GetPropertyOrThrow(string.Empty));
            }

            [Test]
            public void WhenPropertyDoesNotExist_ThenThrowException()
            {
                Assert.Throws<InvalidOperationException>(() => _sut.GetPropertyOrThrow("DoesNotExist"));
            }

            [TestCase("PrivateReadOnly")]
            [TestCase("PrivateWritable")]
            [TestCase("ProtectedReadOnly")]
            [TestCase("ProtectedWritable")]
            [TestCase("InternalReadOnly")]
            [TestCase("InternalWritable")]
            [TestCase("PublicReadOnly")]
            [TestCase("PublicWritable")]
            public void WhenPropertyExists_ThenReturnProperty(string propertyName)
            {
                var result = _sut.GetPropertyOrThrow(propertyName);

                Assert.That(result.Name, Is.EqualTo(propertyName));
            }

            [TestCase("privatereadonly", "PrivateReadOnly")]
            [TestCase("privatewritable", "PrivateWritable")]
            [TestCase("protectedreadonly", "ProtectedReadOnly")]
            [TestCase("protectedwritable", "ProtectedWritable")]
            [TestCase("internalreadonly", "InternalReadOnly")]
            [TestCase("internalwritable", "InternalWritable")]
            [TestCase("publicreadonly", "PublicReadOnly")]
            [TestCase("publicwritable", "PublicWritable")]
            public void WhenPropertyExists_AndIgnoreCase_ThenReturnProperty(string propertyName, string expected)
            {
                var result = _sut.GetPropertyOrThrow(propertyName, true);

                Assert.That(result.Name, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class GetStaticPropertyOrThrow
        {
            private Type _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = typeof(DummyWithProperties);
            }

            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetStaticPropertyOrThrow(null, "PublicReadOnly"));
            }

            [Test]
            public void WhenPropertyNameIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.GetStaticPropertyOrThrow(null));
            }

            [Test]
            public void WhenPropertyNameIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.GetStaticPropertyOrThrow(string.Empty));
            }

            [Test]
            public void WhenPropertyDoesNotExist_ThenThrowException()
            {
                Assert.Throws<InvalidOperationException>(() => _sut.GetStaticPropertyOrThrow("DoesNotExist"));
            }

            [TestCase("StaticPrivateReadOnly")]
            [TestCase("StaticPrivateWritable")]
            [TestCase("StaticProtectedReadOnly")]
            [TestCase("StaticProtectedWritable")]
            [TestCase("StaticInternalReadOnly")]
            [TestCase("StaticInternalWritable")]
            [TestCase("StaticPublicReadOnly")]
            [TestCase("StaticPublicWritable")]
            public void WhenPropertyExists_ThenReturnProperty(string propertyName)
            {
                var result = _sut.GetStaticPropertyOrThrow(propertyName);

                Assert.That(result.Name, Is.EqualTo(propertyName));
            }

            [TestCase("staticprivatereadonly", "StaticPrivateReadOnly")]
            [TestCase("staticprivatewritable", "StaticPrivateWritable")]
            [TestCase("staticprotectedreadonly", "StaticProtectedReadOnly")]
            [TestCase("staticprotectedwritable", "StaticProtectedWritable")]
            [TestCase("staticinternalreadonly", "StaticInternalReadOnly")]
            [TestCase("staticinternalwritable", "StaticInternalWritable")]
            [TestCase("staticpublicreadonly", "StaticPublicReadOnly")]
            [TestCase("staticpublicwritable", "StaticPublicWritable")]
            public void WhenPropertyExists_AndIgnoreCase_ThenReturnProperty(string propertyName, string expected)
            {
                var result = _sut.GetStaticPropertyOrThrow(propertyName, true);

                Assert.That(result.Name, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class GetStaticPropertyValue
        {
            private Type _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = typeof(DummyWithProperties);
            }

            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetStaticPropertyValue<string>(null, "PublicReadOnly"));
            }

            [Test]
            public void WhenPropertyNameIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.GetStaticPropertyValue<string>(null));
            }

            [Test]
            public void WhenPropertyNameIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.GetStaticPropertyValue<string>(string.Empty));
            }

            [Test]
            public void WhenPropertyDoesNotExist_ThenThrowException()
            {
                Assert.Throws<InvalidOperationException>(() => _sut.GetStaticPropertyValue<string>("DoesNotExist"));
            }

            [TestCase("StaticPrivateReadOnly", "Static Private read default value")]
            [TestCase("StaticPrivateWritable", "Static Private writable default value")]
            [TestCase("StaticProtectedReadOnly", "Static Protected read default value")]
            [TestCase("StaticProtectedWritable", "Static Protected writable default value")]
            [TestCase("StaticInternalReadOnly", "Static Internal read default value")]
            [TestCase("StaticInternalWritable", "Static Internal writable default value")]
            [TestCase("StaticPublicReadOnly", "Static Public read default value")]
            [TestCase("StaticPublicWritable", "Static Public writable default value")]
            public void WhenPropertyExists_ThenReturnValue(string propertyName, string expected)
            {
                var result = _sut.GetStaticPropertyValue<string>(propertyName);

                Assert.That(result, Is.EqualTo(expected));
            }

            [TestCase("Staticprivatereadonly", "Static Private read default value")]
            [TestCase("Staticprivatewritable", "Static Private writable default value")]
            [TestCase("Staticprotectedreadonly", "Static Protected read default value")]
            [TestCase("Staticprotectedwritable", "Static Protected writable default value")]
            [TestCase("Staticinternalreadonly", "Static Internal read default value")]
            [TestCase("Staticinternalwritable", "Static Internal writable default value")]
            [TestCase("staticpublicreadonly", "Static Public read default value")]
            [TestCase("staticpublicwritable", "Static Public writable default value")]
            public void WhenPropertyExists_AndIgnoreCase_ThenReturnValue(string propertyName, string expected)
            {
                var result = _sut.GetStaticPropertyValue<string>(propertyName, true);

                Assert.That(result, Is.EqualTo(expected));
            }
        }
        
        [TestFixture]
        public class GetConstants
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetConstants(null));
            }

            [Test]
            public void WhenTypeHasNoConsts_ThenReturnEmpty()
            {
                var result = typeof(DummyWithNoConsts).GetConstants();

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenTypeHasConsts_ThenReturnAllConstFields()
            {
                var result = typeof(DummyWithConsts).GetConstants();

                Assert.That(result.Count(), Is.EqualTo(6));
            }

            [Test]
            public void WhenPublicOnly_ThenReturnPublicConstFields()
            {
                var result = typeof(DummyWithConsts).GetConstants(true);

                Assert.That(result.Count(), Is.EqualTo(2));
            }
        }

        [TestFixture]
        public class GetConstantsValues
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetConstantsValues<string>(null));
            }

            [Test]
            public void WhenTypeHasNoConsts_ThenReturnEmpty()
            {
                var result = typeof(DummyWithNoConsts).GetConstantsValues<string>();

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenTypeHasConsts_ThenReturnAllConstValues()
            {
                var result = typeof(DummyWithConsts).GetConstantsValues<string>().ToList();

                Assert.That(result.Count, Is.EqualTo(6));
                Assert.That(result.First(), Is.EqualTo("PrivateConstValue1"));
                Assert.That(result.Second(), Is.EqualTo("ProtectedValue1"));
                Assert.That(result.Third(), Is.EqualTo("InternalValue1"));
                Assert.That(result.Fourth(), Is.EqualTo("ProtectedInternalValue1"));
                Assert.That(result.Fifth(), Is.EqualTo("PublicValue1"));
                Assert.That(result.Sixth(), Is.EqualTo("PublicValue2"));
            }

            [Test]
            public void WhenPublicOnly_ThenReturnPublicConstValues()
            {
                var result = typeof(DummyWithConsts).GetConstantsValues<string>(true).ToList();

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result.First(), Is.EqualTo("PublicValue1"));
                Assert.That(result.Second(), Is.EqualTo("PublicValue2"));
            }
        }
    }
}