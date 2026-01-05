using System.Text.Json.Serialization;

namespace Automation.ControlCenter.Client.DTOs;

public record ErrorResponse(
    [property: JsonPropertyName("errorCode")] string ErrorCode,
    [property: JsonPropertyName("message")] string Message);
