using Automation.ControlCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
// Resolve database path from configuration
var connectionString = builder.Configuration.GetConnectionString("Default");
// Resolve solution root directory (works regardless of bin/debug structure)
var solutionRoot = Directory.GetParent(AppContext.BaseDirectory)!
    .Parent!
    .Parent!
    .Parent!
    .Parent!.FullName;

// Build absolute path to shared database
var fullDbPath = Path.Combine(
    solutionRoot,
    "Data",
    "processes.db");

// Ensure that the Data directory exists
var dataDirectory = Path.GetDirectoryName(fullDbPath)!;
if (!Directory.Exists(dataDirectory))
{
    Directory.CreateDirectory(dataDirectory);
}

// Dashboard is read-only – database must already exist
if (!File.Exists(fullDbPath))
{
    throw new InvalidOperationException(
        $"Database file not found at path: {fullDbPath}");
}

builder.Services.AddDbContext<ProcessDbContext>(options =>
    options.UseSqlite($"Data Source={fullDbPath}"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
