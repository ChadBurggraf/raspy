//-----------------------------------------------------------------------------------------
// <copyright file="ArithmeticOperationsTests.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Arithmetic operations tests.
    /// </summary>
    [TestClass]
    public sealed class ArithmeticOperationsTests
    {
        /// <summary>
        /// Add tests.
        /// </summary>
        [TestMethod]
        public void ArithmeticOperationsAdd()
        {
            Operand left = new Operand(3);
            Operand right = new Operand(4);
            Assert.AreEqual(new Operand(7), ArithmeticOperationProvider.Add(new Token[] { left, right }));

            right = new Operand(4.2);
            Assert.AreEqual(new Operand(7.2), ArithmeticOperationProvider.Add(new Token[] { left, right }));

            left = new Operand(3.8);
            Assert.AreEqual(new Operand(8.0), ArithmeticOperationProvider.Add(new Token[] { left, right }));
        }

        /// <summary>
        /// Divide tests.
        /// </summary>
        [TestMethod]
        public void ArithmeticOperationsDivide()
        {
            Operand left = new Operand(3);
            Operand right = new Operand(4);
            Assert.AreEqual(new Operand(.75), ArithmeticOperationProvider.Divide(new Token[] { left, right }));
        }

        /// <summary>
        /// Factorial tests.
        /// </summary>
        [TestMethod]
        public void ArithmeticOperationsFactorial()
        {
            Operand op = new Operand(4);
            Assert.AreEqual(new Operand(24), ArithmeticOperationProvider.Factorial(new Token[] { op }));
        }

        /// <summary>
        /// Modulo tests.
        /// </summary>
        [TestMethod]
        public void ArithmeticOperationsModulo()
        {
            Operand left = new Operand(3);
            Operand right = new Operand(4);
            Assert.AreEqual(new Operand(3), ArithmeticOperationProvider.Modulo(new Token[] { left, right }));

            right = new Operand(4.2);
            Assert.AreEqual(new Operand(3.0), ArithmeticOperationProvider.Modulo(new Token[] { left, right }));
        }

        /// <summary>
        /// Multiply tests.
        /// </summary>
        [TestMethod]
        public void ArithmeticOperationsMultiply()
        {
            Operand left = new Operand(3);
            Operand right = new Operand(4);
            Assert.AreEqual(new Operand(12), ArithmeticOperationProvider.Multiply(new Token[] { left, right }));

            right = new Operand(4.5);
            Assert.AreEqual(new Operand(13.5), ArithmeticOperationProvider.Multiply(new Token[] { left, right }));
        }

        /// <summary>
        /// Power tests.
        /// </summary>
        [TestMethod]
        public void ArithmeticOperationsPower()
        {
            Operand left = new Operand(3);
            Operand right = new Operand(4);
            Assert.AreEqual(new Operand(81), ArithmeticOperationProvider.Power(new Token[] { left, right }));

            left = new Operand(2.5);
            right = new Operand(2);
            Assert.AreEqual(new Operand(6.25), ArithmeticOperationProvider.Power(new Token[] { left, right }));
        }

        /// <summary>
        /// Subtract tests.
        /// </summary>
        [TestMethod]
        public void ArithmeticOperationsSubtract()
        {
            Operand left = new Operand(4);
            Operand right = new Operand(3);
            Assert.AreEqual(new Operand(1), ArithmeticOperationProvider.Subtract(new Token[] { left, right }));

            right = new Operand(3.5);
            Assert.AreEqual(new Operand(.5), ArithmeticOperationProvider.Subtract(new Token[] { left, right }));

            right = new Operand(7);
            Assert.AreEqual(new Operand(-3), ArithmeticOperationProvider.Subtract(new Token[] { left, right }));
        }
    }
}
