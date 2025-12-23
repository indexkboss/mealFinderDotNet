using System.Text.Json;
using mealFinderDotNet.Models;

namespace mealFinderDotNet.Services
{
    public class SpoonacularService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public SpoonacularService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(config["Spoonacular:BaseUrl"]);
            _apiKey = config["Spoonacular:ApiKey"];
        }

        // 🍽️ DÉTAILS RECETTE (API ID UNIQUEMENT)
        public async Task<RecetteViewModel?> GetRecipeAsync(int apiId)
        {
            var response = await _httpClient.GetAsync(
                $"recipes/{apiId}/information?apiKey={_apiKey}");

            if (!response.IsSuccessStatusCode)
                return null;

            using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var root = doc.RootElement;

            var recette = new RecetteViewModel
            {
                Id = root.GetProperty("id").GetInt32(),
                Title = root.GetProperty("title").GetString(),
                Image = root.GetProperty("image").GetString(),
                ReadyInMinutes = root.GetProperty("readyInMinutes").GetInt32(),
                Servings = root.GetProperty("servings").GetInt32(),
                Instructions = root.GetProperty("instructions").GetString() ?? "",
                Ingredients = new List<string>()
            };

            if (root.TryGetProperty("extendedIngredients", out var ingredients))
            {
                foreach (var ing in ingredients.EnumerateArray())
                {
                    var name = ing.GetProperty("original").GetString();
                    if (!string.IsNullOrEmpty(name))
                        recette.Ingredients.Add(name);
                }
            }

            return recette;
        }

        // 🔍 SEARCH RECETTES
        public async Task<List<Recette>> SearchRecipesAsync(string query)
        {
            var response = await _httpClient.GetAsync(
                $"recipes/complexSearch?query={query}&number=12&apiKey={_apiKey}");

            if (!response.IsSuccessStatusCode)
                return new List<Recette>();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var results = doc.RootElement.GetProperty("results");
            var recettes = new List<Recette>();

            foreach (var item in results.EnumerateArray())
            {
                recettes.Add(new Recette
                {
                    ApiId = item.GetProperty("id").GetInt32(),
                    Title = item.GetProperty("title").GetString(),
                    Image = item.GetProperty("image").GetString()
                });
            }

            return recettes;
        }

        // 📄 PAGINATION - LISTE DES RECETTES
        public async Task<List<RecetteViewModel>> GetRecipesPagedAsync(int page, int pageSize)
        {
            int offset = (page - 1) * pageSize;

            var response = await _httpClient.GetAsync(
                $"recipes/complexSearch?number={pageSize}&offset={offset}&addRecipeInformation=true&apiKey={_apiKey}");

            if (!response.IsSuccessStatusCode)
                return new List<RecetteViewModel>();

            using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var results = doc.RootElement.GetProperty("results");

            var recettes = new List<RecetteViewModel>();

            foreach (var item in results.EnumerateArray())
            {
                recettes.Add(new RecetteViewModel
                {
                    Id = item.GetProperty("id").GetInt32(),
                    Title = item.GetProperty("title").GetString(),
                    Image = item.GetProperty("image").GetString(),
                    ReadyInMinutes = item.TryGetProperty("readyInMinutes", out var time)
                        ? time.GetInt32()
                        : 0,
                    Servings = item.TryGetProperty("servings", out var servings)
                        ? servings.GetInt32()
                        : 0
                });
            }

            return recettes;
        }
    }
}
