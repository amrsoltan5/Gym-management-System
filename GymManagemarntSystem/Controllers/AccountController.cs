using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GymManagemarntSystem.Models;

namespace GymManagemarntSystem.Controllers
{
    public class AccountController : Controller
    {
        GymSystem Ctx = new GymSystem();
        // GET: Account
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel L1)
        {
            if (Ctx.users.Where(a => a.UserName == L1.username && a.Password == L1.password).SingleOrDefault() != null)
            {
                var user = Ctx.users.Where(a => a.UserName == L1.username && a.Password == L1.password).SingleOrDefault();
                user.C_token = Session.SessionID;
                Session["Role"] = user.Role;
                Session["ID"] = user.ID;
                
                Ctx.SaveChanges();

                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                ViewBag.LoginFailed = "username or password uncorrect";
                return View();
            }
        }
    }
}