using mealFinderDotNet.Data;
using mealFinderDotNet.Models;
using mealFinderDotNet.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace mealFinderDotNet.Controllers
{
    public class RecipeController : Controller
    {
        private readonly SpoonacularService _spoonacularService;
        private readonly TaBaseContext _context;

        public RecipeController(SpoonacularService spoonacularService, TaBaseContext context)
        {
            _spoonacularService = spoonacularService;
            _context = context;
        }

        // GET: /Recipe
        // GET: /Recipe
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 9;

            var recettes = await _spoonacularService.GetRecipesPagedAsync(page, pageSize);

            ViewBag.CurrentPage = page;

            return View(recettes);
        }


        // GET: /Recipe/Details/716429
        public async Task<IActionResult> Details(int id)
        {
            // Chercher la recette dans la DB par ApiId
            var recette = await _context.Recettes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.ApiId == id);

            if (recette == null)
            {
                // Vérifier si elle existe déjà
                var recetteExistante = await _context.Recettes
                    .FirstOrDefaultAsync(r => r.ApiId == id);

                if (recetteExistante != null)
                {
                    recette = recetteExistante;
                }
                else
                {
                    // Sinon récupère depuis Spoonacular
                    var apiRecette = await _spoonacularService.GetRecipeAsync(id);

                    recette = new Recette
                    {
                        ApiId = apiRecette.Id,
                        Title = apiRecette.Title,
                        Image = apiRecette.Image,
                        ReadyInMinutes = apiRecette.ReadyInMinutes,
                        Servings = apiRecette.Servings,
                        Instructions = apiRecette.Instructions,
                        Ingredients = apiRecette.Ingredients
                            .Select(i => new Ingredient { Name = i })
                            .ToList()
                    };

                    // Ajoute seulement si pas déjà présent
                    var exists = await _context.Recettes.AnyAsync(r => r.ApiId == recette.ApiId);
                    if (!exists)
                    {
                        _context.Recettes.Add(recette);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            // Récupérer l'utilisateur connecté
            var firebaseUserId = HttpContext.Session.GetString("_FirebaseUserId");
            int utilisateurId = 0;
            if (!string.IsNullOrEmpty(firebaseUserId))
            {
                var user = _context.Utilisateurs.FirstOrDefault(u => u.FirebaseId == firebaseUserId);
                if (user != null)
                    utilisateurId = user.Id;
            }

            // Vérifier si la recette est déjà en favori
            ViewBag.IsFavori = false;
            if (utilisateurId != 0)
            {
                ViewBag.IsFavori = _context.Favoris
                    .Any(f => f.UtilisateurId == utilisateurId && f.IdRecetteAPI == id);
            }

            // Convertir en RecetteViewModel
            var viewModel = new RecetteViewModel
            {
                Id = recette.ApiId,
                Title = recette.Title,
                Image = recette.Image,
                ReadyInMinutes = recette.ReadyInMinutes,
                Servings = recette.Servings,
                Instructions = recette.Instructions,
                Ingredients = recette.Ingredients.Select(i => i.Name).ToList()
            };

            return View(viewModel);
        }

        // GET: /Recipe/Search?query=pasta
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return View(new List<RecetteViewModel>());

            var apiResults = await _spoonacularService.SearchRecipesAsync(query);

            // Supprimer les doublons par ApiId
            var uniqueResults = apiResults
                .GroupBy(r => r.ApiId)
                .Select(g => g.First())
                .ToList();

            var viewModels = uniqueResults.Select(r => new RecetteViewModel
            {
                Id = r.ApiId,
                Title = r.Title,
                Image = r.Image,
                ReadyInMinutes = r.ReadyInMinutes,
                Servings = r.Servings
            }).ToList();

            return View(viewModels);
        }
    }
}
