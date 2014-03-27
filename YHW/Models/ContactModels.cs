using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YHW.Models
{
    public class Feedback
    {
        public Int32 ID { get; set; }
        [Required(ErrorMessage="Name is Required")]
        public String Name { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,4}$",ErrorMessage="Must be formatted as Email Address")]
        public String Email { get; set; }
        [Required]
        public String Message { get; set; }
    }
}