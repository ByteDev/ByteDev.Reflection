namespace ByteDev.Reflection.UnitTests.TestTypes
{
    public class DummyWithProperties
    {
        private string PrivateReadOnly { get; } = "Private read default value";

        private string PrivateWritable { get; set; } = "Private writable default value";

        protected string ProtectedReadOnly { get; } = "Protected read default value";

        protected string ProtectedWritable { get; set; } = "Protected writable default value";

        internal string InternalReadOnly { get; } = "Internal read default value";

        internal string InternalWritable { get; set; } = "Internal writable default value";

        public string PublicReadOnly { get; } = "Public read default value";
    
        public string PublicWritable { get; set; } = "Public writable default value";


        public DummyWithProperty DummyWithProperty { get; set; }


        private static string StaticPrivateReadOnly { get; } = "Static Private read default value";

        private static string StaticPrivateWritable { get; set; } = "Static Private writable default value";

        protected static string StaticProtectedReadOnly { get; } = "Static Protected read default value";

        protected static string StaticProtectedWritable { get; set; } = "Static Protected writable default value";

        internal static string StaticInternalReadOnly { get; } = "Static Internal read default value";

        internal static string StaticInternalWritable { get; set; } = "Static Internal writable default value";

        public static string StaticPublicReadOnly { get; } = "Static Public read default value";

        public static string StaticPublicWritable { get; set; } = "Static Public writable default value";


        public int PublicWritableInt { get; set; }
    }

    public class DummyChild1 : DummyWithProperties
    {
    }

    public class DummyChild2 : DummyWithProperties
    {
    }

    public class DummyChildsChild1 : DummyChild2
    {
    }

    public class DummyWithProperty
    {
        public string PublicString { get; set; }
    }

    public interface IDummyInterface0
    {
    }

    public interface IDummyInterface1
    {
    }

    public interface IDummyInterface2
    {
    }

    public class DummyWithInterfaces1 : IDummyInterface1, IDummyInterface2
    {
    }

    public class DummyWithInterfaces2 : DummyWithInterfaces1, IDummyInterface0
    {
    }
}