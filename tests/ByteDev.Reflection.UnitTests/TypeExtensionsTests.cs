using System.Linq;
using ByteDev.Collections;
using NUnit.Framework;

namespace ByteDev.Reflection.UnitTests
{
    [TestFixture]
    public class TypeExtensionsTests
    {
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
   }

    internal class DummyWithConsts
    {
        private const string PrivateConst1 = "value";

        protected const string ProtectedConst1 = "value";

        internal const string InternalConst1 = "value";

        protected internal const string ProtectedInternal1 = "value";

        public const string PublicConst1 = "value1";
        public const string PublicConst2 = "value2";
    }

    internal class DummyWithNoConsts
    {
    }
}