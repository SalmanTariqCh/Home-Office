using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UKParliament.CodeTest.Data.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "User's Name")]
        [StringLength(100, ErrorMessage = "Name cannot be greater than 100 characters")]
        public string Name { get; set; }
    }
}
