namespace BaobabBackEndService.ExternalServices.MailSendService
{
    public class MailSendService : IMailSendService
    {
        private readonly HttpClient _httpClient;

        private readonly string? ApiUrl;//link de la api
        private readonly string? ApiKey;//token

        private readonly string? FromEmail;//correo de mailsender

        public MailSendService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            ApiUrl = configuration["MailerSend:APIURL"];
            ApiKey = configuration["MailerSend:APYKEY"];
            FromEmail = configuration["MailerSend:ToFrom"];
        }


        public async Task<string> SendEmailAsync(string toEmail, string CodeCoupon, string PurchaseId, string PurchaseValue, string RedemptionDate)
        {
            var request = new
            {
                from = new { email = FromEmail, },
                to = new[] { new { email = toEmail } },
                subject = "Haz redimido tu cupón",
                variables = new[] {new {email = toEmail,substitutions = new []{
                        new { var = "CodeCoupon", value = CodeCoupon },
                        new { var = "PurchaseId", value = PurchaseId },
                        new { var = "PurchaseValue", value = PurchaseValue },
                        new { var = "RedemptionDate", value = RedemptionDate }
                    }}},

                template_id = "0r83ql3x53zlzw1j"
            };
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ApiKey);

            try
            {
                var result = await _httpClient.PostAsJsonAsync(ApiUrl, request);
                result.EnsureSuccessStatusCode();

                return "Se ha enviado con exito";

            }
            catch (HttpRequestException ex)
            {
                return "Error" + ex.Message;
            }
        }
    }
}