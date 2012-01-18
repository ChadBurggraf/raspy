//-----------------------------------------------------------------------------------------
// <copyright file="Associativity.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines possible operator associativities.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "The spelling is correct.")]
    public enum Associativity
    {
        /// <summary>
        /// Identifies left associativity.
        /// </summary>
        Left,

        /// <summary>
        /// Identifies right associativity.
        /// </summary>
        Right
    }
}