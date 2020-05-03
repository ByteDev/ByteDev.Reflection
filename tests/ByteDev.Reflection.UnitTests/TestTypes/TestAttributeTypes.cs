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
}
