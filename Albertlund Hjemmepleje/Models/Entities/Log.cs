using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;


namespace Albertlund_Hjemmepleje.Models.Entities
{
  
    public class Log
    {
        [Key]
       public string email { get; set; }
        
        public DateTime time { get; set; }
       protected void OnModelCreating(DbModelBuilder modelBuilder)
      {
            // modelBuilder.Entity<Log>()
            //   .ToTable("LogTable");
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
        }

    }
}