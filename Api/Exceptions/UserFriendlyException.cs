using System.Net;

namespace Api.Exceptions;

public class UserFriendlyException : Exception
{
    public HttpStatusCode? ErrorCode { get; }

    public UserFriendlyException(string message) : base(message)
    {
    }

    public UserFriendlyException(string message, HttpStatusCode errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }
}