using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using GymManagemarntSystem.Models;


namespace GymManagemarntSystem
{
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property,AllowMultiple =true)]

    sealed public class ISexistphone:ValidationAttribute
    {
        GymSystem Ctx = new GymSystem();
        
        public override bool IsValid(object value)
        {
            string phone = (string)value;

            if (Ctx.customers.Any(a => a.Phone == phone))
            {
                return false;
            }
            else
                return true;

          
        }

    }
}