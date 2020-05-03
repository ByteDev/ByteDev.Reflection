namespace ByteDev.Reflection.UnitTests.TestTypes
{
    internal class DummyWithConsts
    {
        private const string PrivateConst1 = "PrivateConstValue1";

        protected const string ProtectedConst1 = "ProtectedValue1";

        internal const string InternalConst1 = "InternalValue1";

        protected internal const string ProtectedInternal1 = "ProtectedInternalValue1";

        public const string PublicConst1 = "PublicValue1";
        public const string PublicConst2 = "PublicValue2";
    }

    internal class DummyWithNoConsts
    {
    }
}
