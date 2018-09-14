using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using GymManagemarntSystem.Models;

namespace GymManagemarntSystem
{

    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Field,AllowMultiple =true)]
   sealed public class ISexistPassword:ValidationAttribute
    {
        GymSystem Ctx = new GymSystem();
        public override bool IsValid(object value)
        {
            string password = (string)value;


            return( Ctx.users.Any(a => a.Password == password));

        }
    }
}