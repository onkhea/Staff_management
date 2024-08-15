using System;
using System.ComponentModel.DataAnnotations;

namespace StaffManagementAPI.Models
{
    public class Staff
    {
        [Key]
        [StringLength(8, MinimumLength = 8)]
        public string StaffID { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        [Required]
        [Range(1, 2)] // 1: Male, 2: Female
        public int Gender { get; set; }
    }
}

//The Staff model represents the entity in the database.