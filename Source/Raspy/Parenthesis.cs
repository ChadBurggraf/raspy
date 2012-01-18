//-----------------------------------------------------------------------------------------
// <copyright file="Parenthesis.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;

    /// <summary>
    /// Represents a parenthesis token.
    /// </summary>
    public sealed class Parenthesis : Token
    {
        /// <summary>
        /// Initializes a new instance of the Parenthesis class.
        /// </summary>
        /// <param name="parenthesisType">The parenthesis type this instance represents.</param>
        public Parenthesis(ParenthesisType parenthesisType)
        {
            this.ParenthesisType = parenthesisType;
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
            get { return true; }
        }

        /// <summary>
        /// Gets the parenthesis type this instance represents.
        /// </summary>
        public ParenthesisType ParenthesisType { get; private set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            return this.ParenthesisType == Raspy.ParenthesisType.Left
                ? "("
                : ")";
        }
    }
}
