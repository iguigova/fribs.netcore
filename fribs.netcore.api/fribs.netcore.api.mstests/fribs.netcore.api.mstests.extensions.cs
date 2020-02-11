using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;

namespace fribs.netcore.api.mstests
{
    // https://dev.to/franndotexe/mstest-v2---new-old-kid-on-the-block

    public static class AssertionExtensions
    {
        public static For<T> For<T>(this Assert assert, T instance)
        {
            return new For<T>(instance);
        }
    }

    public class For<T>
    {
        private readonly T _instanceUnderTest;

        public For(T instanceUnderTest)
        {
            _instanceUnderTest = instanceUnderTest;
        }

        public void IsTrue(Expression<Func<T, bool>> assertionExpression)
        {
            if (assertionExpression.Compile().Invoke(_instanceUnderTest)) { return; }

            throw new AssertFailedException($"Assertion failed for expression '{assertionExpression}'.");
        }
    }
}
