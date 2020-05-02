﻿using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace ByteDev.Reflection.UnitTests
{
    [TestFixture]
    public class MemberInfoExtensionsTests
    {
        [TestFixture]
        public class GetAttribute
        {
            [Test]
            public void WhenIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.GetAttribute<PersonAttribute>(null));
            }

            [Test]
            public void WhenMemberHasAttribute_ThenReturnAttribute()
            {
                MemberInfo sut = typeof(Company).GetMember("Ceo").Single();

                var result = sut.GetAttribute<PersonAttribute>();

                Assert.That(result.Name, Is.EqualTo("John Smith"));
            }

            [Test]
            public void WhenMemberDoesNotHaveAttribute_ThenReturnNull()
            {
                MemberInfo sut = typeof(Org).GetMember("Ceo").Single();

                var result = sut.GetAttribute<PersonAttribute>();

                Assert.That(result, Is.Null);
            }

            [Test]
            public void WhenMemberHasMultipleOfAttribute_ThenThrowException()
            {
                MemberInfo sut = typeof(CompanyMultiCeo).GetMember("Ceo").Single();

                Assert.Throws<InvalidOperationException>(() => sut.GetAttribute<PersonAttribute>());
            }
        }

        [TestFixture]
        public class HasAttribute_MemberInfo
        {
            [Test]
            public void WhenTypeIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => (null as MemberInfo).HasAttribute<UsedAttribute>());
            }

            [Test]
            public void WhenMethodHasAttribute_ThenReturnTrue()
            {
                MethodInfo sut = typeof(DummyWithMethods).GetMethod("MethodWithAttribute");

                var result = sut.HasAttribute<UsedAttribute>();

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenMethodDoesNotHaveAttribute_ThenReturnFalse()
            {
                MethodInfo sut = typeof(DummyWithMethods).GetMethod("MethodWithNoAttribute");

                var result = sut.HasAttribute<UsedAttribute>();

                Assert.That(result, Is.False);
            }
        }

        internal class Org
        {
            public string Ceo { get; set; }
        }

        internal class Company
        {
            [Person("John Smith")]
            public string Ceo { get; set; }
        }

        internal class CompanyMultiCeo
        {
            [Person("John Smith")]
            [Person("Joe Bloggs")]
            public string Ceo { get; set; }
        }

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
        internal sealed class PersonAttribute : Attribute
        {
            public string Name { get; set; }

            public PersonAttribute(string name)
            {
                Name = name;
            }
        }
    }
}