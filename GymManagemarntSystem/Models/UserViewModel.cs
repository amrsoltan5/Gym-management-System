using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GymManagemarntSystem.Models
{
    public class UserViewModel
    {
      
        [Required]
        public string name { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string Role { get; set; }
        
    }
}