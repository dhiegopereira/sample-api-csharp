using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using sample_api_csharp.Abstracts;
using sample_api_csharp.DTOs;
using sample_api_csharp.Models;

namespace sample_api_csharp.Services
{
    public class CustomerService : AbstractService
    {
        public CustomerService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<Dictionary<string, string>> GetAddress(string cep)
        {
            
            try
            {
                string url = $"https://viacep.com.br/ws/{cep}/json/";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                using var jsonDocument = JsonDocument.Parse(responseBody);

                var address = new Dictionary<string, string>();
                foreach (var property in jsonDocument.RootElement.EnumerateObject())
                {
                    address[property.Name] = property.Value.GetString();
                }

                return address;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao fazer a requisição HTTP: {ex.Message}");
                throw;
            }
        }

    }
}
