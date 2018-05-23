using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Albertlund_Hjemmepleje.Models
{
    public class Albertlund_HjemmeplejeContext2 : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Albertlund_HjemmeplejeContext2() : base("name=Albertlund_HjemmeplejeContext2")
        {
        }

    //    public System.Data.Entity.DbSet<Albertlund_Hjemmepleje.Models.Entities.Log> Logs { get; set; }
    }
}
