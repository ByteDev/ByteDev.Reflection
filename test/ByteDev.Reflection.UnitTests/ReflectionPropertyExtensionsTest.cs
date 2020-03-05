using System;
using NUnit.Framework;

namespace ByteDev.Reflection.UnitTests
{
    [TestFixture]
    public class ReflectionPropertyExtensionsTest
    {
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
                var sut = new ClassWithProperty();

                Assert.Throws<InvalidOperationException>(() => sut.GetStaticPropertyValue<string>("Surname"));
            }
        }

        [TestFixture]
        public class GetStaticPropertyValue
        {
            [Test]
            public void WhenClassHasStaticProperty_ThenReturnValue()
            {
                var sut = new ClassWithStaticProperty();
                ClassWithStaticProperty.Name = "John";

                var result = sut.GetStaticPropertyValue<string>("Name");

                Assert.That(result, Is.EqualTo(ClassWithStaticProperty.Name));
            }
            
            [Test]
            public void WhenClassHasNoStaticProperty_ThenThrowException()
            {
                var sut = new ClassWithStaticProperty();

                Assert.Throws<InvalidOperationException>(() => sut.GetStaticPropertyValue<string>("Surname"));
            }
        }

        public class ClassWithProperty
        {
            public string Name { get; set; }
            public AnotherClassWithProperty AnotherClassWithProperty { get; set; }
        }

        public class AnotherClassWithProperty
        {
            public int Age { get; set; }
        }

        public class ClassWithStaticProperty
        {
            public static string Name { get; set; }
        }
    }

}