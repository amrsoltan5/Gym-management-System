using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GymManagemarntSystem.Models;
using System.IO;

namespace GymManagemarntSystem.Controllers
{
    [SessionTimeoutAttribute]
    public class CustomerController : Controller
    {
        GymSystem Ctx = new GymSystem();
        // GET: Customer
        public ActionResult Index()
        {
          
            return View(Ctx.customers);
        }


        public ActionResult Create()
        {
            IEnumerable<SelectListItem> Dropdownlist = new SelectList(Ctx.subscriptionTypes, "ID", "subscription_name");

            ViewBag.Subscription_ID = Dropdownlist;

            ViewBag.date = DateTime.Now.Date;

            return View();
        }

        public string IsimageFound(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string filename = Path.GetFileName(file.FileName)+"Dateis"+DateTime.Now.ToString();
                string filepath = Server.MapPath("~/images/");
                string filepathname = Path.Combine(filepath, filename);
                file.SaveAs(filepathname);
                return filename;
            }
            else
                return null;

        }

        [HttpPost]
        public ActionResult Create(CustomerViewModel customer,HttpPostedFileBase file)
        {
            IEnumerable<SelectListItem> Dropdownlist = new SelectList(Ctx.subscriptionTypes, "ID", "subscription_name");

            ViewBag.Subscription_ID = Dropdownlist;



            if (ModelState.IsValid)
            {
                var data = Ctx.subscriptionTypes.SingleOrDefault(a => a.ID == customer.Subscription_ID);
                Ctx.customers.Add(new customer {
                    Name = customer.name,
                    Phone = customer.phone,
                    Height = customer.Height,
                    Weight = customer.Weight,
                    Startdate = DateTime.Now.Date,
                    Enddate = DateTime.Now.Date.AddDays((double)data.Days),
                    Photo = IsimageFound(file),
                    Subscription_ID=customer.Subscription_ID

                });
                Ctx.SaveChanges();

                var idofcustomer = Ctx.customers.Where(a=>a.Phone==customer.phone).SingleOrDefault().ID;

                Ctx.attends.Add(new attend {
                    Customer_ID = idofcustomer,
                    AttendDate_Now=DateTime.Now.Date,
                    AttendNumber = 1
                });

                Ctx.SaveChanges();

                return RedirectToAction("Create");
            }else
            {
                return View(customer);
            }
            
        }


        public ActionResult Edite (int id)
        {
            IEnumerable<SelectListItem> Dropdownlist = new SelectList(Ctx.subscriptionTypes, "ID", "subscription_name");

            ViewBag.Subscription_ID = Dropdownlist;

            var data = Ctx.customers.Single(a => a.ID == id);
            CustomerViewModel data2 = new CustomerViewModel();

            data2.id = data.ID;
            data2.name = data.Name;
            data2.phone = data.Phone;
            data2.Height = data.Height.Value;
            data2.Weight = data.Weight.Value;
            data2.Startdate = data.Startdate.Value;
            data2.Enddate = data.Enddate.Value;
            data2.Subscription_ID = data.Subscription_ID.Value;

            return View(data2);
        }

        [HttpPost]
        public ActionResult Edite(CustomerEditeViewModel customer)
        {

            if (ModelState.IsValid)
            {


                var oldcustomer = Ctx.customers.Find(customer.id);

                oldcustomer.Name = customer.name;
                oldcustomer.Phone = customer.phone;
                oldcustomer.Height = customer.Height;
                oldcustomer.Weight = customer.Weight;
                oldcustomer.Startdate = customer.Startdate.Date;
                oldcustomer.Enddate = customer.Enddate.Date;

                Ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(customer);
            
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = Ctx.customers.Find(id);
            Ctx.customers.Remove(obj);
            Ctx.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Search()
        {
            return View();
        }


        public ActionResult SearchByPhone (string phone )
        {

            var data = Ctx.customers.SingleOrDefault(a => a.Phone == phone);

            if(data!= null)
            {

                return View(data);
            }else
            {
                return View();
            }


        }

      public JsonResult Attend(int id)
            
        {

            if(Ctx.attends.Any(a=>a.Customer_ID==id))
            {

              var customer =  Ctx.attends.Single(a => a.Customer_ID == id);

                customer.AttendNumber++;
                customer.AttendDate_Now = DateTime.Now.Date;
                Ctx.SaveChanges();
                return Json(true,JsonRequestBehavior.AllowGet);


            }
            else
            {
                Ctx.attends.Add(new attend {
                    Customer_ID = id,
                    AttendNumber = 1,
                    AttendDate_Now = DateTime.Now.Date

                });
                Ctx.SaveChanges();

                return Json(false,JsonRequestBehavior.AllowGet);
            }
            

            
        } 





        public JsonResult CheckAttend(int id)
        {
            var obj = Ctx.attends.SingleOrDefault(a => a.Customer_ID == id);
            var attenddate = obj.AttendDate_Now.Value.ToString();
            if (attenddate != null)
            {
                if (obj.AttendDate_Now.Value.ToString("yyyy/MM/dd") == DateTime.Now.Date.ToString("yyyy/MM/dd"))
                {
                    return Json(true, JsonRequestBehavior.AllowGet);

                }

                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

            }
            else
                return Json("  ",JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Extend(int id)
        {
            IEnumerable<SelectListItem> Dropdownlist = new SelectList(Ctx.subscriptionTypes, "ID", "subscription_name");

            ViewBag.Subscription_ID = Dropdownlist;

            var customer =Ctx.customers.Find(id);

            ExtendViewModel c = new ExtendViewModel();
            c.customer = customer;

            return View(c);
        }


        public ActionResult Extend(ExtendViewModel model)
        {
            if (ModelState.IsValid)
            {

                IEnumerable<SelectListItem> Dropdownlist = new SelectList(Ctx.subscriptionTypes, "ID", "subscription_name");

                ViewBag.Subscription_ID = Dropdownlist;


                var userid = Ctx.users.SingleOrDefault(a => a.C_token == model.sessionID).ID;

                var customerid = model.id;

                var subscriptiontype = Ctx.subscriptionTypes.Find(model.Subscription_ID);

                Ctx.Extends.Add(new Extend
                {

                    userid = userid,
                    Extenddate = DateTime.Now.Date,
                    customerID = customerid,
                    subscriptionID = subscriptiontype.ID

                });
                Ctx.SaveChanges();

                var customer = Ctx.customers.Find(customerid);

                customer.Startdate = DateTime.Now.Date;
                customer.Enddate = DateTime.Now.Date.AddDays((double)subscriptiontype.Days);
                customer.Subscription_ID = model.Subscription_ID;
                Ctx.SaveChanges();

                var attendData = Ctx.attends.SingleOrDefault(a => a.Customer_ID == customer.ID);

                attendData.AttendDate_Now = DateTime.Now.Date;
                attendData.AttendNumber = null;
                Ctx.SaveChanges();

                return RedirectToAction("Index");
            }
            else
                return View(model);



           
        }


        public ActionResult ExtendToday()
        {
           var  DateToday = DateTime.Now.Date;
            
            var data = Ctx.Extends.Where(a => a.Extenddate.Value == DateToday);

            return View(data);
        }


        public ActionResult GetValid()
        {

            return View(Ctx.spGetValide().AsEnumerable());
        }


        public ActionResult GetNonValid()
        {
            return View(Ctx.spGetNonValide().AsEnumerable());
        }
    }
}