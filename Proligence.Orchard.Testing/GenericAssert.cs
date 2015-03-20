#if (NUNIT)
using NUnit.Framework;
#endif
using System.Collections.Generic;
#if (XUNIT)
using Xunit;
#endif

namespace Proligence.Orchard.Testing
{
    public static class GenericAssert
    {
        public static void AreEqual<T>(T expected, T actual)
        {
#if (NUNIT)
            Assert.That(actual, Is.EqualTo(expected));
#elif (XUNIT)
            Assert.Equal(expected, actual);
#endif
        }

        public static void AreSame<T>(T expected, T actual)
        {
#if (NUNIT)
            Assert.That(actual, Is.SameAs(expected));
#elif (XUNIT)
            Assert.Same(expected, actual);
#endif
        }

        public static void Fail(string message)
        {
#if (NUNIT)
            Assert.Fail(message);
#elif (XUNIT)
            Assert.True(false, message);
#endif
        }

        public static void True(bool condition, string message)
        {
#if (NUNIT)
            Assert.IsTrue(condition, message);
#elif (XUNIT)
            Assert.True(condition, message);
#endif
        }

        public static void InstanceOf<T>(object obj)
        {
#if (UNIT)
            Assert.That(obj, Is.InstanceOf<T>());
#elif (XUNIT)
            Assert.IsType<T>(obj);
#endif
        }

        public static void CollectionsAreEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
#if (NUNIT)
            Assert.That(actual, Is.EqualTo(expected));
#elif (XUNIT)
            Assert.Equal(expected, actual);
#endif
        }
    }
}