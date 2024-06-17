namespace BaobabBackEndService.ExternalServices.SlackNotificationService;

public class SlackSettingsService
{
    /* 
        Declaración de propiedades las cuales se inicializarán como 'configuraciones globales'
        en la variable de entorno 'SlackSettingsService' en el archivo 'appsettings.json'.
    */
    public string WebhookUrl { get; set; } // Propiedad para almacenar la URL del WebHook.
    public string BotToken { get; set; } // Propiedad para almacenar el token del bot.
}