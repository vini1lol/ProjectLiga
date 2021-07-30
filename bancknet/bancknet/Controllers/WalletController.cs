using bancknet.Data;
using bancknet.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace bancknet.Controllers
{
    public class WalletController : Controller
    {
        private readonly AplicatinsDbContext _db;
        public WalletController(AplicatinsDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
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
                var user = _db.User.Where(b=>b.userid==id).FirstOrDefault();
                ViewData.Add("Valor", user.Valor);
                IEnumerable<Mywallet> ObjList = _db.Mywallet.Where(b => b.User_id==id);
                return View(ObjList);
            }
            return RedirectToAction("Login","User");
        }
        //Post creat
        [HttpPost]
        public IActionResult Creat(Mywallet obj)
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
            user.Valor += obj.Value;
            obj.User_id =id;
            _db.User.Update(user);
            _db.Mywallet.Add(obj);
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
            return RedirectToAction("Login", "User");
        }

    }
}
