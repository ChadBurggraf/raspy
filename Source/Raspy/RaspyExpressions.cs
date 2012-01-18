//-----------------------------------------------------------------------------------------
// <copyright file="RaspyExpressions.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Provides static helpers and extensions for shorthand access to common Raspy operations.
    /// </summary>
    public static class RaspyExpressions
    {
        private static Evaluator evaluator = new Evaluator();
        private static Parser parser = new Parser();

        /// <summary>
        /// Parses the expression.
        /// </summary>
        /// <param name="expression">The expression to parse.</param>
        /// <returns>The parsed expression.</returns>
        public static ExpressionQueue Parse(this string expression)
        {
            return parser.Parse(expression);
        }

        /// <summary>
        /// Parses and evaluates the expression.
        /// </summary>
        /// <param name="expression">The expression to parse and evaluate.</param>
        /// <returns>The result of the evaluation.</returns>
        public static object ParseAndEvaluate(this string expression)
        {
            return evaluator.Evaluate(Parse(expression));
        }

        /// <summary>
        /// Tries to parse and evaluate the expression, returning true if successful
        /// and false otherwise.
        /// </summary>
        /// <param name="expression">The expression to parse and evaluate.</param>
        /// <param name="result">Contains the result of the evaluation upon completion.</param>
        /// <returns>True if the parse and evaluation was successful, false otherwise.</returns>
        public static bool TryParseAndEvaluate(this string expression, out object result)
        {
            result = null;

            try
            {
                result = Parse(expression);
                return true;
            }
            catch (RaspyException)
            {
            }

            return false;
        }

        /// <summary>
        /// Gets the expression string representation of the token stack.
        /// </summary>
        /// <param name="stack">The stack to get the expression string representation of.</param>
        /// <returns>An expression string.</returns>
        internal static string ToExpressionString(this Stack<Token> stack)
        {
            StringBuilder sb = new StringBuilder();

            if (stack != null)
            {
                Token[] tokens = stack.ToArray();

                for (int i = 0; i < tokens.Length; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(" ");
                    }

                    sb.Append(tokens[i].ToString());
                }
            }

            return sb.ToString();
        }
    }
}
