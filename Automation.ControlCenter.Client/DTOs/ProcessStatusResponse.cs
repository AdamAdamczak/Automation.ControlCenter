using System.Text.Json.Serialization;

namespace Automation.ControlCenter.Client.DTOs;

public record ProcessStatusResponse(
    [property: JsonPropertyName("processId")] Guid ProcessId,
    [property: JsonPropertyName("status")] string Status);
