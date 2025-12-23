using mealFinderDotNet.Data;
using mealFinderDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace mealFinderDotNet.Controllers
{
    public class FavorisController : Controller
    {
        private readonly TaBaseContext _context;

        public FavorisController(TaBaseContext context)
        {
            _context = context;
        }

        // ❤️ Toggle favori : ajoute ou supprime selon si c'est déjà présent
        [HttpPost]
        public IActionResult Toggle(int recipeId)
        {
            // Vérifier si utilisateur connecté via Firebase
            var firebaseId = HttpContext.Session.GetString("_FirebaseUserId");
            if (string.IsNullOrEmpty(firebaseId))
                return RedirectToAction("SignIn", "Home");

            var user = _context.Utilisateurs.FirstOrDefault(u => u.FirebaseId == firebaseId);
            if (user == null)
                return RedirectToAction("SignIn", "Home");

            int utilisateurId = user.Id;

            // Vérifier si le favori existe
            var favori = _context.Favoris
                .FirstOrDefault(f => f.UtilisateurId == utilisateurId && f.IdRecetteAPI == recipeId);

            if (favori != null)
            {
                _context.Favoris.Remove(favori);
            }
            else
            {
                _context.Favoris.Add(new Favori
                {
                    IdRecetteAPI = recipeId,
                    UtilisateurId = utilisateurId
                });
            }

            _context.SaveChanges();

            return RedirectToAction("Details", "Recipe", new { id = recipeId });
        }

        // 📌 Liste des favoris de l'utilisateur
        public IActionResult Index()
        {
            var firebaseId = HttpContext.Session.GetString("_FirebaseUserId");
            if (string.IsNullOrEmpty(firebaseId))
                return RedirectToAction("SignIn", "Home");

            var user = _context.Utilisateurs.FirstOrDefault(u => u.FirebaseId == firebaseId);
            if (user == null)
                return RedirectToAction("SignIn", "Home");

            int utilisateurId = user.Id;

            var favoris = _context.Favoris
                .Where(f => f.UtilisateurId == utilisateurId)
                .Join(
                    _context.Recettes,
                    f => f.IdRecetteAPI,
                    r => r.ApiId,
                    (f, r) => new FavoriViewModel
                    {
                        FavoriId = f.Id,
                        RecipeId = r.ApiId,
                        Title = r.Title,
                        Image = r.Image
                    })
                .ToList();

            return View(favoris);
        }

        // ❌ Supprimer un favori depuis la liste
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var firebaseId = HttpContext.Session.GetString("_FirebaseUserId");
            if (string.IsNullOrEmpty(firebaseId))
                return RedirectToAction("SignIn", "Home");

            var user = _context.Utilisateurs.FirstOrDefault(u => u.FirebaseId == firebaseId);
            if (user == null)
                return RedirectToAction("SignIn", "Home");

            int utilisateurId = user.Id;

            var favori = _context.Favoris.FirstOrDefault(f => f.Id == id && f.UtilisateurId == utilisateurId);
            if (favori != null)
            {
                _context.Favoris.Remove(favori);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
