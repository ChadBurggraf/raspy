//-----------------------------------------------------------------------------------------
// <copyright file="ParserTests.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy.Test
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Parser tests.
    /// </summary>
    [TestClass]
    public sealed class ParserTests
    {
        /// <summary>
        /// Empty tests.
        /// </summary>
        [TestMethod]
        public void ParseEmpty()
        {
            Parser parser = new Parser();
            ExpressionQueue result = parser.Parse(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);

            result = parser.Parse(string.Empty);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);

            result = parser.Parse("     \t");
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

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
        /// Fail read operator tests.
        /// </summary>
        [TestMethod]
        public void ParserFailReadOperator()
        {
            Parser parser = new Parser();

            string expr = "3+4";
            int pos = 0;

            ReadResult result = parser.ReadOperator(expr, pos);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(0, result.Position);

            expr = "3&4";
            pos = 1;

            result = parser.ReadOperator(expr, pos);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.Position);
        }

        /// <summary>
        /// Parse tests.
        /// </summary>
        [TestMethod]
        public void ParserParse()
        {
            Parser parser = new Parser();
            ExpressionQueue output = parser.Parse("3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3");
            Assert.AreEqual(13, output.Count);

            Token t = output.Dequeue();
            Assert.IsInstanceOfType(t, typeof(Operand));
            Assert.AreEqual(3L, ((Operand)t).Value);

            t = output.Dequeue();
            Assert.IsInstanceOfType(t, typeof(Operand));
            Assert.AreEqual(4L, ((Operand)t).Value);

            t = output.Dequeue();
            Assert.IsInstanceOfType(t, typeof(Operand));
            Assert.AreEqual(2L, ((Operand)t).Value);

            t = output.Dequeue();
            Assert.IsInstanceOfType(t, typeof(RaspyOperator));
            Assert.AreEqual('*', ((RaspyOperator)t).Symbol);

            t = output.Dequeue();
            Assert.IsInstanceOfType(t, typeof(Operand));
            Assert.AreEqual(1L, ((Operand)t).Value);

            t = output.Dequeue();
            Assert.IsInstanceOfType(t, typeof(Operand));
            Assert.AreEqual(5L, ((Operand)t).Value);

            t = output.Dequeue();
            Assert.IsInstanceOfType(t, typeof(RaspyOperator));
            Assert.AreEqual('-', ((RaspyOperator)t).Symbol);

            t = output.Dequeue();
            Assert.IsInstanceOfType(t, typeof(Operand));
            Assert.AreEqual(2L, ((Operand)t).Value);

            t = output.Dequeue();
            Assert.IsInstanceOfType(t, typeof(Operand));
            Assert.AreEqual(3L, ((Operand)t).Value);

            t = output.Dequeue();
            Assert.IsInstanceOfType(t, typeof(RaspyOperator));
            Assert.AreEqual('^', ((RaspyOperator)t).Symbol);

            t = output.Dequeue();
            Assert.IsInstanceOfType(t, typeof(RaspyOperator));
            Assert.AreEqual('^', ((RaspyOperator)t).Symbol);

            t = output.Dequeue();
            Assert.IsInstanceOfType(t, typeof(RaspyOperator));
            Assert.AreEqual('/', ((RaspyOperator)t).Symbol);

            t = output.Dequeue();
            Assert.IsInstanceOfType(t, typeof(RaspyOperator));
            Assert.AreEqual('+', ((RaspyOperator)t).Symbol);
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

        /// <summary>
        /// Read operator tests.
        /// </summary>
        [TestMethod]
        public void ParserReadOperator()
        {
            Parser parser = new Parser();

            string expr = "3+4";
            int pos = 1;

            ReadResult result = parser.ReadOperator(expr, pos);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Token);
            Assert.AreEqual(2, result.Position);
            Assert.IsInstanceOfType(result.Token, typeof(RaspyOperator));
            Assert.AreEqual('+', ((RaspyOperator)result.Token).Symbol);
        }
    }
}
