using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bancknet.Data;
using bancknet.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace bancknet.Controllers
{
    public class UserController : Controller
    {
        private readonly AplicatinsDbContext _db ;
        public UserController(AplicatinsDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                IEnumerable<User> ObjList = _db.User;
                return View(ObjList);
            }
            return RedirectToAction("Login");
        }

        //Post creat
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Creat(User obj)
        {
            _db.User.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Get creat
        public IActionResult Creat()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login");
        }

        //Get login
        public IActionResult Login()
        {
            return View();
        }

        //Post Login
        [HttpPost]
        public async Task<IActionResult> LoginAsync(string Email, string Password)
        {
            var user = _db.User.Where(b=> b.email == Email).FirstOrDefault();
            if (user.password == Password)
            {

                List<Claim> AccessRights = new()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.userid.ToString()),
                    new Claim(ClaimTypes.Name,user.name)
                };

                var identity = new ClaimsIdentity(AccessRights,"Access.Login");
                var userPrincial = new ClaimsPrincipal(new[] { identity });

                await HttpContext.SignInAsync("Access.Login", userPrincial,
                    new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTime.Now.AddHours(1)
                    });

                
                return RedirectToAction("Index","Wallet");
                
            }
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }
            return RedirectToAction("Login");
        }
    }
}
