﻿namespace ByteDev.Reflection.UnitTests.TestTypes
{
    public class TestClassAllTypes
    {
        public string String { get; set; }

        public bool Bool { get; set; }

        public char Char { get; set; }


        public long Long { get; set; }

        public int Int { get; set; }

        public short Short { get; set; }

        public byte Byte { get; set; }


        public decimal Decimal { get; set; }

        public double Double { get; set; }

        public float Float { get; set; }


        public ulong ULong { get; set; }

        public uint UInt { get; set; }

        public ushort UShort { get; set; }

        public sbyte SByte { get; set; }


        public TrafficLights Enum { get; set;}


        public string ReadOnlyString { get; }
    }

    public enum TrafficLights
    {
        Red = 1,
        Yellow = 2,
        Green = 3
    }
}