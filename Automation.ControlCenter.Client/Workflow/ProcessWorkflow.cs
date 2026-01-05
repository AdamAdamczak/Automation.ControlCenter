using Automation.ControlCenter.Client.Http;

namespace Automation.ControlCenter.Client.Workflow;

public class ProcessWorkflow
{
    private readonly TimeSpan _pollInterval = TimeSpan.FromSeconds(2);
    private readonly TimeSpan _timeout = TimeSpan.FromSeconds(30);
    private readonly ApiClient _apiClient;

    public ProcessWorkflow(
        ApiClient apiClient,
        TimeSpan pollInterval,
        TimeSpan timeout)
    {
        _apiClient = apiClient;
        _pollInterval = pollInterval;
        _timeout = timeout;
    }
    public async Task RunAsync()
    {
        try
        {
            await RunInternalAsync();
        }
        catch (ApiClientException ex)
        {
            HandleApiError(ex);
        }
    }

    private async Task RunInternalAsync()
    {
        Console.WriteLine("Starting process...");

        var startResult = await _apiClient.StartProcessAsync(
            "InvoiceBot",
            "ConsoleClient");

        Console.WriteLine($"Process started. Id={startResult.ProcessId}");

        var startTime = DateTime.UtcNow;

        while (true)
        {
            if (DateTime.UtcNow - startTime > _timeout)
            {
                Console.WriteLine("❌ Timeout reached.");
                break;
            }

            await Task.Delay(_pollInterval);

            var statusResult = await _apiClient.GetStatusAsync(
                startResult.ProcessId);

            Console.WriteLine($"Current status: {statusResult.Status}");

            if (statusResult.Status is "Completed" or "Failed")
                break;
        }
    }

    private void HandleApiError(ApiClientException ex)
    {
        Console.WriteLine($"❌ API error: {ex.Message}");

        if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            Console.WriteLine("➡️ API Key invalid or missing. Stop bot.");
            return;
        }

        if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("➡️ Process not found. Stop bot.");
            return;
        }

        if ((int)ex.StatusCode >= 500)
        {
            Console.WriteLine("➡️ Server error. Retry might be needed.");
            return;
        }

        Console.WriteLine("➡️ Unexpected error. Stop bot.");
    }


}
