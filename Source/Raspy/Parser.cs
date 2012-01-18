//-----------------------------------------------------------------------------------------
// <copyright file="Parser.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Provides parsing services for infix arithmetic expressions.
    /// </summary>
    public sealed class Parser
    {
        private Dictionary<char, IOperationProvider> providerCache = new Dictionary<char, IOperationProvider>();
        private IOperationProvider[] providers;

        /// <summary>
        /// Initializes a new instance of the Parser class.
        /// </summary>
        public Parser()
        {
            this.providers = new IOperationProvider[]
            {
                new ArithmeticOperationProvider()
            };
        }

        /// <summary>
        /// Initializes a new instance of the Parser class.
        /// </summary>
        /// <param name="providers">The custom <see cref="IOperationProvider"/> collection to use.</param>
        public Parser(IEnumerable<IOperationProvider> providers)
        {
            this.providers = (providers ?? new IOperationProvider[0]).ToArray();

            if (this.providers.Length == 0)
            {
                throw new ArgumentException("The providers collection must contain at least one IOperationProvider implementation.", "providers");
            }
        }

        /// <summary>
        /// Parses an infix expression into a an RPN token queue.
        /// </summary>
        /// <param name="expression">The expression to parse.</param>
        /// <returns>An RPN token queue.</returns>
        public Queue<Token> Parse(string expression)
        {
            Queue<Token> output = new Queue<Token>();
            Stack<Token> stack = new Stack<Token>();

            if (!string.IsNullOrEmpty(expression))
            {
                int i = 0;
                ReadResult result;

                while (i < expression.Length)
                {
                    result = this.ReadOperand(expression, i);

                    if (result.Success)
                    {
                        output.Enqueue(result.Token);
                    }
                    else
                    {
                        result = this.ReadOperator(expression, i);

                        if (result.Success)
                        {
                            Operator op1 = (Operator)result.Token;

                            while (stack.Count > 0 && stack.Peek().IsOperator)
                            {
                                Operator op2 = (Operator)stack.Peek();

                                if ((op1.Associativity == Associativity.LeftToRight && op1.Precedence <= op2.Precedence)
                                    || (op1.Associativity == Associativity.RightToLeft && op1.Precedence < op2.Precedence))
                                {
                                    output.Enqueue(stack.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }

                            stack.Push(result.Token);
                        }
                        else if (expression[i] == '(')
                        {
                            stack.Push(new Parenthesis(ParenthesisType.Left));
                        }
                        else if (expression[i] == ')')
                        {
                            bool foundLeft = false;

                            while (stack.Count > 0)
                            {
                                Token token = stack.Peek();

                                if (token.IsOperator)
                                {
                                    output.Enqueue(stack.Pop());
                                }
                                else if (token.IsParenthesis)
                                {
                                    if (((Parenthesis)token).ParenthesisType == ParenthesisType.Left)
                                    {
                                        stack.Pop();
                                        foundLeft = true;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }

                            if (!foundLeft)
                            {
                                throw new RaspyParseException("The expression contains mismatched parentheses.", expression);
                            }
                        }
                        else if (!char.IsWhiteSpace(expression[i]))
                        {
                            throw new RaspyParseException(string.Format(CultureInfo.InvariantCulture, "The expression contains an invalid character, '{0}'.", expression[i]), expression);
                        }
                    }
                }

                while (stack.Count > 0)
                {
                    Token token = stack.Peek();

                    if (!token.IsParenthesis)
                    {
                        output.Enqueue(stack.Pop());
                    }
                    else
                    {
                        throw new RaspyParseException("The expression contains mismatched parentheses.", expression);
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Reads an operand from the given string expression, starting at the given position.
        /// </summary>
        /// <param name="expr">The string expression to read the operand from.</param>
        /// <param name="pos">The position to start reading at.</param>
        /// <returns>The result of the read.</returns>
        internal ReadResult ReadOperand(string expr, int pos)
        {
            ReadResult result = new ReadResult() { Position = pos };
            StringBuilder sb = new StringBuilder();
            bool hasPeriod = false;
            char c;

            do
            {
                c = expr[pos];

                if (char.IsDigit(c))
                {
                    sb.Append(c);
                    pos++;
                }
                else if (c == '.')
                {
                    if (!hasPeriod)
                    {
                        sb.Append(c);
                        pos++;
                        hasPeriod = true;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            while (pos < expr.Length);

            if (sb.Length > 0)
            {
                try
                {
                    if (hasPeriod)
                    {
                        result.Token = new Operand(double.Parse(sb.ToString(), CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        result.Token = new Operand(long.Parse(sb.ToString(), CultureInfo.InvariantCulture));
                    }

                    result.Position = pos;
                    result.Success = true;
                }
                catch (FormatException)
                {
                }
                catch (OverflowException)
                {
                }
            }

            return result;
        }

        /// <summary>
        /// Reads an operator from the given string expression, starting at the given position.
        /// </summary>
        /// <param name="expr">The expression to read the operator from.</param>
        /// <param name="pos">The position to start reading at.</param>
        /// <returns>The result of the read.</returns>
        internal ReadResult ReadOperator(string expr, int pos)
        {
            ReadResult result = new ReadResult() { Position = pos };
            char c = expr[pos];

            if (!char.IsWhiteSpace(c))
            {
                IOperationProvider provider = this.GetProvider(c);

                if (provider != null)
                {
                    result.Token = provider.CreateOperator(c);
                    result.Position = pos + 1;
                    result.Success = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the <see cref="IOperationProvider"/> to use when creating the operator for the given symbol.
        /// </summary>
        /// <param name="symbol">The symbol to get the <see cref="IOperationProvider"/> for.</param>
        /// <returns>An <see cref="IOperationProvider"/>, or null if none was found for the given symbol.</returns>
        private IOperationProvider GetProvider(char symbol)
        {
            IOperationProvider provider = null;
            bool containsKey = false;

            lock (this.providerCache)
            {
                if (this.providerCache.ContainsKey(symbol))
                {
                    provider = this.providerCache[symbol];
                    containsKey = true;
                }
                else
                {
                    containsKey = false;
                }
            }

            if (!containsKey)
            {
                provider = (from p in this.providers
                            where p.CanCreateOperator(symbol)
                            select p).FirstOrDefault();

                lock (this.providerCache)
                {
                    this.providerCache[symbol] = provider;
                }
            }

            return provider;
        }
    }
}
