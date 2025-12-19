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
        FirebaseAuthProvider auth;
        public HomeController()
        {
            auth = new FirebaseAuthProvider(
                            new FirebaseConfig("AIzaSyCV23d2nbWeg6CCC9fHTQJdLM31uPywVxk"));
        }

        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("_UserToken");
            if (token != null)
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

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel userModel)
        {
            try
            {
                //Creer l'utilisateur dans Firebase (Email + Password)
                var fbAuthLink = await auth.CreateUserWithEmailAndPasswordAsync(userModel.Email, userModel.Password);

                //Stocker FullName + Email + FirebaseId dans SQL
                using (var db = new TaBaseContext())
                {
                    var user = new Utilisateur
                    {
                        FullName = userModel.FullName,
                        Email = userModel.Email,
                        FirebaseId = fbAuthLink.User.LocalId
                    };
                    db.Utilisateurs.Add(user);
                    db.SaveChanges();
                }

                //Log in le nouvel utilisateur
                var fbSignIn = await auth.SignInWithEmailAndPasswordAsync(userModel.Email, userModel.Password);
                string token = fbSignIn.FirebaseToken;

                if (token != null)
                {
                    HttpContext.Session.SetString("_UserToken", token);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(userModel);
                }
            }
            catch (FirebaseAuthException ex)
            {
                // Gestion simple des erreurs Firebase (ex : email deja utiliser)
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(userModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(RegisterViewModel userModel)
        {
            var fbAuthLink = await auth.SignInWithEmailAndPasswordAsync(userModel.Email, userModel.Password);
            string token = fbAuthLink.FirebaseToken;

            if (token != null)
            {
                HttpContext.Session.SetString("_UserToken", token);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("_UserToken");
            return RedirectToAction("SignIn");
        }
    }
}

