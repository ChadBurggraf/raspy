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
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        private RaspyEvaluationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}