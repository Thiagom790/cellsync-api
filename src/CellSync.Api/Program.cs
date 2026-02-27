using CellSync.Application;
using CellSync.Infrastructure;
using CellSync.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add Aspire service defaults
builder.AddServiceDefaults();

//Inject Dependêncies
builder.AddInfrastructure();
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Map aspire health checks
app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Run migrations
await app.Services.MigrateDatabaseAsync();

app.Run();

public partial class Program
{
}