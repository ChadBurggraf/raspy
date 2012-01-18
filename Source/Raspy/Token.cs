//-----------------------------------------------------------------------------------------
// <copyright file="Token.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;

    /// <summary>
    /// Represents a parsed expression token.
    /// </summary>
    public abstract class Token
    {
        /// <summary>
        /// Gets a value indicating whether this instance represents an operator.
        /// </summary>
        public abstract bool IsOperator { get; }

        /// <summary>
        /// Gets a value indicating whether this instance represents a parenthesis.
        /// </summary>
        public abstract bool IsParenthesis { get; }
    }
}
