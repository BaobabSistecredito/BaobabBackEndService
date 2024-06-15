using System.Text;
using System.Text.Json;


namespace BaobabBackEndService.ExternalServices.SlackNotificationService;

public class SlackNotificationService
{
    public async void SendSlackNotification(string errorC, string errorM, string generalM)
    {
        // URL del webhook para la solicitud POST de la API de Slack:
        string url = "https://hooks.slack.com/services/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";

        // Se crea una instancia de la clase 'SlackMessageService' para contener el mensaje:
        var slackMessage = new SlackMessageService
        {
            errorCode = errorC,
            errorMessage = errorM,
            generalMessage = generalM
        };

        // Serializar el objeto 'slackMessage' en formato JSON:
        string jsonBody = JsonSerializer.Serialize(slackMessage);

        // Crear un objeto HTTPClient para realizar la solicitud HTTP:
        using(HttpClient client = new HttpClient())
        {
            // Crear el contenido de la solicitud POST como StringContent:
            StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            // Realizar la solicitud POST a la URL indicada:
            HttpResponseMessage response = await client.PostAsync(url, content);
            // Se confirma si la solicitud fue éxitosa (código de estado: 200 - 209):
            if(response.IsSuccessStatusCode)
            {
                // Se muestra el estado de la solicitud:
                Console.WriteLine($"Estado de la solicitud: {response.StatusCode}");
            }
            else
            {
                // Si la solicitud no fue éxitosa, se muestra el estado de la solicitud:
                Console.WriteLine($"La solicitud falló con el código de estado: {response.StatusCode}");
            }
        }
    }
}