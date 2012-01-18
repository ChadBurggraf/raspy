//-----------------------------------------------------------------------------------------
// <copyright file="Operand{T}.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;

    /// <summary>
    ///  Represents an operand token.
    /// </summary>
    /// <typeparam name="T">The type of value the operand represents.</typeparam>
    public abstract class Operand<T> : Token where T : struct
    {
        /// <summary>
        /// Initializes a new instance of the Operand class.
        /// </summary>
        /// <param name="value">The value of the operand.</param>
        protected Operand(T value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets a value indicating whether this instance represents an operator.
        /// </summary>
        public override bool IsOperator
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance represents a parenthesis.
        /// </summary>
        public override bool IsParenthesis
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the value of the operand.
        /// </summary>
        public T Value { get; private set; }
    }
}
