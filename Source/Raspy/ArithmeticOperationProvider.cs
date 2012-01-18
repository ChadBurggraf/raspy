//-----------------------------------------------------------------------------------------
// <copyright file="ArithmeticOperationProvider.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Implements <see cref="IOperationProvider"/> for basic arithmetic.
    /// </summary>
    public sealed class ArithmeticOperationProvider : IOperationProvider
    {
        internal Token Add(Token[] args)
        {
            //Token left 
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a value indicating whether this provider can create
        /// an operator for the given symbol.
        /// </summary>
        /// <param name="symbol">The symbol to create the operator for.</param>
        /// <returns>True if this provider can create the operator, false otherwise.</returns>
        public bool CanCreateOperator(char symbol)
        {
            switch (symbol)
            {
                case '!':
                case '*':
                case '/':
                case '%':
                case '+':
                case '-':
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Creates an operator for the given symbol.
        /// </summary>
        /// <param name="symbol">The symbol to create the operator for.</param>
        /// <returns>An operator.</returns>
        public Operator CreateOperator(char symbol)
        {
            switch (symbol)
            {
                case '!':
                    return new Operator(symbol, Associativity.RightToLeft, 1, 1);
                case '*':
                case '/':
                case '%':
                    return new Operator(symbol, Associativity.LeftToRight, 2, 2);
                case '+':
                case '-':
                    return new Operator(symbol, Associativity.LeftToRight, 3, 2);
                default:
                    return null;
            }
        }

        internal Token Divide(Token[] args)
        {
            throw new NotImplementedException();
        }

        internal Token Factorial(Token[] args)
        {
            throw new NotImplementedException();
        }

        internal Token Modulo(Token[] args)
        {
            throw new NotImplementedException();
        }

        internal Token Multiply(Token[] args)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Executes an operator with a set of arguments.
        /// </summary>
        /// <param name="op">The operator to execute.</param>
        /// <param name="args">The arguments to use when executing the operator.</param>
        /// <returns>The result of the operation.</returns>
        public Token Operate(Operator op, Token[] args)
        {
            if (op == null)
            {
                throw new ArgumentNullException("op", "op cannot be null.");
            }

            if (args == null)
            {
                throw new ArgumentNullException("args", "args cannot be null.");
            }

            if (args.Length != op.ArgumentCount)
            {
                throw new ArgumentException("The argument length must match the number of arguments expected by the operator.", "args");
            }

            switch (op.Symbol)
            {
                case '!':
                    return this.Factorial(args);
                case '*':
                    return this.Multiply(args);
                case '/':
                    return this.Divide(args);
                case '%':
                    return this.Modulo(args);
                case '+':
                    return this.Add(args);
                case '-':
                    return this.Subtract(args);
                default:
                    throw new ArgumentException("op", string.Format(CultureInfo.InvariantCulture, "The operator '{0}' is not implemented by this provider.", op.Symbol));
            }
        }

        internal Token Subtract(Token[] args)
        {
            throw new NotImplementedException();
        }
    }
}
