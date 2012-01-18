//-----------------------------------------------------------------------------------------
// <copyright file="RaspyException.cs" company="Tasty Codes">
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
    public abstract class RaspyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the RaspyException class.
        /// </summary>
        public RaspyException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RaspyException class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public RaspyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RaspyException class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public RaspyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RaspyException class.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected RaspyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
