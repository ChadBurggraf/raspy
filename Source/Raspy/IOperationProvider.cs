//-----------------------------------------------------------------------------------------
// <copyright file="IOperationProvider.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;

    /// <summary>
    /// Defines the interface for operation providers.
    /// </summary>
    public interface IOperationProvider
    {
        /// <summary>
        /// Gets a value indicating whether this provider can create
        /// an operator for the given symbol.
        /// </summary>
        /// <param name="symbol">The symbol to create the operator for.</param>
        /// <returns>True if this provider can create the operator, false otherwise.</returns>
        bool CanCreateOperator(char symbol);

        /// <summary>
        /// Creates an operator for the given symbol.
        /// </summary>
        /// <param name="symbol">The symbol to create the operator for.</param>
        /// <returns>An operator.</returns>
        RaspyOperator CreateOperator(char symbol);

        /// <summary>
        /// Executes an operator with a set of arguments.
        /// </summary>
        /// <param name="op">The operator to execute.</param>
        /// <param name="args">The arguments to use when executing the operator.</param>
        /// <returns>The result of the operation.</returns>
        Token Operate(RaspyOperator op, Token[] args);
    }
}
