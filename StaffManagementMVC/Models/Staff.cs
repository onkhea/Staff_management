using System;

namespace StaffManagementMVC.Models
{
    public class Staff
    {
        public string StaffId { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public int Gender { get; set; } // 1: Male, 2: Female
    }
}
