using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GymManagemarntSystem.Models;
using System.Data.SqlClient;

namespace GymManagemarntSystem.Controllers
{
    [SessionTimeoutAttribute]
    public class SettingsController : Controller
    {
        GymSystem Ctx = new GymSystem();
        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {

            return View(Ctx.users);
        }

        public ActionResult Deleteusers(int id)
        {
           var user = Ctx.users.Find(id);

            Ctx.users.Remove(user);
            Ctx.SaveChanges();
           
            return RedirectToAction("Users");
        }


        [HttpPost]
        public JsonResult ChangeUserPassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Ctx.users.SingleOrDefault(a => a.C_token == model.token);
                if(user!= null)
                {
                    user.Password = model.newpassword;
                    Ctx.SaveChanges();
                }

                return Json("تم تغيير كلمة السر بنجاح",JsonRequestBehavior.AllowGet);
            }
            else
                return Json("كلمة السر غير صحيحة",JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult addUser(UserViewModel Model)
        {
            if (ModelState.IsValid)
            {
                Ctx.users.Add(new user { UserName = Model.name, Password = Model.password ,Role=Model.Role });
                Ctx.SaveChanges();
                return Json("تم اضافة المستخدم بنجاح", JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json("لم يتم ادخال بيانات صحيحة", JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public JsonResult addsubscriptionType (subscriptionType model)
        {
            Ctx.subscriptionTypes.Add(new subscriptionType { subscription_name=model.subscription_name,Price=model.Price,Days=model.Days });
            Ctx.SaveChanges();
            

            return Json("تم اضافة نوع الاشتراك بنجاح",JsonRequestBehavior.AllowGet);
        }



        public JsonResult BackupDB ()
        {
            //var path = Server.MapPath("~/Backup/");

            //string file = path + DateTime.Now.ToString() + ".bak";
            string date = DateTime.Now.Date.ToString("yyyy/MM/dd");
            string DLocation = "D:" +date +".bak";
            
            var con = new SqlConnection(@"Data Source=.;Initial Catalog=GymSystem;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
            var cmd = "BACKUP DATABASE GymSystem TO DISK = '"+DLocation+"' ";
            
            con.Open();
            var command = new SqlCommand(cmd, con);
            SqlDataReader reader;

            reader =  command.ExecuteReader();

            con.Close();

            return Json("Done",JsonRequestBehavior.AllowGet);
        }



        public ActionResult Logout()
        {
            var user = Ctx.users.Where(a => a.C_token == Session.SessionID).SingleOrDefault();
            user.C_token = null;
            Ctx.SaveChanges();

            Session.Abandon();

            return Redirect("/Account/Login");
        }

    }
}