using JobWatcher.Api;
using JobWatcher.Infrastructure.Persistence;
using JobWatcher.Infrastructure;
using JobWatcher.Worker;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHostedService<MonitoringWorker>();

builder.Services.AddInfrastructure();

// builder.Services.AddHostedService<ScraperDiTestWorker>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapTelegramWebhook();

app.Run();