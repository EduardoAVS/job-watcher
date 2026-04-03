using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace JobWatcher.Api;

public static class TelegramWebhookEndpoints
{
    public static void MapTelegramWebhook(this WebApplication app)
    {
        app.MapPost("/telegram/webhook", async (HttpRequest request) =>
        {
            // MVP: por enquanto só confirmamos que o endpoint recebe POST
            // Depois vamos parsear o Update do Telegram e chamar casos de uso (/start, /add, etc.)
            return Results.Ok();
        });
    }
}