using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Road.Models
{
    public class UserLogin
    {
        [Key]
        [Required(ErrorMessage = "*")]
        [Display(Name = "Login ID")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}