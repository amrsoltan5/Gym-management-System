using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GymManagemarntSystem
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
         
            if(HttpContext.Current.Session["ID"] == null)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
                return;
            }

            base.OnActionExecuting(filterContext);
        }


    }
}