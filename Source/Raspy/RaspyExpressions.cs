//-----------------------------------------------------------------------------------------
// <copyright file="RaspyExpressions.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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
        /// Parses and evaluates the expression.
        /// </summary>
        /// <typeparam name="T">The type expected for the output of the evaluation (e.g., long, double, etc.).</typeparam>
        /// <param name="expression">The expression to parse and evaluate.</param>
        /// <returns>The result of the evaluation.</returns>
        public static T ParseAndEvaluate<T>(this string expression) where T : struct
        {
            object result = ParseAndEvaluate(expression);

            if (result != null)
            {
                return (T)Convert.ChangeType(result, typeof(T));
            }

            return default(T);
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
                result = evaluator.Evaluate(Parse(expression));
                return true;
            }
            catch (RaspyException)
            {
            }

            return false;
        }

        /// <summary>
        /// Tries to parse and evaluate the expression, returning true if successful
        /// and false otherwise.
        /// </summary>
        /// <typeparam name="T">The type expected for the output of the evaluation (e.g., long, double, etc.).</typeparam>
        /// <param name="expression">The expression to parse and evaluate.</param>
        /// <param name="result">Contains the result of the evaluation upon completion.</param>
        /// <returns>True if the parse and evaluation was successful, false otherwise.</returns>
        public static bool TryParseAndEvaluate<T>(this string expression, out T result)
        {
            result = default(T);

            object r;
            bool success = TryParseAndEvaluate(expression, out r);

            if (success && r != null)
            {
                success = false;

                try
                {
                    result = (T)Convert.ChangeType(r, typeof(T));
                    success = true;
                }
                catch (InvalidCastException)
                {
                }
                catch (FormatException)
                {
                }
                catch (OverflowException)
                {
                }
            }

            return success;
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
