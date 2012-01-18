//-----------------------------------------------------------------------------------------
// <copyright file="Associativity.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;

    /// <summary>
    /// Defines possible operator associativities.
    /// </summary>
    public enum Associativity
    {
        /// <summary>
        /// Identifies left-to-right associativity.
        /// </summary>
        LeftToRight,

        /// <summary>
        /// Identifies right-to-left associativity.
        /// </summary>
        RightToLeft
    }
}