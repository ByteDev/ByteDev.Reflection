using System;

namespace ByteDev.Reflection.UnitTests.TestTypes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class UsedMethodAttribute : Attribute
    {
        public string Name { get; } = "John Smith";

        public UsedMethodAttribute()
        {
        }

        public UsedMethodAttribute(string name)
        {
            Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class UsedPropertyAttribute : Attribute
    {
    }

    public sealed class UsedClassAttribute : Attribute
    {
    }

    public sealed class NotUsedAttribute : Attribute
    {
    }

    public class DummyWithMethods
    {
        [UsedMethod]
        public void MethodWithAttribute()
        {
        }

        public void MethodWithNoAttribute()
        {
        }

        [UsedMethod("Peter Smith")]
        [UsedMethod("Paul Smith")]
        public void MethodWithMultiAttribute()
        {
        }
    }

    [UsedClass]
    public class DummyWithClassAttribute
    {
    }

    public class DummyWithPropertyAttributes
    {
        [UsedProperty]
        public string Property1 { get; set; }

        public string Property2 { get; set; }

        [UsedProperty]
        public string Property3 { get; set; }
    }
}
