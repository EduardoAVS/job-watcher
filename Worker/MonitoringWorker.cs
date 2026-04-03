using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JobWatcher.Worker;

public class MonitoringWorker : BackgroundService
{
    private readonly ILogger<MonitoringWorker> _logger;

    public MonitoringWorker(ILogger<MonitoringWorker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // MVP: roda de tempos em tempos (ex.: 6h no final)
        // Agora vamos rodar a cada 1 minuto só pra ver funcionando.
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

        while (!stoppingToken.IsCancellationRequested &&
               await timer.WaitForNextTickAsync(stoppingToken))
        {
            _logger.LogInformation("Worker tick: {Time}", DateTimeOffset.UtcNow);

            // Depois entra o fluxo: buscar páginas, baixar HTML, extrair links, dedupe, classificar, salvar, notificar
            // (conforme requisito) :contentReference[oaicite:9]{index=9}
        }
    }
}