using JobWatcher.Api;
using JobWatcher.Worker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<MonitoringWorker>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapTelegramWebhook();

app.Run();