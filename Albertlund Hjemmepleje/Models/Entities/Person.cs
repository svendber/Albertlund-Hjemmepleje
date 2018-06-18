using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Albertlund_Hjemmepleje.Models.Entities
{
    public class Person
    {

       [Key]
        public string email { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string name { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
      [DataType(DataType.Password)]
        public string password { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public Boolean role { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string occupation { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string phone { get; set; }
       


    }
}


