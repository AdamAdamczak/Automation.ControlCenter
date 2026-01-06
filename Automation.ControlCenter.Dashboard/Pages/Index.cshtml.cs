using Automation.ControlCenter.Dashboard.DTOs.Dashboard;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

public class IndexModel : PageModel
{
    private readonly HttpClient _http;

    public DashboardOverviewDto Overview { get; private set; } = new();

    public IndexModel(HttpClient http)
    {
        _http = http;
    }

    public async Task OnGet()
    {
        Overview = await _http.GetFromJsonAsync<DashboardOverviewDto>(
            "/api/dashboard/overview")
            ?? new DashboardOverviewDto();
    }
}
