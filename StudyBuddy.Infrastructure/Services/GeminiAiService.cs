using Microsoft.Extensions.Configuration;
using StudyBuddy.Application.Services.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudyBuddy.Infrastructure.Services
{
    public class GeminiAiService : IAiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public GeminiAiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<string> GenerateAsync(string prompt)
        {
            var apiKey = _config["Gemini:ApiKey"];
            var endpoint = _config["Gemini:Endpoint"] + $"?key={apiKey}";

            var payload = new
            {
                contents = new[]
                {
                new { parts = new[] { new { text = prompt } }
                }
            }
            };

            var response = await _httpClient.PostAsJsonAsync(endpoint, payload);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<JsonElement>();

            return result.GetProperty("candidates")[0]
                         .GetProperty("content")
                         .GetProperty("parts")[0]
                         .GetProperty("text")
                         .GetString();


        }
    }
}
