using System.Text.Json.Serialization;

namespace Automation.ControlCenter.Client.DTOs;

public record StartProcessResponse(
    [property: JsonPropertyName("processId")] Guid ProcessId,
    [property: JsonPropertyName("status")] string Status);
