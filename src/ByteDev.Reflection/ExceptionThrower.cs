using System;

namespace ByteDev.Reflection
{
    internal static class ExceptionThrower
    {
        public static void ThrowPropertyIsNotWriteable(Type type, string propertyName)
        {
            throw new InvalidOperationException($"Type: '{type.Name}' property: '{propertyName}' cannot be written to.");
        }

        public static void ThrowPropertyDoesNotExist(Type type, string propertyName)
        {
            throw new InvalidOperationException($"Type '{type.Name}' has no property called '{propertyName}'.");
        }
    }
}