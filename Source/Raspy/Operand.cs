//-----------------------------------------------------------------------------------------
// <copyright file="Operand.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;

    /// <summary>
    /// Represents an operand token.
    /// </summary>
    public sealed class Operand : Token
    {
        /// <summary>
        /// Initializes a new instance of the Operand class.
        /// </summary>
        /// <param name="value">The value to initialize this instance with.</param>
        public Operand(double value)
        {
            this.Value = value;
            this.IsFloat = true;
        }

        /// <summary>
        /// Initializes a new instance of the Operand class.
        /// </summary>
        /// <param name="value">The value to initialize this instance with.</param>
        public Operand(long value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets a value indicating whether this operand
        /// represents a floating-point value.
        /// </summary>
        public bool IsFloat { get; private set; }

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
        public object Value { get; private set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            if (this.Value != null)
            {
                return this.Value.ToString();
            }

            return base.ToString();
        }
    }
}
