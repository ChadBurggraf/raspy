//-----------------------------------------------------------------------------------------
// <copyright file="RaspyParseException.cs" company="Tasty Codes">
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
    public sealed class RaspyParseException : RaspyException
    {
        private string expression;

        /// <summary>
        /// Initializes a new instance of the RaspyParseException class.
        /// </summary>
        public RaspyParseException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RaspyParseException class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public RaspyParseException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RaspyParseException class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="expression">The expression that caused this exception to be thrown.</param>
        public RaspyParseException(string message, string expression)
            : base(message)
        {
            this.expression = expression;
        }

        /// <summary>
        /// Initializes a new instance of the RaspyParseException class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public RaspyParseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RaspyParseException class.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        private RaspyParseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.expression = info.GetString("expression");
        }

        /// <summary>
        /// Gets or sets the expression that caused this exception to be thrown.
        /// </summary>
        public string Expression
        {
            get { return this.expression; }
            set { this.expression = value; }
        }

        /// <summary>
        /// Sets the SerializationInfo with information about the exception.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("expression", this.expression);
        }
    }
}