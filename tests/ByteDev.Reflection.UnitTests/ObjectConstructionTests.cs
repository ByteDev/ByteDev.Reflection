using System;
using System.Collections.Generic;
using ByteDev.Reflection.UnitTests.TestTypes;
using NUnit.Framework;

namespace ByteDev.Reflection.UnitTests
{
    [TestFixture]
    public class ObjectConstructionTests
    {
        [TestFixture]
        public class ConstructNonPublic
        {
            [Test]
            public void WhenParamTypesIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => ObjectConstruction.ConstructNonPublic<TestClassPrivateConstructor>(null));
            }

            [Test]
            public void WhenNoMatchingContructorFound_ThenThrowException()
            {
                var parameters = new Dictionary<Type, object>
                {
                    { typeof(string), "Hello World" }
                };

                var ex = Assert.Throws<InvalidOperationException>(() => ObjectConstruction.ConstructNonPublic<TestClassPrivateConstructor>(parameters));
                
                Assert.That(ex.Message, Is.EqualTo("No matching constructor could be found."));
            }

            [Test]
            public void WhenHasPrivateConstructor_ThenReturnInstance()
            {
                var result = ObjectConstruction.ConstructNonPublic<TestClassPrivateConstructor>();

                Assert.That(result.One, Is.EqualTo(1));
            }            
            
            [Test]
            public void WhenHasPrivateProtectedConstructor_ThenReturnInstance()
            {
                var result = ObjectConstruction.ConstructNonPublic<TestClassPrivateProtectedConstructor>();

                Assert.That(result.One, Is.EqualTo(1));
            }

            [Test]
            public void WhenHasProtectedConstructor_ThenReturnInstance()
            {
                var result = ObjectConstruction.ConstructNonPublic<TestClassProtectedConstructor>();

                Assert.That(result.One, Is.EqualTo(1));
            }            
            
            [Test]
            public void WhenHasInternalConstructor_ThenReturnInstance()
            {
                var result = ObjectConstruction.ConstructNonPublic<TestClassInternalConstructor>();

                Assert.That(result.One, Is.EqualTo(1));
            }            
            
            [Test]
            public void WhenHasInternalConstructor_WithParam_ThenReturnInstance()
            {
                var parameters = new Dictionary<Type, object>
                {
                    {typeof(int), 10}
                };

                var result = ObjectConstruction.ConstructNonPublic<TestClassInternalWithParamConstructor>(parameters);

                Assert.That(result.Value, Is.EqualTo(10));
            }
        }

    }
}