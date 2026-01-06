using Automation.ControlCenter.Domain;
using Automation.ControlCenter.Infrastructure;
using Automation.ControlCenter.Infrastructure.Middleware;
using Automation.ControlCenter.Infrastructure.Persistence;
using Automation.ControlCenter.Services.Implementations;
using Automation.ControlCenter.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IProcessService, ProcessService>();
builder.Services.AddScoped<ProcessStateService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProcessRepository, ProcessRepository>();

// Resolve database path and ensure data directory exists
var connectionString = builder.Configuration.GetConnectionString("Default");
var dbPath = connectionString!.Replace("Data Source=", string.Empty);

// Resolve absolute path based on application base directory
var fullDbPath = Path.GetFullPath(
    Path.Combine(AppContext.BaseDirectory, dbPath));

// Ensure that the Data directory exists (API owns the database lifecycle)
var dbDirectory = Path.GetDirectoryName(fullDbPath)!;
if (!Directory.Exists(dbDirectory))
{
    Directory.CreateDirectory(dbDirectory);
}

// Register DbContext with resolved absolute database path
builder.Services.AddDbContext<ProcessDbContext>(options =>
    options.UseSqlite($"Data Source={fullDbPath}"));


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProcessDbContext>();
    db.Database.EnsureCreated();
}
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
    
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();




