//-----------------------------------------------------------------------------------------
// <copyright file="ProviderFactoryTests.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Provider factory tests.
    /// </summary>
    [TestClass]
    public sealed class ProviderFactoryTests
    {
        /// <summary>
        /// Custom get provider tests.
        /// </summary>
        [TestMethod]
        public void ProviderFactoryCustomGetProvider()
        {
            AssertArithmenticOperations(new OperationProviderFactory(new IOperationProvider[] { new ArithmeticOperationProvider() }));
        }

        /// <summary>
        /// Default get provider tests.
        /// </summary>
        [TestMethod]
        public void ProviderFactoryDefaultGetProvider()
        {
            AssertArithmenticOperations(OperationProviderFactory.DefaultInstance);
        }

        /// <summary>
        /// Asserts all of the arithmetic operators against the given factory.
        /// </summary>
        /// <param name="factory">The factory to assert the arithmetic operators for.</param>
        private static void AssertArithmenticOperations(OperationProviderFactory factory)
        {
            Assert.IsNotNull(factory.GetProvider('!'));
            Assert.IsNotNull(factory.GetProvider('^'));
            Assert.IsNotNull(factory.GetProvider('*'));
            Assert.IsNotNull(factory.GetProvider('/'));
            Assert.IsNotNull(factory.GetProvider('%'));
            Assert.IsNotNull(factory.GetProvider('+'));
            Assert.IsNotNull(factory.GetProvider('-'));
        }
    }
}
