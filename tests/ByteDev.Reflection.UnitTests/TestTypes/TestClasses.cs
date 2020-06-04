namespace ByteDev.Reflection.UnitTests.TestTypes
{
    public class TestClassPrivateConstructor
    {
        public int One { get; } = 1;

        private TestClassPrivateConstructor()
        {
        }
    }

    public class TestClassPrivateProtectedConstructor
    {
        public int One { get; } = 1;

        protected TestClassPrivateProtectedConstructor()
        {
        }
    }

    public class TestClassProtectedConstructor
    {
        public int One { get; } = 1;

        protected TestClassProtectedConstructor()
        {
        }
    }

    public class TestClassInternalConstructor
    {
        public int One { get; } = 1;

        internal TestClassInternalConstructor()
        {
        }
    }

    public class TestClassInternalWithParamConstructor
    {
        public int Value { get; set; }

        internal TestClassInternalWithParamConstructor(int value)
        {
            Value = value;
        }
    }

    public class TestPerson
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}