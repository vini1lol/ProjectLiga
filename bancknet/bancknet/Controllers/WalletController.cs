using bancknet.Data;
using bancknet.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

//Wallet controller
namespace bancknet.Controllers
{
    public class WalletController : Controller
    {
        //Create database access variabel
        private readonly AplicatinsDbContext _db;
        public WalletController(AplicatinsDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //Verify if the user is logged
            if (User.Identity.IsAuthenticated)
            {
                //Get logged user id and name
                Dictionary<string, string> a = new Dictionary<string, string>();
                string[] key = new string[] { "Id", "Name" };
                int i = 0;
                foreach (Claim ci in User.Claims) 
                { 
                    a.Add(key[i], ci.Value);
                    i++;
                }
                int id = int.Parse(a["Id"]);
                //Get user from database by ID
                var user = _db.User.Where(b=>b.userid==id).FirstOrDefault();
                //Set viewData variables
                ViewData.Add("Valor", user.Valor);
                ViewData.Add("Nome", user.name);
                //Get list of wallets objecs where user_id is the same as the logged user 
                IEnumerable<Mywallet> ObjList = _db.Mywallet.Where(b => b.User_id==id);
                //Return the list for the Wallet Index view
                return View(ObjList);
            }
            //Redirect to User Login action
            return RedirectToAction("Login","User");
        }
        //Post creat
        [HttpPost]
        public IActionResult Creat(Mywallet obj)
        {
            //Get logged user id and name
            Dictionary<string, string> a = new Dictionary<string, string>();
            string[] key = new string[] { "Id", "Name" };
            int i = 0;
            foreach (Claim ci in User.Claims)
            {
                a.Add(key[i], ci.Value);
                i++;
            }
            int id = int.Parse(a["Id"]);
            //Get user from database by ID
            var user = _db.User.Where(b => b.userid == id).FirstOrDefault();
            //Update the Value variable
            user.Valor += obj.Value;
            obj.User_id =id;
            //Update user in database
            _db.User.Update(user);
            //Add new Wallet object in database
            _db.Mywallet.Add(obj);
            //Save database changes
            _db.SaveChanges();
            //Redirect to Index action
            return RedirectToAction("Index");
        }
        //Get creat
        public IActionResult Creat()
        {
            //Verify if the user is logged
            if (User.Identity.IsAuthenticated)
            {
                //Return Creat view
                return View();
            }
            //Redirect to User Login action
            return RedirectToAction("Login", "User");
        }

        // Get Withdraw
        //Actoin that returns the Wallet objects with value > 0 and logged user id
        public IActionResult Withdraw()
        {
            //Verify if the user is logged
            if (User.Identity.IsAuthenticated)
            {
                Dictionary<string, string> a = new Dictionary<string, string>();
                string[] key = new string[] { "Id", "Name" };
                int i = 0;
                foreach (Claim ci in User.Claims)
                {
                    a.Add(key[i], ci.Value);
                    i++;
                }
                int id = int.Parse(a["Id"]);
                var user = _db.User.Where(b => b.userid == id).FirstOrDefault();
                ViewData.Add("Valor", user.Valor);
                ViewData.Add("Nome", user.name);
                IEnumerable<Mywallet> ObjList = _db.Mywallet.Where(b => b.Value < 0 && b.User_id == id);
                return View("Index", ObjList);
            }
            //Redirect to User Login action
            return RedirectToAction("Login", "User");
        }
        // Get Deposit
        //Actoin that returns the Wallet objects with value < 0 and logged user id
        public IActionResult Deposit()
        {
            //Verify if the user is logged
            if (User.Identity.IsAuthenticated)
            {
                Dictionary<string, string> a = new Dictionary<string, string>();
                string[] key = new string[] { "Id", "Name" };
                int i = 0;
                foreach (Claim ci in User.Claims)
                {
                    a.Add(key[i], ci.Value);
                    i++;
                }
                int id = int.Parse(a["Id"]);
                var user = _db.User.Where(b => b.userid == id).FirstOrDefault();
                ViewData.Add("Valor", user.Valor);
                ViewData.Add("Nome", user.name);
                IEnumerable<Mywallet> ObjList = _db.Mywallet.Where(b => b.Value > 0 && b.User_id == id);
                return View("Index", ObjList);
            }
            //Redirect to User Login action
            return RedirectToAction("Login", "User");
        }

    }
}
