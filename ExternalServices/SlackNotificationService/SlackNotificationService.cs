using SlackNet;
using SlackNet.WebApi;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using BaobabBackEndService.ExternalServices.SlackNotificationService;


namespace BaobabBackEndService.ExternalServices.SlackNotificationService;

public class SlackNotificationService
{
    /* Definición de variables las cuales se inicializarán luego con las dependencias */
    private readonly HttpClient _httpClient; // Cliente HTTP para realizar solicitudes HTTP.
    private readonly string _webhookUrl; // URL del WebHook de Slack.
    private readonly string _botToken; // Token de autenticación del bot de Slack.
    /* 
    Constructor que recibe el cliente HTTP y las configuraciones de Slack.
    Inyección de dependencias.
     */
    public SlackNotificationService(HttpClient httpClient, IOptions<SlackSettingsService> slackSettings)
    {
        _httpClient = httpClient; // Inicializa el cliente HTTP.
        _webhookUrl = slackSettings.Value.WebhookUrl; // Inicializa la URL del WebHook.
        _botToken = slackSettings.Value.BotToken; // Inicializa el token del bot.
    }
    
    public async Task SendNotification(string message) // Método para enviar notificaciones a Slack.
    {

        var payload = new // Se crea un objeto que contiene con el mensaje.
        {
            text = message // Asigna el mensaje recibido como parámetro al campo 'text'.
        };

        var jsonPayload = JsonSerializer.Serialize(payload); // Serializa el objeto a formato JSON.
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json"); // Crea el contenido de la solicitud HTTP con el JSON.
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _botToken); // Añade el token de autenticación a la cabecera de la solicitud.

        var response = await _httpClient.PostAsync(_webhookUrl, content); // Envía una solicitud POST a la URL del WebHook con el contenido.

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("No se procesó la notificación: " + response); // Imprime un mensaje en caso de error.
        }
        else
        {
            Console.WriteLine("Notificación exitosa: " + response); // Imprime un mensaje en caso de éxito.
        }
    }
}