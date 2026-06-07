using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LearnFlowERP.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LearnFlowERP.Infrastructure.Notifications
{
    public class BrevoEmailService : IEmailService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public BrevoEmailService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task SendAsync(string to, string subject, string htmlContent)
        {
            var apiKey = _config["Brevo:ApiKey"];

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("api-key", apiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var body = new
            {
                sender = new
                {
                    email = _config["Brevo:SenderEmail"],
                    name = _config["Brevo:SenderName"]
                },
                to = new[]
                {
                new { email = to }
            },
                subject = subject,
                htmlContent = htmlContent
            };

            var content = new StringContent(
                JsonSerializer.Serialize(body),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(
                "https://api.brevo.com/v3/smtp/email",
                content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Email failed: {error}");
            }
        }
    }
}
