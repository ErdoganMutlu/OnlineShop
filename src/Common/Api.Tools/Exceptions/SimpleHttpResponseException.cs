using System.Net;

namespace Api.Tools.Exceptions;

public class SimpleHttpResponseException : Exception
{
    private HttpStatusCode StatusCode { get; set; }

    public SimpleHttpResponseException(HttpStatusCode statusCode, string content) : base(content)
    {
        StatusCode = statusCode;
    }
}