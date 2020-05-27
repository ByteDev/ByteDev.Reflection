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

        [TestFixture]
        public class GetBaseTypes
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetBaseTypes(null));
            }

            [Test]
            public void WhenTypeHasNoParent_ThenReturnObjectType()
            {
                var result = typeof(DummyWithProperty).GetBaseTypes();

                Assert.That(result.Single().Name, Is.EqualTo("Object"));
            }

            [Test]
            public void WhenTypeHasOwnParent_ThenReturnBaseType()
            {
                var result = typeof(DummyChildsChild1).GetBaseTypes();

                Assert.That(result.Count, Is.EqualTo(3));
                Assert.That(result.First().Name, Is.EqualTo("DummyChild2"));
                Assert.That(result.Second().Name, Is.EqualTo("DummyWithProperties"));
                Assert.That(result.Third().Name, Is.EqualTo("Object"));
            }
        }

        [TestFixture]
        public class GetImplementedInterfaces
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetImplementedInterfaces(null));
            }

            [Test]
            public void WhenTypeHasNoInterfaces_ThenReturnEmpty()
            {
                var sut = typeof(DummyWithProperty);

                var result = sut.GetImplementedInterfaces();

                Assert.That(result, Is.Empty);
            }            
            
            [Test]
            public void WhenTypeHasTwoInterfaces_ThenReturnInterfaces()
            {
                var sut = typeof(DummyWithInterfaces1);

                var result = sut.GetImplementedInterfaces().ToList();

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result.First().Name, Is.EqualTo("IDummyInterface1"));
                Assert.That(result.Second().Name, Is.EqualTo("IDummyInterface2"));
            }

            [Test]
            public void WhenTypeHasBaseWithInterfaces_ThenReturnInterfaces()
            {
                var sut = typeof(DummyWithInterfaces2);

                var result = sut.GetImplementedInterfaces().ToList();

                Assert.That(result.Count, Is.EqualTo(3));
                Assert.That(result.First().Name, Is.EqualTo("IDummyInterface1"));
                Assert.That(result.Second().Name, Is.EqualTo("IDummyInterface2"));
                Assert.That(result.Third().Name, Is.EqualTo("IDummyInterface0"));
            }
        }

        [TestFixture]
        public class GetPropertiesWithAttribute
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetPropertiesWithAttribute<UsedPropertyAttribute>(null));
            }

            [Test]
            public void WhenNoPropertiesHaveAttribute_ThenReturnEmpty()
            {
                var result = typeof(DummyWithPropertyAttributes).GetPropertiesWithAttribute<NotUsedAttribute>();

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenPropertiesHaveAttribute_ThenReturnProperties()
            {
                var result = typeof(DummyWithPropertyAttributes).GetPropertiesWithAttribute<UsedPropertyAttribute>().ToList();

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result.First().Name, Is.EqualTo("Property1"));
                Assert.That(result.Second().Name, Is.EqualTo("Property3"));
            }
        }

        [TestFixture]
        public class GetEnumProperties
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetEnumProperties(null));
            }

            [Test]
            public void WhenHasNoEnumProperties_ThenReturnEmpty()
            {
                var result = typeof(DummyWithNoEnumProperties).GetEnumProperties();

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenHasEnumProperties_ThenReturnProperties()
            {
                var result = typeof(DummyWithEnumProperties).GetEnumProperties().ToList();

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result.First().Name, Is.EqualTo("Property1"));
                Assert.That(result.Second().Name, Is.EqualTo("Property2"));
            }
        }

        [TestFixture]
        public class GetPropertiesOfType
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetPropertiesOfType(null, typeof(string)));
            }

            [Test]
            public void WhenHasNoPropertiesOfType_ThenReturnEmpty()
            {
                var result = typeof(DummyWithProperties).GetPropertiesOfType(typeof(decimal));

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenHasPropertiesOfType_ThenReturnProperties()
            {
                var result = typeof(DummyWithProperties).GetPropertiesOfType(typeof(int));

                Assert.That(result.Count(), Is.EqualTo(1));
            }
        }

        [TestFixture]
        public class ConstructNonPublic
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => TypeExtensions.ConstructNonPublic<TestClassPrivateConstructor>(null));
            }

            [Test]
            public void WhenParamTypesIsNull_ThenThrowException()
            {
                var sut = typeof(TestClassPrivateConstructor);

                Assert.Throws<ArgumentNullException>(() => sut.ConstructNonPublic<TestClassPrivateConstructor>(null, new object[0]));
            }

            [Test]
            public void WhenParamObjectsIsNull_ThenThrowException()
            {
                var sut = typeof(TestClassPrivateConstructor);

                Assert.Throws<ArgumentNullException>(() => sut.ConstructNonPublic<TestClassPrivateConstructor>(new Type[0], null));
            }

            [Test]
            public void WhenNoMatchingContructorFound_ThenThrowException()
            {
                var sut = typeof(TestClassPrivateConstructor);

                Assert.Throws<InvalidOperationException>(() => sut.ConstructNonPublic<TestClassPrivateConstructor>(new[] {typeof(string)}, new object[] {"Hello world"}));
            }

            [Test]
            public void WhenHasPrivateConstructor_ThenReturnInstance()
            {
                var sut = typeof(TestClassPrivateConstructor);

                var result = sut.ConstructNonPublic<TestClassPrivateConstructor>();

                Assert.That(result.One, Is.EqualTo(1));
            }            
            
            [Test]
            public void WhenHasPrivateProtectedConstructor_ThenReturnInstance()
            {
                var sut = typeof(TestClassPrivateProtectedConstructor);

                var result = sut.ConstructNonPublic<TestClassPrivateProtectedConstructor>();

                Assert.That(result.One, Is.EqualTo(1));
            }

            [Test]
            public void WhenHasProtectedConstructor_ThenReturnInstance()
            {
                var sut = typeof(TestClassProtectedConstructor);

                var result = sut.ConstructNonPublic<TestClassProtectedConstructor>();

                Assert.That(result.One, Is.EqualTo(1));
            }            
            
            [Test]
            public void WhenHasInternalConstructor_ThenReturnInstance()
            {
                var sut = typeof(TestClassInternalConstructor);

                var result = sut.ConstructNonPublic<TestClassInternalConstructor>();

                Assert.That(result.One, Is.EqualTo(1));
            }            
            
            [Test]
            public void WhenHasInternalConstructor_WithParam_ThenReturnInstance()
            {
                var sut = typeof(TestClassInternalWithParamConstructor);

                var result = sut.ConstructNonPublic<TestClassInternalWithParamConstructor>(new[] { typeof(int) }, new object[] { 10 });

                Assert.That(result.Value, Is.EqualTo(10));
            }
        }
    }
}