//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GymManagemarntSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class attend
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> AttendDate_Now { get; set; }
        public Nullable<int> Customer_ID { get; set; }
        public Nullable<int> AttendNumber { get; set; }
    
        public virtual customer customer { get; set; }
    }
}
