namespace ByteDev.Reflection.UnitTests.TestTypes
{
    public enum Enum1
    {
    }

    public enum Enum2
    {
    }

    public class DummyWithNoEnumProperties
    {
    }

    public class DummyWithEnumProperties
    {
        public Enum1 Property1 { get; set; }

        public Enum2 Property2 { get; set; }

        public string Property3 { get; set; }
    }
}