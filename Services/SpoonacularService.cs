using System.Net.Http;
using System.Threading.Tasks;

namespace mealFinderDotNet.Services
{

    public class SpoonacularService
    {
        private readonly HttpClient _httpClient;
        private const string apiKey = "7dea454936f54031a7716dd62cabbb19";
        // Remplacez par votre clé API

        public SpoonacularService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.spoonacular.com/");
        }

        public async Task<string> GetRecipeAsync(int recipeId)
        {
            var response = await _httpClient.GetAsync($"recipes/{recipeId}/information?apiKey={apiKey}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
