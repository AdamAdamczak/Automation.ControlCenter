using System.Text;
using System.Text.Json;
using Automation.ControlCenter.Client.DTOs;

namespace Automation.ControlCenter.Client.Http;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<StartProcessResponse> StartProcessAsync(
        string processName,
        string triggeredBy)
    {
        var payload = new
        {
            processName,
            triggeredBy
        };

        var response = await _httpClient.PostAsync(
            "/api/process/start",
            new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"));

        await ThrowIfErrorAsync(response);

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<StartProcessResponse>(
            json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task<ProcessStatusResponse> GetStatusAsync(Guid processId)
    {
        var response = await _httpClient.GetAsync(
            $"/api/process/{processId}/status");

        await ThrowIfErrorAsync(response);

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ProcessStatusResponse>(
            json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task UpdateStatusAsync(Guid processId, string status)
    {
        var payload = new { status };

        var response = await _httpClient.PostAsync(
            $"/api/process/{processId}/status",
            new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"));

        await ThrowIfErrorAsync(response);
    }
    private static async Task ThrowIfErrorAsync(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return;

        var content = await response.Content.ReadAsStringAsync();

        ErrorResponse? error = null;
        try
        {
            error = JsonSerializer.Deserialize<ErrorResponse>(
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch
        {
            // ignore – fallback poniżej
        }

        var message = error != null
            ? $"{error.ErrorCode}: {error.Message}"
            : $"HTTP {(int)response.StatusCode}";

        throw new ApiClientException(
            response.StatusCode,
            message);
    }

}
