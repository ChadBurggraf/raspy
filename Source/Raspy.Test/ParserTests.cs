//-----------------------------------------------------------------------------------------
// <copyright file="ParserTests.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Parser tests.
    /// </summary>
    [TestClass]
    public sealed class ParserTests
    {
        /// <summary>
        /// Fail read operand tests.
        /// </summary>
        [TestMethod]
        public void ParserFailReadOperand()
        {
            Parser parser = new Parser();

            string expr = "(467)";
            int pos = 0;

            ReadResult result = parser.ReadOperand(expr, pos);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(0, result.Position);

            expr = "FFF";
            pos = 0;

            result = parser.ReadOperand(expr, pos);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(0, result.Position);
        }

        /// <summary>
        /// Read operand tests.
        /// </summary>
        [TestMethod]
        public void ParserReadOperand()
        {
            Parser parser = new Parser();

            string expr = "3";
            int pos = 0;

            ReadResult result = parser.ReadOperand(expr, pos);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Token);
            Assert.AreEqual(1, result.Position);
            Assert.IsInstanceOfType(result.Token, typeof(Operand));
            Assert.AreEqual(3L, ((Operand)result.Token).Value);

            expr = "(467)";
            pos = 1;

            result = parser.ReadOperand(expr, pos);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Token);
            Assert.AreEqual(4, result.Position);
            Assert.IsInstanceOfType(result.Token, typeof(Operand));
            Assert.AreEqual(467L, ((Operand)result.Token).Value);

            expr = "0.1234";
            pos = 0;

            result = parser.ReadOperand(expr, pos);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Token);
            Assert.AreEqual(6, result.Position);
            Assert.IsInstanceOfType(result.Token, typeof(Operand));
            Assert.AreEqual(.1234, ((Operand)result.Token).Value);

            expr = ".2.3.4";
            pos = 0;

            result = parser.ReadOperand(expr, pos);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Token);
            Assert.AreEqual(2, result.Position);
            Assert.IsInstanceOfType(result.Token, typeof(Operand));
            Assert.AreEqual(.2, ((Operand)result.Token).Value);
        }
    }
}
