using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GymManagemarntSystem.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [ISexistPassword(ErrorMessage =" password is not vaild")]
        public string oldpassword { get; set; }

        [Required]
        
        public string newpassword { get; set; }

        [Required]
        [Compare("newpassword",ErrorMessage ="password Do not Match")]
        public string retypenewpassword { get; set; }
        [Required]
        public string token { get; set; }
    }
}