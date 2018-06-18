using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Albertlund_Hjemmepleje.Models
{
    public class Albertlund_HjemmeplejeContext : DbContext
    {
        
    

        public Albertlund_HjemmeplejeContext() : base("name=Albertlund_HjemmeplejeContext"){}
    
  //      public Albertlund_HjemmeplejeContext() : base("AlberslundDB"){}
        
        public System.Data.Entity.DbSet<Albertlund_Hjemmepleje.Models.Entities.Person> People { get; set; }
        public System.Data.Entity.DbSet<Albertlund_Hjemmepleje.Models.Entities.Log> Logs { get; set; }

    }
}

