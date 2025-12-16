using mealFinderDotNet.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace mealFinderDotNet.Controllers
{
    

    public class RecipeController : Controller
    {
        private readonly SpoonacularService _spoonacularService;

        public RecipeController(SpoonacularService spoonacularService)
        {
            _spoonacularService = spoonacularService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var recipeJson = await _spoonacularService.GetRecipeAsync(id);
            return Content(recipeJson, "application/json");
        }
    }
}
