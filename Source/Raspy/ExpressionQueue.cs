//-----------------------------------------------------------------------------------------
// <copyright file="ExpressionQueue.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents an expression parsed by a <see cref="Parser"/> instance.
    /// </summary>
    public sealed class ExpressionQueue : Queue<Token>
    {
        /// <summary>
        /// Initializes a new instance of the ExpressionQueue class.
        /// </summary>
        /// <param name="infixExpression">The original infix expression this instance is constructed from.</param>
        public ExpressionQueue(string infixExpression)
        {
            this.InfixExpression = infixExpression;
        }

        /// <summary>
        /// Gets the original infix expression this instance is constructed from.
        /// </summary>
        public string InfixExpression { get; private set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Token[] tokens = ToArray();

            for (int i = 0; i < tokens.Length; i++)
            {
                if (i > 0)
                {
                    sb.Append(" ");
                }

                sb.Append(tokens[i].ToString());
            }

            return sb.ToString();
        }
    }
}
