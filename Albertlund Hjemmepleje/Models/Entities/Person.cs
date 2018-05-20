using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Albertlund_Hjemmepleje.Models.Entities
{
    public class Person
    {
        public string email { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string name { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string password { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public Boolean role { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string occupation { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public uint phone { get; set; }
       


    }
}


