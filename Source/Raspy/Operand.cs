//-----------------------------------------------------------------------------------------
// <copyright file="Operand.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Represents an operand token.
    /// </summary>
    public sealed class Operand : Token, IEquatable<Operand>
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

        /// <summary>
        /// Determines whether the specified Object is equal to the current Object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object. </param>
        /// <returns>True if the current object is equal to the given object, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Operand);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>True if the current object is equal to the given object, false otherwise.</returns>
        public bool Equals(Operand other)
        {
            if (other != null)
            {
                if (this.IsFloat && other.IsFloat)
                {
                    return Convert.ToDouble(this.Value, CultureInfo.InvariantCulture) == Convert.ToDouble(other.Value, CultureInfo.InvariantCulture);
                }
                else if (!this.IsFloat && !other.IsFloat)
                {
                    return Convert.ToInt64(this.Value, CultureInfo.InvariantCulture) == Convert.ToInt64(other.Value, CultureInfo.InvariantCulture);
                }
            }

            return false;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current Object.</returns>
        public override int GetHashCode()
        {
            return this.Value != null ? this.Value.GetHashCode() : 0;
        }
    }
}
