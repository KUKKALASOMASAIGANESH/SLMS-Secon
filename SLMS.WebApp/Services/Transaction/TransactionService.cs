using System.Text;
using System.Text.Json;
using SLMS.WebApp.Models;

namespace SLMS.WebApp.Services.Transaction
{
    public class TransactionService
    {
        private readonly HttpClient _httpClient;

        public TransactionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // ✅ GET ALL
        public async Task<List<TransactionViewModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7277/api/transaction");

            if (!response.IsSuccessStatusCode)
                return new List<TransactionViewModel>();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<TransactionViewModel>>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<TransactionViewModel>();
        }

        // ✅ CREATE
        public async Task CreateAsync(TransactionViewModel model)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(model),
                Encoding.UTF8,
                "application/json");

            await _httpClient.PostAsync("https://localhost:7277/api/transaction", content);
        }
    }
}