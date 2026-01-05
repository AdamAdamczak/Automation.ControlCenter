using Automation.ControlCenter.Client.Configuration;
using Automation.ControlCenter.Client.Http;
using Automation.ControlCenter.Client.Workflow;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var apiOptions = configuration
    .GetSection("Api")
    .Get<ApiOptions>()!;

var workflowOptions = configuration
    .GetSection("Workflow")
    .Get<WorkflowOptions>()!;

using var httpClient = new HttpClient
{
    BaseAddress = new Uri(apiOptions.BaseUrl)
};

httpClient.DefaultRequestHeaders.Add(
    "X-API-KEY",
    apiOptions.ApiKey);

httpClient.DefaultRequestHeaders.Add(
    "X-Correlation-Id",
    Guid.NewGuid().ToString());

var apiClient = new ApiClient(httpClient);

var workflow = new ProcessWorkflow(
    apiClient,
    TimeSpan.FromSeconds(workflowOptions.PollIntervalSeconds),
    TimeSpan.FromSeconds(workflowOptions.TimeoutSeconds));

await workflow.RunAsync();
