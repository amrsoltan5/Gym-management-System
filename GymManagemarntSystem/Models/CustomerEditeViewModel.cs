using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GymManagemarntSystem.Models
{
    
    public class CustomerEditeViewModel
    {
        
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string phone { get; set; }
       
        public int Height { get; set; }
        public int Weight { get; set; }
        [Required]
        public DateTime Startdate { get; set; }
        [Required]
        public DateTime Enddate { get; set; }

        
        
        public int Subscription_ID { get; set; }

    }
}