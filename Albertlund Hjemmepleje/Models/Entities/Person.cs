using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Albertlund_Hjemmepleje.Models.Entities
{
    public class Person
    {

       public int iD { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string occupation { get; set; }
        public uint phone { get; set; }

    }
}