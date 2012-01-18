//-----------------------------------------------------------------------------------------
// <copyright file="Operator.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;

    /// <summary>
    /// Represents an operator token.
    /// </summary>
    public sealed class Operator : Token
    {
        /// <summary>
        /// Initializes a new instance of the Operator class.
        /// </summary>
        /// <param name="symbol">The representing the operator.</param>
        /// <param name="associativity">The operator's associativity.</param>
        /// <param name="precedence">The operator's precedence.</param>
        /// <param name="argumentCount">The operator's argument count.</param>
        public Operator(char symbol, Associativity associativity, int precedence, int argumentCount)
        {
            this.Symbol = symbol;
            this.Associativity = associativity;
            this.Precedence = precedence;
            this.ArgumentCount = argumentCount;
        }

        /// <summary>
        /// Gets the number of arguments the operator expects.
        /// </summary>
        public int ArgumentCount { get; private set; }

        /// <summary>
        /// Gets the operator's associativity.
        /// </summary>
        public Associativity Associativity { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance represents an operator.
        /// </summary>
        public override bool IsOperator
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance represents a parenthesis.
        /// </summary>
        public override bool IsParenthesis
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the operator's precedence.
        /// </summary>
        public int Precedence { get; private set; }

        /// <summary>
        /// Gets the operator's symbol.
        /// </summary>
        public char Symbol { get; private set; }
    }
}
