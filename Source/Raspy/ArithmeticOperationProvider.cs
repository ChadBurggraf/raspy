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
                case '^':
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
        public RaspyOperator CreateOperator(char symbol)
        {
            switch (symbol)
            {
                case '!':
                    return new RaspyOperator(symbol, Associativity.Right, 4, 1);
                case '^':
                    return new RaspyOperator(symbol, Associativity.Right, 4, 2);
                case '*':
                case '/':
                case '%':
                    return new RaspyOperator(symbol, Associativity.Left, 3, 2);
                case '+':
                case '-':
                    return new RaspyOperator(symbol, Associativity.Left, 2, 2);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Executes an operator with a set of arguments.
        /// </summary>
        /// <param name="op">The operator to execute.</param>
        /// <param name="args">The arguments to use when executing the operator.</param>
        /// <returns>The result of the operation.</returns>
        public Token Operate(RaspyOperator op, Token[] args)
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
                    return Factorial(args);
                case '^':
                    return Power(args);
                case '*':
                    return Multiply(args);
                case '/':
                    return Divide(args);
                case '%':
                    return Modulo(args);
                case '+':
                    return Add(args);
                case '-':
                    return Subtract(args);
                default:
                    throw new ArgumentException("op", string.Format(CultureInfo.InvariantCulture, "The operator '{0}' is not implemented by this provider.", op.Symbol));
            }
        }

        /// <summary>
        /// Implements the add operation.
        /// </summary>
        /// <param name="args">The operand arguments.</param>
        /// <returns>The result of the operation.</returns>
        internal static Token Add(Token[] args)
        {
            Operand left = args[0] as Operand;
            Operand right = args[1] as Operand;

            if (left.IsFloat)
            {
                if (right.IsFloat)
                {
                    return new Operand(Convert.ToDouble(left.Value, CultureInfo.InvariantCulture) + Convert.ToDouble(right.Value, CultureInfo.InvariantCulture));
                }
                else
                {
                    return new Operand(Convert.ToDouble(left.Value, CultureInfo.InvariantCulture) + Convert.ToInt64(right.Value, CultureInfo.InvariantCulture));
                }
            }
            else
            {
                if (right.IsFloat)
                {
                    return new Operand(Convert.ToInt64(left.Value, CultureInfo.InvariantCulture) + Convert.ToDouble(right.Value, CultureInfo.InvariantCulture));
                }
                else
                {
                    return new Operand(Convert.ToInt64(left.Value, CultureInfo.InvariantCulture) + Convert.ToInt64(right.Value, CultureInfo.InvariantCulture));
                }
            }
        }

        /// <summary>
        /// Implements the divide operation.
        /// </summary>
        /// <param name="args">The operand arguments.</param>
        /// <returns>The result of the operation.</returns>
        internal static Token Divide(Token[] args)
        {
            double left = Convert.ToDouble(((Operand)args[0]).Value, CultureInfo.InvariantCulture);
            double right = Convert.ToDouble(((Operand)args[1]).Value, CultureInfo.InvariantCulture);

            return new Operand(left / right);
        }

        /// <summary>
        /// Implements the factorial operation.
        /// </summary>
        /// <param name="args">The operand arguments.</param>
        /// <returns>The result of the operation.</returns>
        internal static Token Factorial(Token[] args)
        {
            long f = 1;
            long n = Convert.ToInt64(((Operand)args[0]).Value, CultureInfo.InvariantCulture);

            for (long i = 1; i <= n; i++)
            {
                f *= i;
            }

            return new Operand(f);
        }

        /// <summary>
        /// Implements the modulo operation.
        /// </summary>
        /// <param name="args">The operand arguments.</param>
        /// <returns>The result of the operation.</returns>
        internal static Token Modulo(Token[] args)
        {
            Operand left = args[0] as Operand;
            Operand right = args[1] as Operand;

            if (left.IsFloat)
            {
                if (right.IsFloat)
                {
                    return new Operand(Convert.ToDouble(left.Value, CultureInfo.InvariantCulture) % Convert.ToDouble(right.Value, CultureInfo.InvariantCulture));
                }
                else
                {
                    return new Operand(Convert.ToDouble(left.Value, CultureInfo.InvariantCulture) % Convert.ToInt64(right.Value, CultureInfo.InvariantCulture));
                }
            }
            else
            {
                if (right.IsFloat)
                {
                    return new Operand(Convert.ToInt64(left.Value, CultureInfo.InvariantCulture) % Convert.ToDouble(right.Value, CultureInfo.InvariantCulture));
                }
                else
                {
                    return new Operand(Convert.ToInt64(left.Value, CultureInfo.InvariantCulture) % Convert.ToInt64(right.Value, CultureInfo.InvariantCulture));
                }
            }
        }

        /// <summary>
        /// Implements the multiply operation.
        /// </summary>
        /// <param name="args">The operand arguments.</param>
        /// <returns>The result of the operation.</returns>
        internal static Token Multiply(Token[] args)
        {
            Operand left = args[0] as Operand;
            Operand right = args[1] as Operand;

            if (left.IsFloat)
            {
                if (right.IsFloat)
                {
                    return new Operand(Convert.ToDouble(left.Value, CultureInfo.InvariantCulture) * Convert.ToDouble(right.Value, CultureInfo.InvariantCulture));
                }
                else
                {
                    return new Operand(Convert.ToDouble(left.Value, CultureInfo.InvariantCulture) * Convert.ToInt64(right.Value, CultureInfo.InvariantCulture));
                }
            }
            else
            {
                if (right.IsFloat)
                {
                    return new Operand(Convert.ToInt64(left.Value, CultureInfo.InvariantCulture) * Convert.ToDouble(right.Value, CultureInfo.InvariantCulture));
                }
                else
                {
                    return new Operand(Convert.ToInt64(left.Value, CultureInfo.InvariantCulture) * Convert.ToInt64(right.Value, CultureInfo.InvariantCulture));
                }
            }
        }

        /// <summary>
        /// Implements the power operation.
        /// </summary>
        /// <param name="args">The operand arguments.</param>
        /// <returns>The result of the operation.</returns>
        internal static Token Power(Token[] args)
        {
            Operand left = args[0] as Operand;
            Operand right = args[1] as Operand;
            bool integer = !left.IsFloat && !right.IsFloat;

            double result = Math.Pow(
                Convert.ToDouble(left.Value, CultureInfo.InvariantCulture),
                Convert.ToDouble(right.Value, CultureInfo.InvariantCulture));

            return integer ? new Operand((long)result) : new Operand(result);
        }

        /// <summary>
        /// Implements the subtract operation.
        /// </summary>
        /// <param name="args">The operand arguments.</param>
        /// <returns>The result of the operation.</returns>
        internal static Token Subtract(Token[] args)
        {
            Operand left = args[0] as Operand;
            Operand right = args[1] as Operand;

            if (left.IsFloat)
            {
                if (right.IsFloat)
                {
                    return new Operand(Convert.ToDouble(left.Value, CultureInfo.InvariantCulture) - Convert.ToDouble(right.Value, CultureInfo.InvariantCulture));
                }
                else
                {
                    return new Operand(Convert.ToDouble(left.Value, CultureInfo.InvariantCulture) - Convert.ToInt64(right.Value, CultureInfo.InvariantCulture));
                }
            }
            else
            {
                if (right.IsFloat)
                {
                    return new Operand(Convert.ToInt64(left.Value, CultureInfo.InvariantCulture) - Convert.ToDouble(right.Value, CultureInfo.InvariantCulture));
                }
                else
                {
                    return new Operand(Convert.ToInt64(left.Value, CultureInfo.InvariantCulture) - Convert.ToInt64(right.Value, CultureInfo.InvariantCulture));
                }
            }
        }
    }
}
