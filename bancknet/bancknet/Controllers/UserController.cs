using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bancknet.Data;
using bancknet.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

//User controller
namespace bancknet.Controllers
{
    public class UserController : Controller
    {
        //Database access variable
        private readonly AplicatinsDbContext _db ;
        public UserController(AplicatinsDbContext db)
        {
            _db = db;
        }
        //Index page action
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //Get all users
                IEnumerable<User> ObjList = _db.User;
                //return to Index view the list of users
                return View(ObjList);
            }
            //Redirect to Login action
            return RedirectToAction("Login");
        }

        //Post creat
        [HttpPost]
        public IActionResult Creat(User obj)
        {
            //Password hash wish BCrypt
            obj.password = BCrypt.Net.BCrypt.HashPassword(obj.password);
            //Add user to Database
            _db.User.Add(obj);
            //Save changes in the database
            _db.SaveChanges();
            //Redirect to Login action
            return RedirectToAction("Login");
        }

        //Get creat
        public IActionResult Creat()
        {
            //Go to creat view
            return View();
        }

        //Get login
        public IActionResult Login()
        {
            //Go to Login view
            return View();
        }

        //Post Login
        [HttpPost]
        public async Task<IActionResult> LoginAsync(string Email, string Password)
        {
            //Get user from database by email
            var user = _db.User.Where(b=> b.email == Email).FirstOrDefault();
            //Verify if is null
            if (user == null)
                //Redirect to Login action
                return RedirectToAction("Login");
            //verify user password
            bool isValidpassword = BCrypt.Net.BCrypt.Verify(Password,user.password);
            if (isValidpassword)
            {
                //Create a list of claims
                List<Claim> AccessRights = new()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.userid.ToString()),
                    new Claim(ClaimTypes.Name,user.name)
                };

                var identity = new ClaimsIdentity(AccessRights,"Access.Login");
                var userPrincial = new ClaimsPrincipal(new[] { identity });

                //Start de user Login
                await HttpContext.SignInAsync("Access.Login", userPrincial,
                    new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTime.Now.AddHours(1)
                    });

                //Redirect to Wallet Index action
                return RedirectToAction("Index","Wallet");
                
            }
            //Redirect to Login action
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            //Verify if the user is logged
            if (User.Identity.IsAuthenticated)
            {
                //Logout user
                await HttpContext.SignOutAsync();
            }
            //Redirect to Home Index action
            return RedirectToAction("Index","Home");
        }
    }
}
