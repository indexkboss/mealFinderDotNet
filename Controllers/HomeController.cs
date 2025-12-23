//using System.Diagnostics;
//using mealFinderDotNet.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace mealFinderDotNet.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;

//        public HomeController(ILogger<HomeController> logger)
//        {
//            _logger = logger;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}
using Firebase.Auth;
using mealFinderDotNet.Data;
using mealFinderDotNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace mealFinderDotNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly FirebaseAuthProvider _auth;
        private readonly TaBaseContext _context;

        public HomeController(TaBaseContext context)
        {
            _context = context;
            _auth = new FirebaseAuthProvider(
                new FirebaseConfig("AIzaSyCV23d2nbWeg6CCC9fHTQJdLM31uPywVxk"));
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("_FirebaseUserId");
            if (!string.IsNullOrEmpty(userId))
            {
                return View();
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel userModel)
        {
            try
            {
                // Créer l'utilisateur Firebase
                var fbAuthLink = await _auth.CreateUserWithEmailAndPasswordAsync(userModel.Email, userModel.Password);

                // Stocker dans la DB locale
                var user = new Utilisateur
                {
                    FullName = userModel.FullName,
                    Email = userModel.Email,
                    FirebaseId = fbAuthLink.User.LocalId
                };

                _context.Utilisateurs.Add(user);
                await _context.SaveChangesAsync();

                // Connecter l'utilisateur
                var fbSignIn = await _auth.SignInWithEmailAndPasswordAsync(userModel.Email, userModel.Password);

                HttpContext.Session.SetString("_FirebaseUserId", fbSignIn.User.LocalId);
                return RedirectToAction("Index");
            }
            catch (FirebaseAuthException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(userModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(RegisterViewModel userModel)
        {
            try
            {
                var fbAuthLink = await _auth.SignInWithEmailAndPasswordAsync(userModel.Email, userModel.Password);
                HttpContext.Session.SetString("_FirebaseUserId", fbAuthLink.User.LocalId);
                return RedirectToAction("Index");
            }
            catch (FirebaseAuthException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(userModel);
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("_FirebaseUserId");
            return RedirectToAction("SignIn");
        }
    }
}

