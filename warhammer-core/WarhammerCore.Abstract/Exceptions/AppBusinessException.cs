using System;

namespace WarhammerCore.Abstract.Exceptions
{
    /// <summary>
    /// Exception raised by the Business part of the application.
    /// </summary>
    public class AppBusinessException : Exception
    {
        private const string DefaultErrorCode = "OperationCancelled";

        public string ErrorCode { get; }
        public AppBusinessException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public AppBusinessException(string message) : this(message, DefaultErrorCode)
        {

        }
    }
}
