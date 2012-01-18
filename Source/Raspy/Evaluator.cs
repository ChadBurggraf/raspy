//-----------------------------------------------------------------------------------------
// <copyright file="Evaluator.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Provides evaluation services on parsed arithmetic expressions.
    /// </summary>
    public sealed class Evaluator
    {
        private OperationProviderFactory providerFactory;

        /// <summary>
        /// Initializes a new instance of the Evaluator class.
        /// </summary>
        public Evaluator()
        {
            this.providerFactory = OperationProviderFactory.DefaultInstance;
        }

        /// <summary>
        /// Initializes a new instance of the Evaluator class.
        /// </summary>
        /// <param name="providerFactory">The <see cref="OperationProviderFactory"/> to use when creating <see cref="IOperationProvider"/>s.</param>
        public Evaluator(OperationProviderFactory providerFactory)
        {
            if (providerFactory == null)
            {
                throw new ArgumentNullException("providerFactory", "providerFactory cannot be null.");
            }

            this.providerFactory = providerFactory;
        }

        /// <summary>
        /// Evaluates a parsed arithmetic expression.
        /// </summary>
        /// <param name="expression">The expression to evaluate.</param>
        /// <returns>The result of the evaluation.</returns>
        public object Evaluate(ExpressionQueue expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression", "expression cannot be null.");
            }

            string infixExpression = expression.InfixExpression;
            string parsedExpression = expression.ToString();
            Stack<Token> stack = new Stack<Token>();

            try
            {
                while (expression.Count > 0)
                {
                    Token token = expression.Dequeue();

                    if (token.IsOperator)
                    {
                        RaspyOperator op = (RaspyOperator)token;
                        IOperationProvider provider = this.providerFactory.GetProvider(op.Symbol);

                        if (provider != null)
                        {
                            List<Token> args = new List<Token>();
                            int i = op.ArgumentCount;

                            while (i-- > 0)
                            {
                                args.Add(stack.Pop());
                            }

                            args.Reverse();
                            stack.Push(provider.Operate(op, args.ToArray()));
                        }
                        else
                        {
                            throw new RaspyEvaluationException(
                                string.Format(CultureInfo.InvariantCulture, "No provider could be found for operator '{0}'.", op.Symbol), 
                                infixExpression, 
                                parsedExpression);
                        }
                    }
                    else
                    {
                        stack.Push(token);
                    }
                }

                if (stack.Count != 1 || !stack.Peek().IsOperand)
                {
                    throw new RaspyEvaluationException(
                        string.Format(CultureInfo.InvariantCulture, "Invalid expression: '{0}'.", parsedExpression), 
                        infixExpression,
                        parsedExpression);
                }

                return ((Operand)stack.Pop()).Value;
            }
            catch (RaspyException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new RaspyEvaluationException(
                    string.Format(CultureInfo.InvariantCulture, "An error occurred while evaluating the expression '{0}'.", parsedExpression),
                    ex,
                    infixExpression,
                    parsedExpression);
            }
        }
    }
}
