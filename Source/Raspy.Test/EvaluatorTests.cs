//-----------------------------------------------------------------------------------------
// <copyright file="EvaluatorTests.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Evaluator tests.
    /// </summary>
    [TestClass]
    public sealed class EvaluatorTests
    {
        /// <summary>
        /// Empty fail tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(RaspyEvaluationException))]
        public void EvaluatorEmptyFail()
        {
            Evaluator evaluator = new Evaluator();
            evaluator.Evaluate(new ExpressionQueue(string.Empty));
        }

        /// <summary>
        /// Invalid fail tests.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(RaspyEvaluationException))]
        public void EvaluatorInvalidFail()
        {
            Evaluator evaluator = new Evaluator();

            ExpressionQueue expression = new ExpressionQueue("4 + 5 +");
            expression.Enqueue(new Operand(4));
            expression.Enqueue(new Operand(5));
            expression.Enqueue(new RaspyOperator('+', Associativity.Left, 2, 2));
            expression.Enqueue(new RaspyOperator('+', Associativity.Left, 2, 2));

            evaluator.Evaluate(expression);
        }

        /// <summary>
        /// Evaluate tests.
        /// </summary>
        [TestMethod]
        public void EvaluatorEvaluate()
        {
            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            string expr = "3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3";

            object result = evaluator.Evaluate(parser.Parse(expr));
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(double));
            Assert.IsTrue((double)result > 3);
            Assert.IsTrue((double)result < 3.01);
        }
    }
}
