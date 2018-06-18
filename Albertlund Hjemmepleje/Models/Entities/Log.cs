using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;


namespace Albertlund_Hjemmepleje.Models.Entities
{
  
    public class Log
    {
        [Key]
        public int id { get; set; }
     
        public string email { get; set; }
        
        public DateTime time { get; set; }
    
    }
}