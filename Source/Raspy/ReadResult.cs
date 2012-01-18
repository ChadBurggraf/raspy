//-----------------------------------------------------------------------------------------
// <copyright file="ReadResult.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;

    /// <summary>
    /// Represents the result of a parser read attempt.
    /// </summary>
    internal sealed class ReadResult
    {
        /// <summary>
        /// Gets or sets the position in the input string after the read.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the read was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the token that was read, if the read was successful.
        /// </summary>
        public Token Token { get; set; }
    }
}
