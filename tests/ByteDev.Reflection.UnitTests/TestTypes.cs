using System;

namespace ByteDev.Reflection.UnitTests
{
    public class UsedAttribute : Attribute
    {
    }

    public class NotUsedAttribute : Attribute
    {
    }

    public class DummyWithMethods
    {
        [Used]
        public void MethodWithAttribute()
        {
        }

        public void MethodWithNoAttribute()
        {
        }
    }

    [Used]
    public class DummyWithClassAttribute
    {
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


    public class Car
    {
        public string PublicReadOnly { get; } = "Public default value";
    
        public string PublicWritable { get; set; }

        public string PrivateReadOnly { get; } = "Private default value";

        public string PrivateWritable { get; set; }
    }

    public class Ford : Car
    {
    }

    public class Fiat : Car
    {
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