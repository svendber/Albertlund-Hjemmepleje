using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Albertlund_Hjemmepleje.Models.Entities
{
    public class Log
    {
        [Key]
       public string email { get; set; }
        
        public DateTime time { get; set; }


    }
}