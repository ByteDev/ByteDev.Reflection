using System;
using NUnit.Framework;

namespace ByteDev.Reflection.UnitTests
{
    [TestFixture]
    public class GenericExtensionsTests
    {
        public int PublicMethod(int x, int y)
        {
            return x + y;
        }

        protected internal int ProtectedInternalMethod(int x, int y)
        {
            return x + y;
        }

        internal int InternalMethod(int x, int y)
        {
            return x + y;
        }

        protected int ProtectedMethod(int x, int y)
        {
            return x + y;
        }

        private int PrivateMethod(int x, int y)
        {
            return x + y;
        }

        private void PrivateMethodReturnVoid()
        {
        }


        public static int StaticPublicMethod(int x, int y)
        {
            return x + y;
        }

        protected internal static int StaticProtectedInternalMethod(int x, int y)
        {
            return x + y;
        }

        internal static int StaticInternalMethod(int x, int y)
        {
            return x + y;
        }

        protected static int StaticProtectedMethod(int x, int y)
        {
            return x + y;
        }

        private static int StaticPrivateMethod(int x, int y)
        {
            return x + y;
        }

        private static void StaticPrivateMethodReturnVoid()
        {
        }
        
        [TestFixture]
        public class InvokeMethod
        {
            private GenericExtensionsTests _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = new GenericExtensionsTests();
            }

            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => GenericExtensions.InvokeMethod(null as object, "PrivateMethodReturnVoid"));
            }

            [Test]
            public void WhenMethodNameIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.InvokeMethod(null));
            }

            [Test]
            public void WhenMethodNameIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.InvokeMethod(string.Empty));
            }

            [Test]
            public void WhenMethodNameDoesNotExist_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.InvokeMethod("ThisMethodDoesNotExist"));
            }
            
            [TestCase("PublicMethod")]
            [TestCase("ProtectedInternalMethod")]
            [TestCase("InternalMethod")]
            [TestCase("ProtectedMethod")]
            [TestCase("PrivateMethod")]
            [TestCase("StaticPublicMethod")]
            [TestCase("StaticProtectedInternalMethod")]
            [TestCase("StaticInternalMethod")]
            [TestCase("StaticProtectedMethod")]
            [TestCase("StaticPrivateMethod")]
            public void WhenInvokingExistingMethod_ThenReturnValue(string methodName)
            {
                var result = _sut.InvokeMethod(methodName, 1, 2);

                Assert.That(result, Is.EqualTo(3));
            }
            
            [Test]
            public void WhenInvokingExistingMethodWithVoidReturn_ThenReturnNull()
            {
                var result = _sut.InvokeMethod("PrivateMethodReturnVoid");

                Assert.That(result, Is.Null);
            }

            [Test]
            public void WhenInvokingExistingStaticMethodWithVoidReturn_ThenReturnNull()
            {
                var result = _sut.InvokeMethod("StaticPrivateMethodReturnVoid");

                Assert.That(result, Is.Null);
            }
        }
    }
}