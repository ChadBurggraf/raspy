//-----------------------------------------------------------------------------------------
// <copyright file="IntegerOperand.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;

    /// <summary>
    /// Represents an integer operand token.
    /// </summary>
    public sealed class IntegerOperand : Operand<long>
    {
        /// <summary>
        /// Initializes a new instance of the IntegerOperand class.
        /// </summary>
        /// <param name="value">The value of the operand.</param>
        public IntegerOperand(long value)
            : base(value)
        {
        }
    }
}