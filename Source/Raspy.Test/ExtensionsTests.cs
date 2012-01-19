//-----------------------------------------------------------------------------------------
// <copyright file="ExtensionsTests.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Raspy;

    /// <summary>
    /// Extensions tests.
    /// </summary>
    [TestClass]
    public sealed class ExtensionsTests
    {
        /// <summary>
        /// Parse tests.
        /// </summary>
        [TestMethod]
        public void ExtensionsParse()
        {
            ExpressionQueue output = "3+4".Parse();
            Assert.IsNotNull(output);
        }
        
        /// <summary>
        /// Parse and evaluate tests.
        /// </summary>
        [TestMethod]
        public void ExtensionsParseAndEvaluate()
        {
            Assert.AreEqual(7L, "3+4".ParseAndEvaluate());
            Assert.IsInstanceOfType("3+4".ParseAndEvaluate<int>(), typeof(int));
            Assert.IsInstanceOfType("3+4".ParseAndEvaluate<float>(), typeof(float));
            Assert.IsInstanceOfType("7/2".ParseAndEvaluate<double>(), typeof(double));
            Assert.IsInstanceOfType("7/2".ParseAndEvaluate<long>(), typeof(long));
        }

        /// <summary>
        /// Try parse and evaluate tests.
        /// </summary>
        [TestMethod]
        public void ExtensionsTryParseAndEvaluate()
        {
            object o;
            int i;
            float f;
            double d;
            long l;

            Assert.AreEqual(true, "3+4".TryParseAndEvaluate(out o));
            Assert.AreEqual(true, "3+4".TryParseAndEvaluate<int>(out i));
            Assert.AreEqual(true, "3+4".TryParseAndEvaluate<float>(out f));
            Assert.AreEqual(true, "7/2".TryParseAndEvaluate<double>(out d));
            Assert.AreEqual(true, "7/2".TryParseAndEvaluate<long>(out l));

            Assert.AreEqual(false, "3+4+".TryParseAndEvaluate(out o));
        }
    }
}
