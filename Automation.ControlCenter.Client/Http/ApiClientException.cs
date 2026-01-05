using System.Net;

namespace Automation.ControlCenter.Client.Http;

public class ApiClientException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public ApiClientException(HttpStatusCode statusCode, string message)
        : base(message)
    {
        StatusCode = statusCode;
    }
}
