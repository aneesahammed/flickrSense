/*
 * @file:ConfigNullException
 * @brief: Exception for null Config.
 * @credit:https://github.com/Microsoft/UWPCommunityToolkit 
 */

using System;

namespace Microsoft.Toolkit.Uwp.Services.Exceptions
{
    /// <summary>
    /// Exception for null Config.
    /// </summary>
    public class ConfigNullException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigNullException"/> class.
        /// Default constructor.
        /// </summary>
        public ConfigNullException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigNullException"/> class.
        /// Constructor accepting additional message string.
        /// </summary>
        /// <param name="message">Additional error information.</param>
        public ConfigNullException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigNullException"/> class.
        /// Constructor accepting additonal message string and inner exception
        /// </summary>
        /// <param name="message">Additional error information.</param>
        /// <param name="innerException">Reference to inner exception.</param>
        public ConfigNullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}