using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GymManagemarntSystem.Models;

namespace GymManagemarntSystem.Controllers
{
    [SessionTimeoutAttribute]
    public class HomeController : Controller
    {
        GymSystem Ctx = new GymSystem();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult CustomerCount()
        {
            var data = Ctx.customers.Count();

            return Json(data,JsonRequestBehavior.AllowGet);
        }


        public JsonResult ValidCount()
        {
            var data = Ctx.spGetValide().ToList().Count();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


    }
}