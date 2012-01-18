//-----------------------------------------------------------------------------------------
// <copyright file="FloatOperand.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;

    /// <summary>
    /// Represents a floating point operand token.
    /// </summary>
    public sealed class FloatOperand : Operand<double>
    {
        /// <summary>
        /// Initializes a new instance of the FloatOperand class.
        /// </summary>
        /// <param name="value">The value of the operand.</param>
        public FloatOperand(double value)
            : base(value)
        {
        }
    }
}