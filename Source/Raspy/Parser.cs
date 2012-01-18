//-----------------------------------------------------------------------------------------
// <copyright file="Parser.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Provides parsing services for infix arithmetic expressions.
    /// </summary>
    public sealed class Parser
    {
        private OperationProviderFactory providerFactory;

        /// <summary>
        /// Initializes a new instance of the Parser class.
        /// </summary>
        public Parser()
        {
            this.providerFactory = OperationProviderFactory.DefaultInstance;
        }

        /// <summary>
        /// Initializes a new instance of the Parser class.
        /// </summary>
        /// <param name="providerFactory">The <see cref="OperationProviderFactory"/> to use when creating <see cref="IOperationProvider"/>s.</param>
        public Parser(OperationProviderFactory providerFactory)
        {
            if (providerFactory == null)
            {
                throw new ArgumentNullException("providerFactory", "providerFactory cannot be null.");
            }

            this.providerFactory = providerFactory;
        }

        /// <summary>
        /// Parses an infix expression into a an RPN token queue.
        /// </summary>
        /// <param name="expression">The expression to parse.</param>
        /// <returns>An RPN token queue.</returns>
        public ExpressionQueue Parse(string expression)
        {
            ExpressionQueue output = new ExpressionQueue(expression);
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
                        i = result.Position;
                    }
                    else
                    {
                        result = this.ReadOperator(expression, i);

                        if (result.Success)
                        {
                            RaspyOperator op1 = (RaspyOperator)result.Token;

                            while (stack.Count > 0 && stack.Peek().IsOperator)
                            {
                                RaspyOperator op2 = (RaspyOperator)stack.Peek();

                                if ((op1.Associativity == Associativity.Left && op1.Precedence <= op2.Precedence)
                                    || (op1.Associativity == Associativity.Right && op1.Precedence < op2.Precedence))
                                {
                                    output.Enqueue(stack.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }

                            stack.Push(result.Token);
                            i = result.Position;
                        }
                        else if (expression[i] == '(')
                        {
                            stack.Push(new Parenthesis(ParenthesisType.Left));
                            i++;
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
                                        break;
                                    }
                                }
                            }

                            i++;

                            if (!foundLeft)
                            {
                                throw new RaspyParseException("The expression contains mismatched parentheses.", expression);
                            }
                        }
                        else if (!char.IsWhiteSpace(expression[i]))
                        {
                            throw new RaspyParseException(string.Format(CultureInfo.InvariantCulture, "The expression contains an invalid character, '{0}'.", expression[i]), expression);
                        }
                        else
                        {
                            i++;
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
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Interested in consistent access semantics with ReadOperator.")]
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
                IOperationProvider provider = this.providerFactory.GetProvider(c);

                if (provider != null)
                {
                    result.Token = provider.CreateOperator(c);
                    result.Position = pos + 1;
                    result.Success = true;
                }
            }

            return result;
        }
    }
}
