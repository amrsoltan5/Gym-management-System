using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace GymManagemarntSystem.Models
{
    public class ExtendViewModel
    {
        public int id { get; set; }
        
        [Required]
        public string sessionID { get; set; }
        
        public DateTime Extenddate { get; set; }
        
        public customer customer { get; set; }
        [Required]
        public int Subscription_ID { get; set; }
    }
}