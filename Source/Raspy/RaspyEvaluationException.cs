//-----------------------------------------------------------------------------------------
// <copyright file="RaspyEvaluationException.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents an exception thrown during a Raspy operation.
    /// </summary>
    [Serializable]
    public sealed class RaspyEvaluationException : RaspyException
    {
        private string infixExpression, parsedExpression;

        /// <summary>
        /// Initializes a new instance of the RaspyEvaluationException class.
        /// </summary>
        public RaspyEvaluationException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RaspyEvaluationException class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public RaspyEvaluationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RaspyEvaluationException class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public RaspyEvaluationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RaspyEvaluationException class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="infixExpression">The infix expression that caused the exception to be thrown.</param>
        /// <param name="parsedExpression">The parsed expression that caused the exception to be thrown.</param>
        public RaspyEvaluationException(string message, Exception innerException, string infixExpression, string parsedExpression)
            : this(message, innerException)
        {
            this.infixExpression = infixExpression;
            this.parsedExpression = parsedExpression;
        }

        /// <summary>
        /// Initializes a new instance of the RaspyEvaluationException class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="infixExpression">The infix expression that caused the exception to be thrown.</param>
        /// <param name="parsedExpression">The parsed expression that caused the exception to be thrown.</param>
        public RaspyEvaluationException(string message, string infixExpression, string parsedExpression)
            : base(message)
        {
            this.infixExpression = infixExpression;
            this.parsedExpression = parsedExpression;
        }

        /// <summary>
        /// Initializes a new instance of the RaspyEvaluationException class.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        private RaspyEvaluationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.infixExpression = info.GetString("infixExpression");
            this.parsedExpression = info.GetString("parsedExpression");
        }

        /// <summary>
        /// Gets the infix expression that caused this exception to be thrown.
        /// </summary>
        public string InfixExpression
        {
            get { return this.infixExpression; }
        }

        /// <summary>
        /// Gets the parsed expression that caused this exception to be thrown.
        /// </summary>
        public string ParsedExpression
        {
            get { return this.parsedExpression; }
        }

        /// <summary>
        /// Sets the SerializationInfo with information about the exception.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("infixExpression", this.infixExpression);
            info.AddValue("parsedExpression", this.parsedExpression);
        }
    }
}